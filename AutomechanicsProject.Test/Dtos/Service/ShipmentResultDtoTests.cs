using Xunit;
using AutomechanicsProject.Dtos.Service;
using System;

namespace AutomechanicsProject.Tests.Dtos.Service
{
    public class ShipmentResultDtoTests
    {
        [Fact]
        public void CanCreateShipmentResultDto()
        {
            var dto = new ShipmentResultDto();
            Assert.NotNull(dto);
        }

        [Fact]
        public void Success_DefaultValue_IsFalse()
        {
            var dto = new ShipmentResultDto();
            Assert.False(dto.Success);
        }

        [Fact]
        public void CanSetSuccessAndShipmentId()
        {
            var shipmentId = Guid.NewGuid();
            var dto = new ShipmentResultDto
            {
                Success = true,
                ShipmentId = shipmentId,
                TotalAmount = 15000m,
                ItemsCount = 3
            };

            Assert.True(dto.Success);
            Assert.Equal(shipmentId, dto.ShipmentId);
            Assert.Equal(15000m, dto.TotalAmount);
            Assert.Equal(3, dto.ItemsCount);
        }

        [Fact]
        public void CanSetErrorMessage()
        {
            var dto = new ShipmentResultDto
            {
                Success = false,
                ErrorMessage = "Ошибка при отгрузке"
            };

            Assert.Equal("Ошибка при отгрузке", dto.ErrorMessage);
        }

        [Fact]
        public void ErrorMessage_CanBeNull()
        {
            var dto = new ShipmentResultDto { ErrorMessage = null };
            Assert.Null(dto.ErrorMessage);
        }
    }
}