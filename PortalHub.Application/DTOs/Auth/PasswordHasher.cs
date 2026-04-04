using System.Security.Cryptography;
using System.Text;

namespace PortalHub.Application.Common
{
    public static class PasswordHasher
    {
        // =============================
        // CREATE HASH (Register / Reset)
        // =============================
        public static void CreateHash(
            string password,
            out byte[] passwordHash,
            out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();

            passwordSalt = hmac.Key; // random 512-bit salt
            passwordHash = hmac.ComputeHash(
                Encoding.UTF8.GetBytes(password));
        }

        // =============================
        // VERIFY HASH (Login)
        // =============================
        public static bool Verify(
            string password,
            byte[] storedHash,
            byte[] storedSalt)
        {
            using var hmac = new HMACSHA512(storedSalt);
            var computedHash = hmac.ComputeHash(
                Encoding.UTF8.GetBytes(password));

            return CryptographicOperations.FixedTimeEquals(
                computedHash,
                storedHash);
        }
    }
}
