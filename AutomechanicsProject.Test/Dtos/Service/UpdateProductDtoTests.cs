using Xunit;
using AutomechanicsProject.Dtos.Service;
using System;

namespace AutomechanicsProject.Tests.Dtos.Service
{
    public class UpdateProductDtoTests
    {
        [Fact]
        public void CanCreateUpdateProductDto()
        {
            var dto = new UpdateProductDto();
            Assert.NotNull(dto);
        }

        [Fact]
        public void CanSetAllProperties()
        {
            var id = Guid.NewGuid();
            var categoryId = Guid.NewGuid();
            var unitId = Guid.NewGuid();

            var dto = new UpdateProductDto
            {
                Id = id,
                Article = "ART-001",
                Name = "Масло",
                CategoryId = categoryId,
                UnitId = unitId,
                PurchasePrice = 1200m
            };

            Assert.Equal(id, dto.Id);
            Assert.Equal("ART-001", dto.Article);
            Assert.Equal("Масло", dto.Name);
            Assert.Equal(categoryId, dto.CategoryId);
            Assert.Equal(unitId, dto.UnitId);
            Assert.Equal(1200m, dto.PurchasePrice);
        }

        [Fact]
        public void PurchasePrice_CanBeZero()
        {
            var dto = new UpdateProductDto { PurchasePrice = 0 };
            Assert.Equal(0, dto.PurchasePrice);
        }
    }
}