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

    [Fact]
    public void DisplayName_OnlyArticle_ReturnsCorrect()
    {
        var vm = new ProductViewModel
        {
            Article = "A1"
        };

        var result = vm.DisplayName;

        Assert.Contains("A1", result);
    }

    [Fact]
    public void DisplayName_OnlyName_ReturnsCorrect()
    {
        var vm = new ProductViewModel
        {
            Name = "Масло"
        };

        var result = vm.DisplayName;

        Assert.Contains("Масло", result);
    }

    [Fact]
    public void BalanceWithUnit_NoUnit_ReturnsCorrect()
    {
        var vm = new ProductViewModel
        {
            Balance = 10
        };

        var result = vm.BalanceWithUnit;

        Assert.Contains("10", result);
    }

    [Fact]
    public void BalanceWithUnit_Negative()
    {
        var vm = new ProductViewModel
        {
            Balance = -5,
            UnitName = "шт"
        };

        var result = vm.BalanceWithUnit;

        Assert.Equal("-5 шт", result);
    }
}