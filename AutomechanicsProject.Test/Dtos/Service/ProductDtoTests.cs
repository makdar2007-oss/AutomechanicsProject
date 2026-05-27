using Xunit;
using AutomechanicsProject.Dtos.Service;
using System;

namespace AutomechanicsProject.Tests.Dtos.Service
{
    public class ProductDtoTests
    {
        [Fact]
        public void CanCreateProductDto()
        {
            var dto = new ProductDto();
            Assert.NotNull(dto);
        }

        [Fact]
        public void CanSetAllProperties()
        {
            var id = Guid.NewGuid();
            var categoryId = Guid.NewGuid();
            var unitId = Guid.NewGuid();
            var expiryDate = new DateTime(2025, 12, 31);

            var dto = new ProductDto
            {
                Id = id,
                Article = "ART-001",
                Name = "Масло",
                CategoryId = categoryId,
                CategoryName = "Смазочные",
                UnitId = unitId,
                UnitName = "л",
                PurchasePrice = 1000m,
                Price = 1500m,
                Balance = 50,
                ExpiryDate = expiryDate,
                BatchNumber = "BATCH-001"
            };

            Assert.Equal(id, dto.Id);
            Assert.Equal("ART-001", dto.Article);
            Assert.Equal("Масло", dto.Name);
            Assert.Equal(categoryId, dto.CategoryId);
            Assert.Equal("Смазочные", dto.CategoryName);
            Assert.Equal(unitId, dto.UnitId);
            Assert.Equal("л", dto.UnitName);
            Assert.Equal(1000m, dto.PurchasePrice);
            Assert.Equal(1500m, dto.Price);
            Assert.Equal(50, dto.Balance);
            Assert.Equal(expiryDate, dto.ExpiryDate);
            Assert.Equal("BATCH-001", dto.BatchNumber);
        }

        [Fact]
        public void ExpiryDate_CanBeNull()
        {
            var dto = new ProductDto { ExpiryDate = null };
            Assert.Null(dto.ExpiryDate);
        }

        [Fact]
        public void BatchNumber_CanBeNull()
        {
            var dto = new ProductDto { BatchNumber = null };
            Assert.Null(dto.BatchNumber);
        }
    }
}