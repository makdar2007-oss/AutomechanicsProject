using Xunit;
using AutomechanicsProject.Classes;
using System;

namespace AutomechanicsProject.Tests.Classes
{
    public class SupplyPositionTests
    {
        [Fact]
        public void CanCreateSupplyPosition()
        {
            var position = new SupplyPosition();
            Assert.NotNull(position);
        }

        [Fact]
        public void CanSetQuantityAndPrice()
        {
            var position = new SupplyPosition();
            position.Quantity = 10;
            position.Price = 500.50m;

            Assert.Equal(10, position.Quantity);
            Assert.Equal(500.50m, position.Price);
        }

        [Fact]
        public void ExpiryDate_CanBeSetToNull()
        {
            var position = new SupplyPosition();
            position.ExpiryDate = null;

            Assert.Null(position.ExpiryDate);
        }

        [Fact]
        public void ExpiryDate_CanBeSetToDate()
        {
            var position = new SupplyPosition();
            var date = new DateTime(2025, 12, 31);
            position.ExpiryDate = date;

            Assert.Equal(date, position.ExpiryDate);
        }
    }
}