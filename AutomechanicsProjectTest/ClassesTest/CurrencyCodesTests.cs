using Xunit;
using AutomechanicsProject.Classes;

public class CurrencyCodesTests
{
    [Fact]
    public void Constants_AreCorrect()
    {
        Assert.Equal("RUB", CurrencyCodes.RUB);
        Assert.Equal("USD", CurrencyCodes.USD);
        Assert.Equal("EUR", CurrencyCodes.EUR);
    }
}