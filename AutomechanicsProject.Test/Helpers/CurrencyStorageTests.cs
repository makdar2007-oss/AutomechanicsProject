using Xunit;
using AutomechanicsProject.Helpers;
using System.Collections.Generic;

namespace AutomechanicsProject.Tests.Helpers
{
    public class CurrencyStorageTests
    {
        [Fact]
        public void SaveRates_DoesNotThrowException()
        {
            var rates = new Dictionary<string, decimal>
            {
                { "RUB", 1.00m },
                { "USD", 0.016m }
            };

            var exception = Record.Exception(() => CurrencyStorage.SaveRates(rates, "USD"));
            Assert.Null(exception);
        }
    }
}