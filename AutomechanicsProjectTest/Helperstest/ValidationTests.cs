using Xunit;

namespace AutomechanicsProjectTest.Classes
{
    public class ValidationTests
    {
        [Fact]
        public void IsValidLogin_ValidLogin_ReturnsTrue()
        {
            var result = Validation.IsValidLogin("user_123");

            Assert.True(result);
        }

        [Fact]
        public void IsValidLogin_InvalidLogin_ReturnsFalse()
        {
            var result = Validation.IsValidLogin("user@123");

            Assert.False(result);
        }

        [Fact]
        public void IsValidLoginLength_TooShort_ReturnsFalse()
        {
            var result = Validation.IsValidLoginLength("ab");

            Assert.False(result);
        }

        [Fact]
        public void IsValidLoginLength_ValidLength_ReturnsTrue()
        {
            var result = Validation.IsValidLoginLength("abcdef");

            Assert.True(result);
        }

        [Fact]
        public void IsValidPassword_ValidPassword_ReturnsTrue()
        {
            var result = Validation.IsValidPassword("abc123");

            Assert.True(result);
        }

        [Fact]
        public void IsValidPassword_NoDigits_ReturnsFalse()
        {
            var result = Validation.IsValidPassword("abcdef");

            Assert.False(result);
        }

        [Fact]
        public void IsValidPassword_TooShort_ReturnsFalse()
        {
            var result = Validation.IsValidPassword("a1");

            Assert.False(result);
        }

        [Fact]
        public void ValidatePrice_Valid_ReturnsTrue()
        {
            var result = Validation.ValidatePrice("123", out var price);

            Assert.True(result);
            Assert.Equal(123m, price);
        }

        [Fact]
        public void ValidatePrice_Invalid_ReturnsFalse()
        {
            var result = Validation.ValidatePrice("abc", out var price);

            Assert.False(result);
            Assert.Equal(0, price);
        }

        [Fact]
        public void ValidateQuantity_Valid_ReturnsTrue()
        {
            var result = Validation.ValidateQuantity("10", out var quantity);

            Assert.True(result);
            Assert.Equal(10, quantity);
        }

        [Fact]
        public void ValidateQuantity_Zero_ReturnsFalse()
        {
            var result = Validation.ValidateQuantity("0", out var quantity);

            Assert.False(result);
        }

        [Fact]
        public void IsValidRussianName_Valid_ReturnsTrue()
        {
            var result = Validation.IsValidRussianName("Иван Иванов");

            Assert.True(result);
        }

        [Fact]
        public void IsValidRussianName_Invalid_ReturnsFalse()
        {
            var result = Validation.IsValidRussianName("John");

            Assert.False(result);
        }
        [Fact]
        public void IsValidPassword_NoLetters_ReturnsFalse()
        {
            var result = Validation.IsValidPassword("123456");

            Assert.False(result);
        }

        [Fact]
        public void IsValidPassword_WithSpaces_ReturnsFalse()
        {
            var result = Validation.IsValidPassword("   ");

            Assert.False(result);
        }

        [Fact]
        public void IsValidLogin_Empty_ReturnsFalse()
        {
            var result = Validation.IsValidLogin("");

            Assert.False(result);
        }

        [Fact]
        public void IsValidNameLength_TooLong_ReturnsFalse()
        {
            var text = new string('а', 101);

            var result = Validation.IsValidNameLength(text);

            Assert.False(result);
        }

        [Fact]
        public void IsPasswordMatch_Same_ReturnsTrue()
        {
            var result = Validation.IsPasswordMatch("123", "123");

            Assert.True(result);
        }

        [Fact]
        public void IsPasswordMatch_Different_ReturnsFalse()
        {
            var result = Validation.IsPasswordMatch("123", "456");

            Assert.False(result);
        }
    }
}