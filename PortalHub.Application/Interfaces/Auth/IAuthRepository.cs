using PortalHub.Domain.Models.Portal;

namespace PortalHub.Application.Interfaces.Auth
{
    public interface IAuthRepository
    {
        Task<User?> GetUserForLoginAsync(string email);

        Task SaveRefreshTokenAsync(UserRefreshToken token);

        Task RevokeUserTokensAsync(long userId);

        Task<UserRefreshToken?> GetRefreshTokenAsync(string token);

        Task UpdateUserAsync(User user);

        Task LogAsync(AuthAuditLog auditLog);

        Task<int> GetRecentFailedAttemptsByIpAsync(
            string ip,
            int minutes);

        Task BlockIpAsync(
            string ip,
            DateTime blockedUntil);

        Task<bool> IsIpBlockedAsync(string ip);

        // ================= PASSWORD RESET =================

        Task<User?> GetByEmailAsync(string email);

        Task SaveOtpAsync(UserOtp otp);

        Task<UserOtp?> GetValidOtpAsync(
            long userId,
            string otpCode,
            OtpType otpType);

        Task UpdateOtpAsync(UserOtp otp);

        Task<UserPassword?> GetPasswordAsync(long userId);

        Task UpdatePasswordAsync(UserPassword password);
    }
}