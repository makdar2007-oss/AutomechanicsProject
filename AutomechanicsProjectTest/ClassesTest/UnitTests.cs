using Xunit;
using AutomechanicsProject.Classes;

public class UnitTests
{
    [Fact]
    public void DisplayName_ReturnsCorrectFormat()
    {
        var unit = new Unit
        {
            Name = "Штука",
            ShortName = "шт"
        };

        var result = unit.DisplayName;

        Assert.Equal("Штука (шт)", result);
    }

    [Fact]
    public void Properties_SetCorrectly()
    {
        var unit = new Unit
        {
            Name = "Литр",
            ShortName = "л"
        };

        Assert.Equal("Литр", unit.Name);
        Assert.Equal("л", unit.ShortName);
    }
}