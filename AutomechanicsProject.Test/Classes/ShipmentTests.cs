using Xunit;
using AutomechanicsProject.Classes;
using System;

namespace AutomechanicsProject.Tests.Classes
{
    public class ShipmentTests
    {
        [Fact]
        public void CanCreateShipment()
        {
            var shipment = new Shipment();
            Assert.NotNull(shipment);
        }

        [Fact]
        public void CanSetTotalAmount()
        {
            var shipment = new Shipment();
            shipment.TotalAmount = 10000m;
            Assert.Equal(10000m, shipment.TotalAmount);
        }

        [Fact]
        public void ShipmentType_DefaultIsShipment()
        {
            var shipment = new Shipment();
            Assert.Equal("Shipment", shipment.ShipmentType);
        }

        [Fact]
        public void ItemsCollection_IsInitialized()
        {
            var shipment = new Shipment();
            Assert.NotNull(shipment.Items);
        }
    }
}