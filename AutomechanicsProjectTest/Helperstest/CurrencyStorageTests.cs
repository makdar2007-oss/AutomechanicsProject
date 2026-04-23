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
        public void SaveAndLoadRates_ReturnsSameCurrency()
        {
            var rates = new Dictionary<string, decimal>
    {
        { "USD", 0.01m }
    };

            CurrencyStorage.SaveRates(rates, "USD");

            var result = CurrencyStorage.LoadRates();

            Assert.NotNull(result);
            Assert.Equal("USD", result.Value.LastSelectedCurrency);
        }

        [Fact]
        public void SaveAndLoadRates_PreservesRates()
        {
            var rates = new Dictionary<string, decimal>    {
        { "USD", 0.01m },
        { "EUR", 0.02m }
    };

            CurrencyStorage.SaveRates(rates, "USD");

            var result = CurrencyStorage.LoadRates();

            Assert.NotNull(result);
            Assert.True(result.Value.ExchangeRates.ContainsKey("USD"));
            Assert.Equal(0.01m, result.Value.ExchangeRates["USD"]);
        }

        [Fact]
        public void SaveRates_Overwrite_Works()
        {
            var rates1 = new Dictionary<string, decimal>
    {
        { "USD", 0.01m }
    };

            var rates2 = new Dictionary<string, decimal>
    {
        { "USD", 0.02m }
    };

            CurrencyStorage.SaveRates(rates1, "USD");
            CurrencyStorage.SaveRates(rates2, "USD");

            var result = CurrencyStorage.LoadRates();

            Assert.Equal(0.02m, result.Value.ExchangeRates["USD"]);
        }
    }
}