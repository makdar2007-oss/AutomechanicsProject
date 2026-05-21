using Xunit;
using AutomechanicsProject.Classes;

namespace AutomechanicsProject.Tests.Classes
{
    public class CurrencyManagerTests
    {
        [Fact]
        public void DefaultCurrencyIsRUB()
        {
            Assert.Equal(CurrencyCodes.RUB, CurrencyManager.SelectedCurrency);
            Assert.Equal(1m, CurrencyManager.CurrentRate);
        }

        [Fact]
        public void CanChangeCurrency()
        {
            CurrencyManager.SetCurrency(CurrencyCodes.USD, 0.016m);

            Assert.Equal(CurrencyCodes.USD, CurrencyManager.SelectedCurrency);
            Assert.Equal(0.016m, CurrencyManager.CurrentRate);

            CurrencyManager.SetCurrency(CurrencyCodes.RUB, 1m);
        }

        [Fact]
        public void ConvertPrice_WithRUB_DoesNotChangePrice()
        {
            CurrencyManager.SetCurrency(CurrencyCodes.RUB, 1m);

            decimal result = CurrencyManager.ConvertPrice(100m);

            Assert.Equal(100m, result);
        }

        [Fact]
        public void ConvertPrice_WithUSD_ConvertsPrice()
        {
            CurrencyManager.SetCurrency(CurrencyCodes.USD, 0.016m);

            decimal result = CurrencyManager.ConvertPrice(100m);

            Assert.Equal(1.6m, result);

            CurrencyManager.SetCurrency(CurrencyCodes.RUB, 1m);
        }

        [Fact]
        public void ConvertPriceToRub_ReturnsPriceInRubles()
        {
            decimal result = CurrencyManager.ConvertPriceToRub(50m, CurrencyCodes.USD, 0.016m);

            Assert.Equal(3125m, result);
        }

        [Fact]
        public void ConvertPriceToRub_WithZeroRate_ReturnsZero()
        {
            decimal result = CurrencyManager.ConvertPriceToRub(100m, CurrencyCodes.USD, 0m);

            Assert.Equal(0m, result);
        }
    }
}