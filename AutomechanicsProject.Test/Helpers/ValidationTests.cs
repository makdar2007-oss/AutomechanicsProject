using Xunit;

namespace AutomechanicsProject.Tests.Helpers
{
    public class ValidationTests
    {
        [Fact]
        public void IsValidRussianName_WithRussianLetters_ReturnsTrue()
        {
            var result = Validation.IsValidRussianName("Иванов");
            Assert.True(result);
        }

        [Fact]
        public void IsValidRussianName_WithEnglishLetters_ReturnsFalse()
        {
            var result = Validation.IsValidRussianName("Ivanov");
            Assert.False(result);
        }

        [Fact]
        public void IsValidRussianName_WithNumbers_ReturnsFalse()
        {
            var result = Validation.IsValidRussianName("Иванов123");
            Assert.False(result);
        }

        [Fact]
        public void IsValidRussianName_WithHyphen_ReturnsTrue()
        {
            var result = Validation.IsValidRussianName("Анна-Мария");
            Assert.True(result);
        }

        [Fact]
        public void IsValidLogin_WithEnglishLettersAndNumbers_ReturnsTrue()
        {
            var result = Validation.IsValidLogin("user_123");
            Assert.True(result);
        }

        [Fact]
        public void IsValidLogin_WithRussianLetters_ReturnsFalse()
        {
            var result = Validation.IsValidLogin("пользователь");
            Assert.False(result);
        }

        [Fact]
        public void IsValidLoginLength_With3To20Chars_ReturnsTrue()
        {
            Assert.True(Validation.IsValidLoginLength("user"));
            Assert.True(Validation.IsValidLoginLength("verylongusername123"));
            Assert.False(Validation.IsValidLoginLength("ab"));
            Assert.False(Validation.IsValidLoginLength(""));
        }

        [Fact]
        public void IsValidPassword_With6CharsLetterAndDigit_ReturnsTrue()
        {
            var result = Validation.IsValidPassword("pass12");
            Assert.True(result);
        }

        [Fact]
        public void IsValidPassword_TooShort_ReturnsFalse()
        {
            var result = Validation.IsValidPassword("pas1");
            Assert.False(result);
        }

        [Fact]
        public void IsValidPassword_NoDigit_ReturnsFalse()
        {
            var result = Validation.IsValidPassword("password");
            Assert.False(result);
        }

        [Fact]
        public void IsValidPassword_NoLetter_ReturnsFalse()
        {
            var result = Validation.IsValidPassword("123456");
            Assert.False(result);
        }

        [Fact]
        public void IsPasswordMatch_WithSamePasswords_ReturnsTrue()
        {
            var result = Validation.IsPasswordMatch("pass123", "pass123");
            Assert.True(result);
        }

        [Fact]
        public void IsPasswordMatch_WithDifferentPasswords_ReturnsFalse()
        {
            var result = Validation.IsPasswordMatch("pass123", "pass456");
            Assert.False(result);
        }

        [Fact]
        public void ValidatePrice_WithValidPrice_ReturnsTrue()
        {
            var result = Validation.ValidatePrice("1500,50", out decimal price);
            Assert.True(result);
            Assert.Equal(1500.50m, price);
        }

        [Fact]
        public void ValidatePrice_WithNegativePrice_ReturnsFalse()
        {
            var result = Validation.ValidatePrice("-100", out _);
            Assert.False(result);
        }

        [Fact]
        public void ValidateQuantity_WithPositiveInteger_ReturnsTrue()
        {
            var result = Validation.ValidateQuantity("10", out int quantity);
            Assert.True(result);
            Assert.Equal(10, quantity);
        }

        [Fact]
        public void ValidateQuantity_WithZero_ReturnsFalse()
        {
            var result = Validation.ValidateQuantity("0", out _);
            Assert.False(result);
        }

        [Fact]
        public void IsWatermark_WithWatermarkText_ReturnsTrue()
        {
            var result = Validation.IsWatermark("Введите текст", "Введите текст");
            Assert.True(result);
        }

        [Fact]
        public void IsWatermark_WithEmptyString_ReturnsTrue()
        {
            var result = Validation.IsWatermark("", "watermark");
            Assert.True(result);
        }

        [Fact]
        public void IsWatermark_WithNormalText_ReturnsFalse()
        {
            var result = Validation.IsWatermark("нормальный текст", "watermark");
            Assert.False(result);
        }
    }
}