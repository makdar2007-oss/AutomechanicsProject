using Xunit;
using AutomechanicsProject.ViewModels;

public class ProductViewModelTests
{
    [Fact]
    public void DisplayName_ReturnsCorrectFormat()
    {
        var vm = new ProductViewModel
        {
            Article = "A1",
            Name = "Масло"
        };

        var result = vm.DisplayName;

        Assert.Equal("A1 - Масло", result);
    }

    [Fact]
    public void BalanceWithUnit_ReturnsCorrectFormat()
    {
        var vm = new ProductViewModel
        {
            Balance = 10,
            UnitName = "шт"
        };

        var result = vm.BalanceWithUnit;

        Assert.Equal("10 шт", result);
    }
}