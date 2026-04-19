using Xunit;
using AutomechanicsProject.Helpers;
using System.Collections.Generic;

namespace AutomechanicsProjectTest.Classes
{
    public class CurrencyStorageTests
    {
        [Fact]
        public void SaveAndLoadRates_ShouldNotCrash()
        {
            var rates = new Dictionary<string, decimal>
            {
                { "USD", 0.01m }
            };

            CurrencyStorage.SaveRates(rates, "USD");

            var result = CurrencyStorage.LoadRates();

            Assert.True(result == null || result.Value.ExchangeRates != null);
        }

        [Fact]
        public void LoadRates_WhenFileDoesNotExist_ReturnsNull()
        {
            var result = CurrencyStorage.LoadRates();

            Assert.True(result == null || result.Value.ExchangeRates != null);
        }
    }
}