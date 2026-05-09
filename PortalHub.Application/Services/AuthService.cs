using PortalHub.Application.Common;
using PortalHub.Application.DTOs.Auth;
using PortalHub.Application.Interfaces.Auth;
using PortalHub.Domain.Models.Portal;
using Microsoft.Extensions.Options;

namespace PortalHub.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repo;
        private readonly IJwtTokenGenerator _jwt;
        private readonly JwtSettings _settings;
       

        public AuthService(
            IAuthRepository repo,
            IJwtTokenGenerator jwt,
            IOptions<JwtSettings> settings
           
            )
        {
            _repo = repo;
            _jwt = jwt;
            _settings = settings.Value;
           
        }

        public async Task<ServiceResult<LoginResponseDto>> LoginAsync(LoginRequestDto request,string ipAddress,string userAgent)
        {

            var blocked = await _repo.IsIpBlockedAsync(ipAddress);

            if (blocked)
            {
                return ServiceResult<LoginResponseDto>.Fail( "Too many login attempts from your IP. Try again later.", AuthAuditEvents.IpBlocked);
            }


            var identifier = request.Email.Trim();

            var user = await _repo.GetUserForLoginAsync(identifier);

            if (user == null)
            {

                await _repo.LogAsync(new AuthAuditLog
                {
                    UserId = null,
                    Email = identifier,
                    IsSuccess = false,
                    EventType = AuthAuditEvents.LoginFailed,
                    Reason = "User Not Found",
                    IpAddress = ipAddress,
                    UserAgent = userAgent,
                    CreatedAt = DateTime.UtcNow
                });
                return ServiceResult<LoginResponseDto>.Fail("Invalid email or password",ErrorCodes.InvalidCredentials);

            }
             if (user.IsLocked && user.LockoutEnd > DateTime.UtcNow)
            {
                await _repo.LogAsync(new AuthAuditLog
                {
                    UserId = null,
                    Email = identifier,
                    IsSuccess = false,
                    EventType = AuthAuditEvents.AccountLocked,
                    Reason = "Account Locked",
                    IpAddress = ipAddress,
                    UserAgent = userAgent,
                    CreatedAt = DateTime.UtcNow
                });
               
                    return ServiceResult<LoginResponseDto>.Fail("Account locked due to multiple failed login attempts. Try again later.",AuthAuditEvents.AccountLocked);
                }
              

            var valid = PasswordHasher.Verify(request.Password,user.Password.PasswordHash,user.Password.PasswordSalt);

               if (!valid)
                {
                  user.FailedLoginAttempts++;
                  user.LastFailedLogin = DateTime.UtcNow;

                    if (user.FailedLoginAttempts >= 5)
                    {
                        user.IsLocked = true;
                        user.LockoutEnd = DateTime.UtcNow.AddMinutes(60);
                    }

                    await _repo.UpdateUserAsync(user);

                await _repo.LogAsync(new AuthAuditLog
                {
                    UserId = null,
                    Email = identifier,
                    IsSuccess = false,
                    EventType = AuthAuditEvents.LoginFailed,
                    Reason = "Wrong password or email",
                    IpAddress = ipAddress,
                    UserAgent = userAgent,
                    CreatedAt = DateTime.UtcNow
                });

                // CHECK BRUTE FORCE BY IP
                var failedAttempts = await _repo.GetRecentFailedAttemptsByIpAsync(ipAddress, 5);

                if (failedAttempts >= 10)
                {
                    await _repo.BlockIpAsync(ipAddress, DateTime.UtcNow.AddHours(1));
                }
                //10 failed attempts Per IP => within 5 minutes => block IP for 1 hour

                return ServiceResult<LoginResponseDto>.Fail( "Invalid email or password",ErrorCodes.InvalidCredentials);
                }
            //if (!user.IsEmailVerified)
            //    return ServiceResult<LoginResponseDto>.Fail("Email not verified",ErrorCodes.EmailNotVerified);

            //if (!user.IsPhoneVerified)
            //    return ServiceResult<LoginResponseDto>.Fail("Mobile not verified", ErrorCodes.MobileNotVerified);

            if (!user.IsActive)
                {
                await _repo.LogAsync(new AuthAuditLog
                {
                    UserId = null,
                    Email = identifier,
                    IsSuccess = false,
                    EventType = AuthAuditEvents.AccountInactive,
                    Reason = "Account Inactive",
                    IpAddress = ipAddress,
                    UserAgent = userAgent,
                    CreatedAt = DateTime.UtcNow
                });
               
                    return ServiceResult<LoginResponseDto>.Fail("Account is inactive",ErrorCodes.UserInactive);
                }

            await _repo.RevokeUserTokensAsync(user.UserId);

            var accessToken = _jwt.GenerateAccessToken(user);
            var refreshToken = _jwt.GenerateRefreshToken();

           

            await _repo.SaveRefreshTokenAsync(new UserRefreshToken
            {
                UserId = user.UserId,
                Token = refreshToken,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            });


            user.FailedLoginAttempts = 0;
            user.IsLocked = false;
            user.LockoutEnd = null;

            await _repo.UpdateUserAsync(user);

            await _repo.LogAsync(new AuthAuditLog
            {
                UserId = user.UserId,
                Email = identifier,
                IsSuccess = true, // false 09.05.2026 check it
                EventType = AuthAuditEvents.LoginSuccess,
                Reason = "Login successful",
                IpAddress = ipAddress,
                UserAgent = userAgent,
                CreatedAt = DateTime.UtcNow
            });

          

            return ServiceResult<LoginResponseDto>.Ok(new LoginResponseDto
            {
                UserId = user.UserId,
                FullName = $"{user.FirstName} {user.LastName}",
                Role = user.Role.RoleName,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(_settings.AccessTokenMinutes)
            });
        }
    
    public async Task<ServiceResult<bool>> ForgotPasswordAsync(ForgotPasswordDto dto)
{
    var user = await _repo.GetByEmailAsync(dto.Email.Trim());

    // do not expose user existence
    if (user == null)
    {
        return ServiceResult<bool>.Ok(
            true,
            "If account exists, OTP sent");
    }

    // generate otp
    var otp = new Random().Next(100000, 999999).ToString();

    await _repo.SaveOtpAsync(new UserOtp
    {
        UserId = user.UserId,
        OtpCode = otp,
        OtpType = OtpType.PasswordReset,
        CreatedAt = DateTime.UtcNow,
        ExpireAt = DateTime.UtcNow.AddMinutes(10),
        IsUsed = false,
        AttemptCount = 0
    });

    // TODO:
    // send otp via email/sms here

    return ServiceResult<bool>.Ok(
        true,
        "Password reset OTP sent");
}
    public async Task<ServiceResult<bool>> VerifyResetOtpAsync(VerifyResetOtpDto dto)
{
    var user = await _repo.GetByEmailAsync(dto.Email.Trim());

    if (user == null)
    {
        return ServiceResult<bool>.Fail(
            "Invalid OTP",
            ErrorCodes.ValidationError);
    }

    var otp = await _repo.GetValidOtpAsync(
        user.UserId,
        dto.OtpCode,
        OtpType.PasswordReset);

    if (otp == null)
    {
        return ServiceResult<bool>.Fail(
            "Invalid or expired OTP",
            ErrorCodes.ValidationError);
    }

    otp.VerifiedAt = DateTime.UtcNow;

    await _repo.UpdateOtpAsync(otp);

    return ServiceResult<bool>.Ok(
        true,
        "OTP verified");
}
    public async Task<ServiceResult<bool>> ResetPasswordAsync(ResetPasswordDto dto)
{
    if (dto.NewPassword != dto.ConfirmPassword)
    {
        return ServiceResult<bool>.Fail(
            "Password and confirm password mismatch",
            ErrorCodes.ValidationError);
    }

    var user = await _repo.GetByEmailAsync(dto.Email.Trim());

    if (user == null)
    {
        return ServiceResult<bool>.Fail(
            "Invalid request",
            ErrorCodes.ValidationError);
    }

    var otp = await _repo.GetValidOtpAsync(
        user.UserId,
        dto.OtpCode,
        OtpType.PasswordReset);

    if (otp == null)
    {
        return ServiceResult<bool>.Fail(
            "Invalid or expired OTP",
            ErrorCodes.ValidationError);
    }

    var password = await _repo.GetPasswordAsync(user.UserId);

    if (password == null)
    {
        return ServiceResult<bool>.Fail(
            "Password record not found",
            ErrorCodes.NotFound);
    }

    // hash password
    PasswordHasher.CreateHash(
        dto.NewPassword,
        out byte[] hash,
        out byte[] salt);

    password.PasswordHash = hash;
    password.PasswordSalt = salt;
    password.UpdatedAt = DateTime.UtcNow;

    await _repo.UpdatePasswordAsync(password);

    // mark otp used
    otp.IsUsed = true;
    otp.VerifiedAt = DateTime.UtcNow;

    await _repo.UpdateOtpAsync(otp);

    // revoke old login sessions
    await _repo.RevokeUserTokensAsync(user.UserId);

    return ServiceResult<bool>.Ok(
        true,
        "Password reset successful");
}



    }


}
