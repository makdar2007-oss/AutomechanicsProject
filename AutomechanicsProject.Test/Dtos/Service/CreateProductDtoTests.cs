using Xunit;
using AutomechanicsProject.Dtos.Service;
using System;

namespace AutomechanicsProject.Tests.Dtos.Service
{
    public class CreateProductDtoTests
    {
        [Fact]
        public void CanCreateCreateProductDto()
        {
            var dto = new CreateProductDto();
            Assert.NotNull(dto);
        }

        [Fact]
        public void CanSetArticleAndName()
        {
            var dto = new CreateProductDto
            {
                Article = "ART-001",
                Name = "Моторное масло"
            };

            Assert.Equal("ART-001", dto.Article);
            Assert.Equal("Моторное масло", dto.Name);
        }

        [Fact]
        public void CanSetCategoryIdAndUnitId()
        {
            var categoryId = Guid.NewGuid();
            var unitId = Guid.NewGuid();
            var dto = new CreateProductDto
            {
                CategoryId = categoryId,
                UnitId = unitId
            };

            Assert.Equal(categoryId, dto.CategoryId);
            Assert.Equal(unitId, dto.UnitId);
        }

        [Fact]
        public void CanSetPrice()
        {
            var dto = new CreateProductDto { Price = 1500.50m };
            Assert.Equal(1500.50m, dto.Price);
        }

        [Fact]
        public void HasExpiryDate_DefaultValue_IsFalse()
        {
            var dto = new CreateProductDto();
            Assert.False(dto.HasExpiryDate);
        }

        [Fact]
        public void CanSetHasExpiryDate()
        {
            var dto = new CreateProductDto { HasExpiryDate = true };
            Assert.True(dto.HasExpiryDate);
        }
    }
}