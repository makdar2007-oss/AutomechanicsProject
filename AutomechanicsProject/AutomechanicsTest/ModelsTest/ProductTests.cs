using Xunit;
using AutomechanicsProject.Classes;
using System;

public class ProductTests
{
    [Fact]
    public void Product_ShouldStoreName()
    {
        var product = new Product { Name = "Фильтр" };
        Assert.Equal("Фильтр", product.Name);
    }

    [Fact]
    public void Product_ShouldStoreArticle()
    {
        var product = new Product { Article = "A123" };
        Assert.Equal("A123", product.Article);
    }

    [Fact]
    public void Product_ShouldStorePrice()
    {
        var product = new Product { Price = 100 };
        Assert.Equal(100, product.Price);
    }

    [Fact]
    public void Product_ShouldStoreBalance()
    {
        var product = new Product { Balance = 10 };
        Assert.Equal(10, product.Balance);
    }

    [Fact]
    public void Product_DefaultUnit_ShouldBePieces()
    {
        var product = new Product();
        Assert.Equal("шт", product.Unit);
    }

    [Fact]
    public void Product_ShouldAllowChangingUnit()
    {
        var product = new Product { Unit = "кг" };
        Assert.Equal("кг", product.Unit);
    }

    [Fact]
    public void Product_ShouldStoreCategoryId()
    {
        var id = Guid.NewGuid();
        var product = new Product { CategoryId = id };

        Assert.Equal(id, product.CategoryId);
    }

    [Fact]
    public void Product_ShouldHaveCategory()
    {
        var category = new Category { Name = "Масла" };
        var product = new Product { Category = category };

        Assert.Equal("Масла", product.Category.Name);
    }

    [Fact]
    public void Product_ShouldAllowChangingCategory()
    {
        var product = new Product
        {
            Category = new Category { Name = "Старое" }
        };

        product.Category = new Category { Name = "Новое" };

        Assert.Equal("Новое", product.Category.Name);
    }

    [Fact]
    public void Product_ShouldStoreZeroPrice()
    {
        var product = new Product { Price = 0 };
        Assert.Equal(0, product.Price);
    }

    [Fact]
    public void Product_ShouldStoreLargePrice()
    {
        var product = new Product { Price = 999999 };
        Assert.Equal(999999, product.Price);
    }

    [Fact]
    public void Product_ShouldStoreZeroBalance()
    {
        var product = new Product { Balance = 0 };
        Assert.Equal(0, product.Balance);
    }

    [Fact]
    public void Product_ShouldCreateWithUniqueId()
    {
        var product = new Product { Id = Guid.NewGuid() };

        Assert.NotEqual(Guid.Empty, product.Id);
    }

    [Fact]
    public void Product_ShouldStoreDecimalPrice()
    {
        var product = new Product { Price = 99.99m };
        Assert.Equal(99.99m, product.Price);
    }

    [Fact]
    public void Product_ShouldUpdateBalance()
    {
        var product = new Product { Balance = 5 };
        product.Balance += 10;

        Assert.Equal(15, product.Balance);
    }
}