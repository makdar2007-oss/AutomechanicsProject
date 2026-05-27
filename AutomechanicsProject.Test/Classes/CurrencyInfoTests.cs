using Xunit;
using AutomechanicsProject.Classes;

namespace AutomechanicsProject.Tests.Classes
{
    public class CurrencyInfoTests
    {
        [Fact]
        public void CanCreateCurrencyInfo()
        {
            var currencyInfo = new CurrencyInfo();
            Assert.NotNull(currencyInfo);
        }

        [Fact]
        public void CanSetCodeAndRate()
        {
            var currencyInfo = new CurrencyInfo();
            currencyInfo.Code = "USD";
            currencyInfo.Rate = 0.016m;

            Assert.Equal("USD", currencyInfo.Code);
            Assert.Equal(0.016m, currencyInfo.Rate);
        }

        [Fact]
        public void CanSetDisplayText()
        {
            var currencyInfo = new CurrencyInfo();
            currencyInfo.DisplayText = "Доллар США";

            Assert.Equal("Доллар США", currencyInfo.DisplayText);
        }
    }
}