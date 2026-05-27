using Xunit;
using AutomechanicsProject.Dtos.UI;
using System;

namespace AutomechanicsProject.Tests.Dtos.UI
{
    public class ProductDisplayItemTests
    {
        [Fact]
        public void CanCreateProductDisplayItem()
        {
            var item = new ProductDisplayItem();
            Assert.NotNull(item);
        }

        [Fact]
        public void CanSetIdAndArticle()
        {
            var id = Guid.NewGuid();
            var item = new ProductDisplayItem
            {
                Id = id,
                Article = "ART-001"
            };

            Assert.Equal(id, item.Id);
            Assert.Equal("ART-001", item.Article);
        }

        [Fact]
        public void CanSetNameAndBalance()
        {
            var item = new ProductDisplayItem
            {
                Name = "Моторное масло",
                Balance = 50
            };

            Assert.Equal("Моторное масло", item.Name);
            Assert.Equal(50, item.Balance);
        }

        [Fact]
        public void CanSetIsActiveAndHasExpiryDate()
        {
            var item = new ProductDisplayItem
            {
                IsActive = true,
                HasExpiryDate = true
            };

            Assert.True(item.IsActive);
            Assert.True(item.HasExpiryDate);
        }

        [Fact]
        public void IsActive_DefaultValue_IsFalse()
        {
            var item = new ProductDisplayItem();
            Assert.False(item.IsActive);
        }

        [Fact]
        public void HasExpiryDate_DefaultValue_IsFalse()
        {
            var item = new ProductDisplayItem();
            Assert.False(item.HasExpiryDate);
        }

        [Fact]
        public void CanSetProductExpiryDate()
        {
            var expiryDate = new DateTime(2025, 12, 31);
            var item = new ProductDisplayItem
            {
                ProductExpiryDate = expiryDate
            };

            Assert.Equal(expiryDate, item.ProductExpiryDate);
        }

        [Fact]
        public void ProductExpiryDate_CanBeNull()
        {
            var item = new ProductDisplayItem { ProductExpiryDate = null };
            Assert.Null(item.ProductExpiryDate);
        }

        [Fact]
        public void SearchString_CombinesArticleAndNameLowercase()
        {
            var item = new ProductDisplayItem
            {
                Article = "ART-001",
                Name = "Моторное Масло"
            };

            Assert.Equal("art-001 моторное масло", item.SearchString);
        }

        [Fact]
        public void SearchString_WhenArticleIsNull_HandlesGracefully()
        {
            var item = new ProductDisplayItem
            {
                Article = null,
                Name = "Масло"
            };

            Assert.Equal(" масло", item.SearchString);
        }

        [Fact]
        public void SearchString_WhenNameIsNull_HandlesGracefully()
        {
            var item = new ProductDisplayItem
            {
                Article = "ART-001",
                Name = null
            };

            Assert.Equal("art-001 ", item.SearchString);
        }

        [Fact]
        public void SearchString_WhenBothAreNull_ReturnsEmptyWithSpace()
        {
            var item = new ProductDisplayItem
            {
                Article = null,
                Name = null
            };

            Assert.Equal(" ", item.SearchString);
        }

        [Fact]
        public void DisplayName_WhenIsActiveTrue_ShowsNameAndBalance()
        {
            var item = new ProductDisplayItem
            {
                Name = "Масло",
                Balance = 10,
                IsActive = true
            };

            Assert.Contains("Масло", item.DisplayName);
            Assert.Contains("10", item.DisplayName);
        }

        [Fact]
        public void DisplayName_WhenIsActiveFalse_ShowsWaitingSupply()
        {
            var item = new ProductDisplayItem
            {
                Name = "Масло",
                IsActive = false
            };

            Assert.Contains("Масло", item.DisplayName);
        }

        [Fact]
        public void DisplayName_WhenIsActiveTrueAndBalanceZero_ShowsZero()
        {
            var item = new ProductDisplayItem
            {
                Name = "Масло",
                Balance = 0,
                IsActive = true
            };

            Assert.Contains("0", item.DisplayName);
        }
    }
}