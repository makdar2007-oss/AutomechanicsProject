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
}