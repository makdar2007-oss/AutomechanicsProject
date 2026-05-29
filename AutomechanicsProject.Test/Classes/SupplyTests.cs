using Xunit;
using AutomechanicsProject.Classes;
using System;

namespace AutomechanicsProject.Tests.Classes
{
    public class SupplyTests
    {
        [Fact]
        public void CanCreateSupply()
        {
            var supply = new Supply();

            Assert.NotNull(supply);
        }

        [Fact]
        public void CurrencyCode_DefaultValue_IsRUB()
        {
            var supply = new Supply();

            Assert.Equal(CurrencyCodes.RUB, supply.CurrencyCode);
        }

        [Fact]
        public void ExchangeRate_DefaultValue_IsOne()
        {
            var supply = new Supply();

            Assert.Equal(1.00m, supply.ExchangeRate);
        }

        [Fact]
        public void PositionsCollection_IsInitialized()
        {
            var supply = new Supply();

            Assert.NotNull(supply.Positions);
        }
    }
}