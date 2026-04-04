using AutoMapper;
using PortalHub.Application.Common;
using PortalHub.Application.DTOs.Portal;
using PortalHub.Application.Interfaces.Portal;
using PortalHub.Application.Interfaces.Repositories;
using PortalHub.Domain.Models.Portal;
using System.Security.Cryptography;
using Microsoft.AspNetCore.WebUtilities;

namespace PortalHub.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepo;
        private readonly IRepository<UserPassword> _passwordRepo;
        private readonly IRepository<UserOtp> _otpRepo;
        private readonly IMapper _mapper;
        private const int MAX_OTP_ATTEMPTS = 3;

        public UserService(
            IRepository<User> userRepo,
            IRepository<UserPassword> passwordRepo,
            IRepository<UserOtp> otpRepo,
            IMapper mapper)
        {
            _userRepo = userRepo;
            _passwordRepo = passwordRepo;
            _otpRepo = otpRepo;
            _mapper = mapper;
        }

        // ---------- REGISTER ----------
        public async Task<ServiceResult<UserDto>> RegisterAsync(CreateUserDto dto)
        {
           

            var existingUser = await _userRepo.FirstOrDefaultAsync( x => x.Email == dto.Email || x.Phone == dto.Phone);

            if (existingUser?.Email == dto.Email)
                return ServiceResult<UserDto>.Fail("Email already registered", ErrorCodes.EmailAlreadyExists);

            if (existingUser?.Phone == dto.Phone)
                return ServiceResult<UserDto>.Fail("Mobile already registered", ErrorCodes.MobileAlreadyExists);
            var user = _mapper.Map<User>(dto);
            await _userRepo.AddAsync(user);
            await _userRepo.SaveChangesAsync();

            PasswordHasher.CreateHash(dto.Password,out var hash,out var salt);

            await _passwordRepo.AddAsync(new UserPassword
            {
                UserId = user.UserId,
                PasswordHash = hash,
                PasswordSalt = salt,
                UpdatedAt = DateTime.UtcNow
            });

            await _passwordRepo.SaveChangesAsync();

            // Generate Email OTP
            var emailOtp = OtpGenerator.GenerateEmailVerificationToken();

            await _otpRepo.AddAsync(new UserOtp
            {
                UserId = user.UserId,
                OtpCode = emailOtp,
                OtpType= OtpType.EmailVerification,
                ExpireAt = DateTime.UtcNow.AddMinutes(10),
                IsUsed = false,
                CreatedAt = DateTime.UtcNow
            });
            //await _passwordRepo.SaveChangesAsync();

            // Generate Mobile OTP
            var mobileOtp= OtpGenerator.GenerateMobileOtp();
            await _otpRepo.AddAsync(new UserOtp
            {
                UserId = user.UserId,
                OtpCode = mobileOtp,
                OtpType = OtpType.MobileVerification,
                ExpireAt = DateTime.UtcNow.AddMinutes(10),
                IsUsed = false,
                CreatedAt = DateTime.UtcNow
            });
            await _otpRepo.SaveChangesAsync();
           

            return ServiceResult<UserDto>.Ok(
                _mapper.Map<UserDto>(user),
                "User registered successfully");
        }

        // ---------- UPDATE PROFILE ----------
        public async Task<ServiceResult<UserDto>> UpdateProfileAsync(long userId,UpdateUserDto dto)
        {
            var user = await _userRepo.FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
                return ServiceResult<UserDto>.Fail("User not found",ErrorCodes.NotFound);
            var existingUser = await _userRepo.FirstOrDefaultAsync(x => x.Phone == dto.Phone && x.UserId != userId);


            if (existingUser?.Phone == dto.Phone)
                return ServiceResult<UserDto>.Fail("Mobile already registered", ErrorCodes.MobileAlreadyExists);

            _mapper.Map(dto, user);

            await _userRepo.UpdateAsync(user);
            await _userRepo.SaveChangesAsync();

            return ServiceResult<UserDto>.Ok(_mapper.Map<UserDto>(user),"Profile updated");
        }

        // ---------- VERIFY EMAIL ----------
        //public async Task<ServiceResult<bool>> VerifyEmailAsync(long userId)
        //{
        //    var user = await _userRepo
        //        .FirstOrDefaultAsync(u => u.UserId == userId);

        //    if (user == null)
        //        return ServiceResult<bool>.Fail(
        //            "User not found",
        //            ErrorCodes.NotFound);

        //    user.IsEmailVerified = true;
        //    user.EmailVerifiedAt = DateTime.UtcNow;

        //    await _userRepo.SaveChangesAsync();

        //    return ServiceResult<bool>.Ok(true, "Email verified");
        //}

        public async Task<ServiceResult<bool>> VerifyEmailAsync(VerifyEmailDto dto)
        {
            var user = await _userRepo
               .FirstOrDefaultAsync(u => u.UserId == dto.UserId && u.Email == dto.Email);


            if (user == null)
                return ServiceResult<bool>.Fail(
                    "User not found",
                    ErrorCodes.NotFound);

            var otp = await _otpRepo.FirstOrDefaultAsync(
                     x => x.UserId == dto.UserId &&
                          x.OtpType == OtpType.EmailVerification &&
                          !x.IsUsed, //&& x.ExpireAt > DateTime.UtcNow,
                     q => q.OrderByDescending(x => x.CreatedAt)
                 );

            if (otp == null)
                return ServiceResult<bool>.Fail("Invalid or expired email token", ErrorCodes.InvalidEmailToken);
            if (otp.ExpireAt < DateTime.UtcNow)
            {
                otp.IsUsed = true;
                await _otpRepo.UpdateAsync(otp);
                await _otpRepo.SaveChangesAsync();
                return ServiceResult<bool>.Fail("expired email token", ErrorCodes.InvalidEmailToken);
            }
            if (otp.AttemptCount >= MAX_OTP_ATTEMPTS) 
            {
                otp.IsUsed = true;
                await _otpRepo.UpdateAsync(otp);
                await _otpRepo.SaveChangesAsync();
                return ServiceResult<bool>.Fail("Too many attempts. Please request a new email token", ErrorCodes.InvalidEmailToken);
            }
               
            if (otp.OtpCode != dto.OtpCode)
            {
                otp.AttemptCount++;

                await _otpRepo.UpdateAsync(otp);
                await _otpRepo.SaveChangesAsync();

                return ServiceResult<bool>.Fail("Invalid Email Token", ErrorCodes.InvalidEmailToken);
            }

            user.IsEmailVerified = true;
            user.EmailVerifiedAt = DateTime.UtcNow;

            otp.IsUsed = true;
            otp.VerifiedAt = DateTime.UtcNow;

            await _userRepo.SaveChangesAsync();

            return ServiceResult<bool>.Ok(true, "Email verified");
        }


        public async Task<ServiceResult<bool>> VerifyMobileAsync(VerifyMobileDto dto)
        {
            var user = await _userRepo
               .FirstOrDefaultAsync(u => u.UserId == dto.UserId && u.Phone == dto.Mobile);


            if (user == null)
                return ServiceResult<bool>.Fail(
                    "User not found",
                    ErrorCodes.NotFound);

            var otp = await _otpRepo.FirstOrDefaultAsync(
                     x => x.UserId == dto.UserId &&
                          x.OtpType == OtpType.MobileVerification &&
                          !x.IsUsed, // && x.ExpireAt > DateTime.UtcNow,
                     q => q.OrderByDescending(x => x.CreatedAt)
                 );

            if (otp == null)
                return ServiceResult<bool>.Fail("Invalid or expired OTP", ErrorCodes.InvalidOTP);
            if (otp.ExpireAt < DateTime.UtcNow)
            {
                otp.IsUsed = true;
                await _otpRepo.UpdateAsync(otp);
                await _otpRepo.SaveChangesAsync();
                return ServiceResult<bool>.Fail("OTP expired", ErrorCodes.ExpiredOTP);
            }
            if (otp.AttemptCount >= MAX_OTP_ATTEMPTS)
            {
                otp.IsUsed = true;
                await _otpRepo.UpdateAsync(otp);
                await _otpRepo.SaveChangesAsync();
                return ServiceResult<bool>.Fail("Too many attempts. Please request a new OTP", ErrorCodes.TooManyAttemptOTP);
            }
                
            if (otp.OtpCode != dto.OtpCode)
            {
                otp.AttemptCount++;

                await _otpRepo.UpdateAsync(otp);
                await _otpRepo.SaveChangesAsync();

                return ServiceResult<bool>.Fail("Invalid OTP", ErrorCodes.InvalidOTP);
            }

            user.IsPhoneVerified = true;
            user.PhoneVerifiedAt = DateTime.UtcNow;

            otp.IsUsed = true;
            otp.VerifiedAt = DateTime.UtcNow;

            await _userRepo.SaveChangesAsync();

            return ServiceResult<bool>.Ok(true, "Mobile verified");
        }


       
        public async Task<UserDto?> GetByIdAsync(long userId)
        {
            var entity = await _userRepo.GetByIdAsync(userId);
            return entity == null ? default : _mapper.Map<UserDto>(entity);
        }
    }


    public static class OtpGenerator
    {
        public static string GenerateMobileOtp()
        {
            byte[] bytes = new byte[4];
            RandomNumberGenerator.Fill(bytes);

            int value = BitConverter.ToInt32(bytes, 0) & int.MaxValue;

            return (value % 900000 + 100000).ToString();
        }



        public static string GenerateEmailVerificationToken()
        {
            var bytes = new byte[32]; // 256-bit token
            RandomNumberGenerator.Fill(bytes);

            return WebEncoders.Base64UrlEncode(bytes);
        }
    }


}
