using Xunit;
using AutomechanicsProject.Dtos;
using System;

namespace AutomechanicsProject.Tests.Dtos.Service
{
    public class ExpiryItemDtoTests
    {
        [Fact]
        public void CanCreateExpiryItemDto()
        {
            var dto = new ExpiryItemDto();
            Assert.NotNull(dto);
        }

        [Fact]
        public void CanSetProductIdAndDisplayText()
        {
            var productId = Guid.NewGuid();
            var dto = new ExpiryItemDto
            {
                ProductId = productId,
                DisplayText = "Масло (до 31.12.2025)"
            };

            Assert.Equal(productId, dto.ProductId);
            Assert.Equal("Масло (до 31.12.2025)", dto.DisplayText);
        }

        [Fact]
        public void CanSetExpiryDateAndBalance()
        {
            var expiryDate = new DateTime(2025, 12, 31);
            var dto = new ExpiryItemDto
            {
                ExpiryDate = expiryDate,
                Balance = 100
            };

            Assert.Equal(expiryDate, dto.ExpiryDate);
            Assert.Equal(100, dto.Balance);
        }

        [Fact]
        public void ExpiryDate_CanBeNull()
        {
            var dto = new ExpiryItemDto { ExpiryDate = null };
            Assert.Null(dto.ExpiryDate);
        }
    }
}