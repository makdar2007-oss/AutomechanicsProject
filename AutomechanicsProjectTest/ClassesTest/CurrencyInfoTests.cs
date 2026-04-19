using Xunit;
using AutomechanicsProject.Classes;

public class CurrencyInfoTests
{
    [Fact]
    public void Properties_SetCorrectly()
    {
        var info = new CurrencyInfo
        {
            Code = "USD",
            Rate = 100,
            DisplayText = "Доллар"
        };

        Assert.Equal("USD", info.Code);
        Assert.Equal(100, info.Rate);
        Assert.Equal("Доллар", info.DisplayText);
    }
}