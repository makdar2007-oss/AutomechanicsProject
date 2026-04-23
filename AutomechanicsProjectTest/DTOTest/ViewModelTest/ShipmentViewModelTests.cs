using Xunit;
using AutomechanicsProject.ViewModels;

public class ShipmentViewModelTests
{
    [Fact]
    public void TotalText_ReturnsFormattedString()
    {
        var vm = new ShipmentViewModel
        {
            Total = 100
        };

        var result = vm.TotalText;

        Assert.False(string.IsNullOrWhiteSpace(result));
    }

    [Fact]
    public void ProfitText_ReturnsFormattedString()
    {
        var vm = new ShipmentViewModel
        {
            Profit = 50
        };

        var result = vm.ProfitText;

        Assert.False(string.IsNullOrWhiteSpace(result));
    }

    [Fact]
    public void TotalText_ReturnsCorrectValue()
    {
        var vm = new ShipmentViewModel
        {
            Total = 100
        };

        var result = vm.TotalText;

        Assert.Contains("100", result);
    }

    [Fact]
    public void ProfitText_Negative_ReturnsCorrect()
    {
        var vm = new ShipmentViewModel
        {
            Profit = -50
        };

        var result = vm.ProfitText;

        Assert.Contains("-50", result);
    }

    [Fact]
    public void ProfitText_Decimal_ReturnsCorrect()
    {
        var vm = new ShipmentViewModel
        {
            Profit = 99.99m
        };

        var result = vm.ProfitText;

        Assert.Contains("99", result);
    }
}