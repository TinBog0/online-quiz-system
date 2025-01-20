using System.Security.Cryptography;
using System.Text;

namespace OnlineQuizSystemApi.Security
{
    public static class PasswordHelper
    {
        public static string HashPassword(string password, out string saltString)
        {
            using var hmac = new HMACSHA256();
            var salt = hmac.Key;
            saltString = Convert.ToBase64String(salt);
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
        }

        public static bool VerifyPassword(string inputPassword, string storedHash, string saltString)
        {
            var salt = Convert.FromBase64String(saltString);
            using var hmac = new HMACSHA256(salt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(inputPassword));
            return storedHash == Convert.ToBase64String(computedHash);
        }
    }
}
