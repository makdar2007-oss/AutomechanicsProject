using Xunit;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Classes;
using System.Linq;

namespace AutomechanicsProject.Tests.Helpers
{
    public class CurrencyHelperTests
    {
        [Fact]
        public void GetFallbackRates_ReturnsDictionary()
        {
            var rates = CurrencyHelper.GetFallbackRates();

            Assert.NotNull(rates);
            Assert.Contains("RUB", rates.Keys);
            Assert.Contains("USD", rates.Keys);
            Assert.Contains("EUR", rates.Keys);
            Assert.Equal(1.00m, rates["RUB"]);
        }

        [Fact]
        public void GetFallbackCurrencies_ReturnsList()
        {
            var currencies = CurrencyHelper.GetFallbackCurrencies();

            Assert.NotNull(currencies);
            Assert.NotEmpty(currencies);
            Assert.Contains(currencies, c => c.Code == "RUB");
            Assert.Contains(currencies, c => c.Code == "USD");
        }

        [Fact]
        public void GetCurrencyName_ReturnsRussianNameForRUB()
        {
            var name = CurrencyHelper.GetCurrencyName("RUB");
            Assert.Equal("Российский рубль", name);
        }

        [Fact]
        public void GetCurrencyName_ReturnsEnglishNameForUSD()
        {
            var name = CurrencyHelper.GetCurrencyName("USD");
            Assert.Equal("Доллар США", name);
        }

        [Fact]
        public void GetCurrencyName_ForUnknownCode_ReturnsCode()
        {
            var name = CurrencyHelper.GetCurrencyName("UNKNOWN");
            Assert.Equal("UNKNOWN", name);
        }

        [Fact]
        public void ConvertToRUB_WithValidRate_ConvertsCorrectly()
        {
            var result = CurrencyHelper.ConvertToRUB(50m, 0.016m);
            Assert.Equal(3125m, result);
        }

        [Fact]
        public void ConvertToRUB_WithZeroRate_ReturnsZero()
        {
            var result = CurrencyHelper.ConvertToRUB(100m, 0m);
            Assert.Equal(0m, result);
        }

        [Fact]
        public void ConvertFromRUB_WithValidRate_ConvertsCorrectly()
        {
            var result = CurrencyHelper.ConvertFromRUB(1000m, 0.016m);
            Assert.Equal(16m, result);
        }

        [Fact]
        public void ConvertFromRUB_WithZeroRate_ReturnsZero()
        {
            var result = CurrencyHelper.ConvertFromRUB(100m, 0m);
            Assert.Equal(0m, result);
        }
    }
}