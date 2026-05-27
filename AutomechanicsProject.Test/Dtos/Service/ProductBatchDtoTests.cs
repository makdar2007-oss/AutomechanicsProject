using Xunit;
using AutomechanicsProject.Dtos.Service;
using System;

namespace AutomechanicsProject.Tests.Dtos.Service
{
    public class ProductBatchDtoTests
    {
        [Fact]
        public void CanCreateProductBatchDto()
        {
            var dto = new ProductBatchDto();
            Assert.NotNull(dto);
        }

        [Fact]
        public void CanSetAllProperties()
        {
            var productId = Guid.NewGuid();
            var expiryDate = new DateTime(2025, 12, 31);

            var dto = new ProductBatchDto
            {
                ProductId = productId,
                BatchNumber = "BATCH-001",
                ExpiryDate = expiryDate,
                Balance = 50,
                Price = 1500m
            };

            Assert.Equal(productId, dto.ProductId);
            Assert.Equal("BATCH-001", dto.BatchNumber);
            Assert.Equal(expiryDate, dto.ExpiryDate);
            Assert.Equal(50, dto.Balance);
            Assert.Equal(1500m, dto.Price);
        }

        [Fact]
        public void BatchNumber_CanBeNull()
        {
            var dto = new ProductBatchDto { BatchNumber = null };
            Assert.Null(dto.BatchNumber);
        }

        [Fact]
        public void ExpiryDate_CanBeNull()
        {
            var dto = new ProductBatchDto { ExpiryDate = null };
            Assert.Null(dto.ExpiryDate);
        }
    }
}