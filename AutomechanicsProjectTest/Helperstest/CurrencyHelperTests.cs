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
        public void ConvertFromRUB_WithZero_ReturnsZero()
        {
            var result = CurrencyHelper.ConvertFromRUB(0m, 0.5m);

            Assert.Equal(0m, result);
        }

        [Fact]
        public void ConvertToRUB_WithNegative_ReturnsCorrect()
        {
            var result = CurrencyHelper.ConvertToRUB(-10m, 0.5m);

            Assert.Equal(-20m, result);
        }
    }
}