using Xunit;

namespace AutomechanicsProjectTest.Classes
{
    public class PasswordHelperTests
    {
        [Fact]
        public void HashPassword_ReturnsDifferentString()
        {
            var password = "test123";

            var hash = PasswordHelper.HashPassword(password);

            Assert.NotEqual(password, hash);
        }

        [Fact]
        public void VerifyPassword_CorrectPassword_ReturnsTrue()
        {
            var password = "test123";
            var hash = PasswordHelper.HashPassword(password);

            var result = PasswordHelper.VerifyPassword(password, hash);

            Assert.True(result);
        }

        [Fact]
        public void VerifyPassword_WrongPassword_ReturnsFalse()
        {
            var hash = PasswordHelper.HashPassword("test123");

            var result = PasswordHelper.VerifyPassword("wrong", hash);

            Assert.False(result);
        }

        [Fact]
        public void HashPassword_SamePasswordDifferentHashes()
        {
            var password = "test123";

            var hash1 = PasswordHelper.HashPassword(password);
            var hash2 = PasswordHelper.HashPassword(password);

            Assert.NotEqual(hash1, hash2);
        }

        [Fact]
        public void VerifyPassword_EmptyPassword_ReturnsFalse()
        {
            var hash = PasswordHelper.HashPassword("test123");

            var result = PasswordHelper.VerifyPassword("", hash);

            Assert.False(result);
        }
    }
}