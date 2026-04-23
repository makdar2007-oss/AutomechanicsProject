using Xunit;
using System;
using AutomechanicsProject.ViewModels;

public class ProductComboViewModelTests
{
    [Fact]
    public void Properties_SetAndGet_WorkCorrectly()
    {
        var vm = new ProductComboViewModel
        {
            Id = Guid.NewGuid(),
            Text = "Test",
            Article = "A1",
            Name = "Масло",
            Price = 100,
            Balance = 10,
            UnitName = "шт"
        };

        Assert.Equal("Test", vm.Text);
        Assert.Equal("A1", vm.Article);
        Assert.Equal(100, vm.Price);
        Assert.Equal(10, vm.Balance);
    }

    [Fact]
    public void Properties_SetAndGet_AllFields()
    {
        var id = Guid.NewGuid();

        var vm = new ProductComboViewModel
        {
            Id = id,
            Text = "Test",
            Article = "A1",
            Name = "Масло",
            Price = 100,
            Balance = 10,
            UnitName = "шт"
        };

        Assert.Equal(id, vm.Id);
        Assert.Equal("Test", vm.Text);
        Assert.Equal("A1", vm.Article);
        Assert.Equal("Масло", vm.Name);
        Assert.Equal(100, vm.Price);
        Assert.Equal(10, vm.Balance);
        Assert.Equal("шт", vm.UnitName);
    }

    [Fact]
    public void DefaultValues_AreCorrect()
    {
        var vm = new ProductComboViewModel();

        Assert.Equal(Guid.Empty, vm.Id);
        Assert.Null(vm.Text);
        Assert.Null(vm.Article);
        Assert.Null(vm.Name);
        Assert.Equal(0, vm.Price);
        Assert.Equal(0, vm.Balance);
        Assert.Null(vm.UnitName);
    }

    [Fact]
    public void NegativeValues_AreAssigned()
    {
        var vm = new ProductComboViewModel
        {
            Price = -100,
            Balance = -5
        };

        Assert.Equal(-100, vm.Price);
        Assert.Equal(-5, vm.Balance);
    }
}