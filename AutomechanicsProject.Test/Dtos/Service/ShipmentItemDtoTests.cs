using Xunit;
using AutomechanicsProject.Dtos.Service;
using System;

namespace AutomechanicsProject.Tests.Dtos.Service
{
    public class ShipmentItemDtoTests
    {
        [Fact]
        public void CanCreateShipmentItemDto()
        {
            var dto = new ShipmentItemDto();
            Assert.NotNull(dto);
        }

        [Fact]
        public void CanSetAllProperties()
        {
            var productId = Guid.NewGuid();
            var dto = new ShipmentItemDto
            {
                ProductId = productId,
                ProductName = "Масло",
                Article = "ART-001",
                Quantity = 10,
                Price = 1500m,
                PurchasePrice = 1000m
            };

            Assert.Equal(productId, dto.ProductId);
            Assert.Equal("Масло", dto.ProductName);
            Assert.Equal("ART-001", dto.Article);
            Assert.Equal(10, dto.Quantity);
            Assert.Equal(1500m, dto.Price);
            Assert.Equal(1000m, dto.PurchasePrice);
        }

        [Fact]
        public void Quantity_DefaultValue_IsZero()
        {
            var dto = new ShipmentItemDto();
            Assert.Equal(0, dto.Quantity);
        }
    }
}