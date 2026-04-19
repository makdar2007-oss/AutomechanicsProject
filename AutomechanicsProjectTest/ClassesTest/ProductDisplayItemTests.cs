using Xunit;
using AutomechanicsProject.Classes;

namespace AutomechanicsProjectTest.Classes
{
    public class ProductDisplayItemTests_Extended
    {
        [Fact]
        public void DisplayName_WithZeroBalance_ShowsZero()
        {
            var item = new ProductDisplayItem
            {
                Name = "Товар",
                Balance = 0,
                IsActive = true
            };

            var result = item.DisplayName;

            Assert.Contains("0", result);
        }

        [Fact]
        public void DisplayName_WithNullName_DoesNotCrash()
        {
            var item = new ProductDisplayItem
            {
                Name = null,
                Balance = 5,
                IsActive = true
            };

            var result = item.DisplayName;

            Assert.NotNull(result);
        }

        [Fact]
        public void SearchString_WithNullFields_DoesNotCrash()
        {
            var item = new ProductDisplayItem
            {
                Name = null,
                Article = null
            };

            var result = item.SearchString;

            Assert.NotNull(result);
        }

        [Fact]
        public void SearchString_AlwaysLowercase()
        {
            var item = new ProductDisplayItem
            {
                Name = "ТОВАР",
                Article = "ABC123"
            };

            var result = item.SearchString;

            Assert.Equal(result, result.ToLower());
        }

        [Fact]
        public void SearchString_ContainsBothFields()
        {
            var item = new ProductDisplayItem
            {
                Name = "Товар",
                Article = "ABC123"
            };

            var result = item.SearchString;

            Assert.Contains("abc123", result);
            Assert.Contains("товар", result);
        }
    }
}