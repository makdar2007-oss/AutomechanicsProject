using Xunit;
using AutomechanicsProject.Dtos.Service;
using System;

namespace AutomechanicsProject.Tests.Dtos.Service
{
    public class ProductListItemDtoTests
    {
        [Fact]
        public void CanCreateProductListItemDto()
        {
            var dto = new ProductListItemDto();
            Assert.NotNull(dto);
        }

        [Fact]
        public void CanSetAllProperties()
        {
            var id = Guid.NewGuid();
            var expiryDate = new DateTime(2025, 12, 31);

            var dto = new ProductListItemDto
            {
                Id = id,
                Article = "ART-001",
                Name = "Масло",
                CategoryName = "Смазочные",
                UnitName = "л",
                Balance = 50,
                ExpiryDate = expiryDate,
                Price = 1500m,
                PurchasePrice = 1000m,
                RequiresDiscount = true,
                IsExpired = false
            };

            Assert.Equal(id, dto.Id);
            Assert.Equal("ART-001", dto.Article);
            Assert.Equal("Масло", dto.Name);
            Assert.Equal("Смазочные", dto.CategoryName);
            Assert.Equal("л", dto.UnitName);
            Assert.Equal(50, dto.Balance);
            Assert.Equal(expiryDate, dto.ExpiryDate);
            Assert.Equal(1500m, dto.Price);
            Assert.Equal(1000m, dto.PurchasePrice);
            Assert.True(dto.RequiresDiscount);
            Assert.False(dto.IsExpired);
        }

        [Fact]
        public void RequiresDiscount_DefaultValue_IsFalse()
        {
            var dto = new ProductListItemDto();
            Assert.False(dto.RequiresDiscount);
        }

        [Fact]
        public void IsExpired_DefaultValue_IsFalse()
        {
            var dto = new ProductListItemDto();
            Assert.False(dto.IsExpired);
        }
    }
}