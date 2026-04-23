using Xunit;
using AutomechanicsProject.Classes;

namespace AutomechanicsProjectTest.Classes
{
    public class CurrencyManagerTests
    {
        [Fact]
        public void SetCurrency_ChangesValues()
        {
            CurrencyManager.SetCurrency("USD", 100);

            Assert.Equal("USD", CurrencyManager.SelectedCurrency);
            Assert.Equal(100, CurrencyManager.CurrentRate);
        }

        [Fact]
        public void ConvertPrice_RUB_ReturnsSame()
        {
            CurrencyManager.SetCurrency("RUB", 1);

            var result = CurrencyManager.ConvertPrice(100);

            Assert.Equal(100, result);
        }

        [Fact]
        public void ConvertPrice_OtherCurrency_Multiplies()
        {
            CurrencyManager.SetCurrency("USD", 2);

            var result = CurrencyManager.ConvertPrice(100);

            Assert.Equal(200, result);
        }

        [Fact]
        public void ConvertPrice_WithZero_ReturnsZero()
        {
            CurrencyManager.SetCurrency(CurrencyCodes.USD, 0.5m);

            var result = CurrencyManager.ConvertPrice(0m);

            Assert.Equal(0m, result);
        }

        [Fact]
        public void ConvertPrice_WithNegativeValue_StillConverts()
        {
            CurrencyManager.SetCurrency(CurrencyCodes.USD, 0.5m);

            var result = CurrencyManager.ConvertPrice(-100m);

            Assert.Equal(-50m, result);
        }

        [Fact]
        public void ConvertPrice_MultipleCalls_UsesLastRate()
        {
            CurrencyManager.SetCurrency(CurrencyCodes.USD, 0.5m);
            CurrencyManager.SetCurrency(CurrencyCodes.USD, 0.2m);

            var result = CurrencyManager.ConvertPrice(100m);

            Assert.Equal(20m, result);
        }

        [Fact]
        public void ConvertPriceToRub_RUB_ReturnsSame()
        {
            var result = CurrencyManager.ConvertPriceToRub(100, "RUB", 1);

            Assert.Equal(100, result);
        }

        [Fact]
        public void ConvertPriceToRub_InvalidRate_ReturnsZero()
        {
            var result = CurrencyManager.ConvertPriceToRub(100, "USD", 0);

            Assert.Equal(0, result);
        }

        [Fact]
        public void ConvertPriceToRub_Normal_ReturnsDivided()
        {
            var result = CurrencyManager.ConvertPriceToRub(200, "USD", 2);

            Assert.Equal(100, result);
        }

        [Fact]
        public void ConvertPriceToRub_WithZeroRate_ShouldNotCrash()
        {
            var result = CurrencyManager.ConvertPriceToRub(100m, CurrencyCodes.USD, 0m);

            Assert.Equal(0m, result);
        }
    }
}