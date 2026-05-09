using Microsoft.EntityFrameworkCore;
using PortalHub.Application.Interfaces.Auth;
using PortalHub.Domain.Models.Portal;

namespace PortalHub.Infrastructure.EF.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;

        public AuthRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserForLoginAsync(string email)
        {
            return await _context.Users
                .Include(x => x.Password)
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email == email); //&& x.IsActive
        }

        public async Task SaveRefreshTokenAsync(UserRefreshToken token)
        {
            _context.UserRefreshTokens.Add(token);
            await _context.SaveChangesAsync();
        }

        public async Task<UserRefreshToken?> GetRefreshTokenAsync(string token)
        {
            return await _context.UserRefreshTokens
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Token == token && !x.IsRevoked);
        }

        public async Task RevokeUserTokensAsync(long userId)
        {
            var tokens = await _context.UserRefreshTokens
                .Where(t => t.UserId == userId && !t.IsRevoked)
                .ToListAsync();

            foreach (var token in tokens)
            {
                token.IsRevoked = true;
                token.RevokedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
        }

           public async Task UpdateUserAsync(User user)
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }

        // auth log history

        public async Task LogAsync(AuthAuditLog logs)
        {
            _context.AuthAuditLogs.Add(logs);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetRecentFailedAttemptsByIpAsync(string ip, int minutes)
        {
            var since = DateTime.UtcNow.AddMinutes(-minutes);

            return await _context.AuthAuditLogs
                .Where(x =>
                    x.IpAddress == ip &&
                    !x.IsSuccess &&
                    x.CreatedAt >= since)
                .CountAsync();
        }

        public async Task BlockIpAsync(string ip, DateTime blockedUntil)
        {
            _context.BlockedIps.Add(new BlockedIp
            {
                IpAddress = ip,
                BlockedAt = DateTime.UtcNow,
                BlockedUntil = blockedUntil,
                Reason = "Too many failed login attempts"
            });

            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsIpBlockedAsync(string ip)
        {
            return await _context.BlockedIps
                .AnyAsync(x => x.IpAddress == ip && x.BlockedUntil > DateTime.UtcNow);
        }

        public async Task<User?> GetByEmailAsync(string email)
{
    return await _context.Users
        .FirstOrDefaultAsync(x => x.Email == email);
}

public async Task SaveOtpAsync(UserOtp otp)
{
    _context.UserOtps.Add(otp);
    await _context.SaveChangesAsync();
}

public async Task<UserOtp?> GetValidOtpAsync(
    long userId,
    string otpCode,
    OtpType otpType)
{
    return await _context.UserOtps
        .FirstOrDefaultAsync(x =>
            x.UserId == userId &&
            x.OtpCode == otpCode &&
            x.OtpType == otpType &&
            !x.IsUsed &&
            x.ExpireAt > DateTime.UtcNow);
}

public async Task UpdateOtpAsync(UserOtp otp)
{
    _context.UserOtps.Update(otp);
    await _context.SaveChangesAsync();
}

public async Task<UserPassword?> GetPasswordAsync(long userId)
{
    return await _context.UserPasswords
        .FirstOrDefaultAsync(x => x.UserId == userId);
}

public async Task UpdatePasswordAsync(UserPassword password)
{
    _context.UserPasswords.Update(password);
    await _context.SaveChangesAsync();
}

    }
}
