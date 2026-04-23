using Xunit;
using AutomechanicsProject.Helpers;

namespace AutomechanicsProjectTest.Classes
{
    public class CurrencyHelperTests
    {
        [Fact]
        public void ConvertToRUB_ReturnsCorrect()
        {
            var result = CurrencyHelper.ConvertToRUB(10m, 0.5m);
            Assert.Equal(20m, result);
        }

        [Fact]
        public void ConvertFromRUB_ReturnsCorrect()
        {
            var result = CurrencyHelper.ConvertFromRUB(100m, 0.5m);
            Assert.Equal(50m, result);
        }

        [Fact]
        public void ConvertToRUB_ZeroRate_ReturnsZero()
        {
            var result = CurrencyHelper.ConvertToRUB(100m, 0m);
            Assert.Equal(0m, result);
        }

        [Fact]
        public void ConvertFromRUB_ZeroRate_ReturnsZero()
        {
            var result = CurrencyHelper.ConvertFromRUB(100m, 0m);
            Assert.Equal(0m, result);
        }

        [Fact]
        public void ConvertToRUB_Negative_ReturnsCorrect()
        {
            var result = CurrencyHelper.ConvertToRUB(-10m, 0.5m);
            Assert.Equal(-20m, result);
        }

        [Fact]
        public void GetFallbackRates_ContainsRUB()
        {
            var rates = CurrencyHelper.GetFallbackRates();
            Assert.True(rates.ContainsKey("RUB"));
        }

        [Fact]
        public void GetCurrencyName_Unknown_ReturnsCode()
        {
            var result = CurrencyHelper.GetCurrencyName("XXX");
            Assert.Equal("XXX", result);
        }

        [Fact]
        public void GetCurrencyName_KnownCurrency_ReturnsName()
        {
            var result = CurrencyHelper.GetCurrencyName("USD");

            Assert.False(string.IsNullOrWhiteSpace(result));
        }

        [Fact]
        public void GetFallbackRates_ContainsMainCurrencies()
        {
            var rates = CurrencyHelper.GetFallbackRates();

            Assert.True(rates.ContainsKey("USD"));
            Assert.True(rates.ContainsKey("EUR"));
            Assert.True(rates["RUB"] == 1m);
        }

        [Fact]
        public void ConvertToRUB_ThenBack_ReturnsOriginal()
        {
            var amount = 100m;
            var rate = 0.5m;

            var rub = CurrencyHelper.ConvertToRUB(amount, rate);
            var back = CurrencyHelper.ConvertFromRUB(rub, rate);

            Assert.Equal(amount, back);
        }

        [Fact]
        public void ConvertToRUB_LargeNumbers_Works()
        {
            var result = CurrencyHelper.ConvertToRUB(1_000_000m, 0.5m);

            Assert.Equal(2_000_000m, result);
        }

        [Fact]
        public void ConvertToRUB_RateOne_ReturnsSame()
        {
            var result = CurrencyHelper.ConvertToRUB(100m, 1m);

            Assert.Equal(100m, result);
        }
    }
}