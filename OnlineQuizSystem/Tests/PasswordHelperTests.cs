using OnlineQuizSystemApi.Security;
using System.Text;

namespace Tests
{
    public class PasswordHelperTests
    {

        public PasswordHelperTests()
        {
        }

        [Fact]
        public void VerifyPassword_ShouldReturnTrue_ForMatchingPassword()
        {
            // Arrange
            string password = "SecurePassword123!";
            string saltString = GenerateSaltString();
            string storedHash = HashPassword(password, saltString);

            // Act
            var result = PasswordHelper.VerifyPassword(password, storedHash, saltString);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void VerifyPassword_ShouldReturnFalse_ForNonMatchingPassword()
        {
            // Arrange
            string password = "SecurePassword123!";
            string saltString = GenerateSaltString();
            string storedHash = HashPassword(password, saltString);
            string incorrectPassword = "WrongPassword";

            // Act
            var result = PasswordHelper.VerifyPassword(incorrectPassword, storedHash, saltString);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void VerifyPassword_ShouldReturnFalse_ForDifferentSalt()
        {
            // Arrange
            string password = "SecurePassword123!";
            string saltString = GenerateSaltString();
            string storedHash = HashPassword(password, saltString);
            string differentSaltString = GenerateSaltString();

            // Act
            var result = PasswordHelper.VerifyPassword(password, storedHash, differentSaltString);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void VerifyPassword_ShouldThrowException_ForInvalidSaltString()
        {
            // Arrange
            string password = "SecurePassword123!";
            string invalidSaltString = "InvalidBase64Salt";
            string storedHash = "SomeHash";

            // Act & Assert
            Assert.Throws<FormatException>(() =>
                PasswordHelper.VerifyPassword(password, storedHash, invalidSaltString)
            );
        }

        private string GenerateSaltString()
        {
            using var hmac = new System.Security.Cryptography.HMACSHA256();
            return Convert.ToBase64String(hmac.Key);
        }

        private string HashPassword(string password, string saltString)
        {
            var salt = Convert.FromBase64String(saltString);
            using var hmac = new System.Security.Cryptography.HMACSHA256(salt);
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
        }
    }
}
