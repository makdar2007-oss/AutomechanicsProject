using Xunit;
using System;
using AutomechanicsProject.Classes;

public class ProductTests
{
    [Fact]
    public void Properties_SetCorrectly()
    {
        var product = new Product
        {
            Article = "A1",
            Name = "Масло",
            Price = 100,
            Balance = 10
        };

        Assert.Equal("A1", product.Article);
        Assert.Equal("Масло", product.Name);
        Assert.Equal(100, product.Price);
        Assert.Equal(10, product.Balance);
    }

    [Fact]
    public void ExpiryDate_CanBeNull()
    {
        var product = new Product
        {
            ExpiryDate = null
        };

        Assert.Null(product.ExpiryDate);
    }

    [Fact]
    public void NegativeBalance_Works()
    {
        var product = new Product { Balance = -5 };

        Assert.Equal(-5, product.Balance);
    }
}