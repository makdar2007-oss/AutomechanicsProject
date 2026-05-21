using Xunit;
using AutomechanicsProject.Classes;

namespace AutomechanicsProject.Tests.Classes
{
    public class CurrencyCodesTests
    {
        [Fact]
        public void RUB_Constant_ReturnsCorrectCode()
        {
            Assert.Equal("RUB", CurrencyCodes.RUB);
        }

        [Fact]
        public void USD_Constant_ReturnsCorrectCode()
        {
            Assert.Equal("USD", CurrencyCodes.USD);
        }

        [Fact]
        public void EUR_Constant_ReturnsCorrectCode()
        {
            Assert.Equal("EUR", CurrencyCodes.EUR);
        }
    }
}