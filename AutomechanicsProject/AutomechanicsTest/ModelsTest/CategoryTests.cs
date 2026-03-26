using Xunit;
using AutomechanicsProject.Classes;
using System.Collections.Generic;

public class CategoryTests
{
    [Fact]
    public void Category_ShouldStoreName()
    {
        var category = new Category { Name = "Масла" };
        Assert.Equal("Масла", category.Name);
    }

    [Fact]
    public void Category_NameCanBeNull()
    {
        var category = new Category { Name = null };
        Assert.Null(category.Name);
    }

    [Fact]
    public void Category_ShouldStoreProducts()
    {
        var products = new List<Product>
        {
            new Product { Name = "Товар1" },
            new Product { Name = "Товар2" }
        };

        var category = new Category { Products = products };

        Assert.Equal(2, category.Products.Count);
    }

    [Fact]
    public void Category_ProductsCanBeNull()
    {
        var category = new Category { Products = null };
        Assert.Null(category.Products);
    }

    [Fact]
    public void DisplayName_ShouldShowZero_WhenProductsNull()
    {
        var category = new Category
        {
            Name = "Масла",
            Products = null
        };

        Assert.Equal("Масла (товаров: 0)", category.DisplayName);
    }

    [Fact]
    public void DisplayName_ShouldShowZero_WhenProductsEmpty()
    {
        var category = new Category
        {
            Name = "Масла",
            Products = new List<Product>()
        };

        Assert.Equal("Масла (товаров: 0)", category.DisplayName);
    }

    [Fact]
    public void DisplayName_ShouldShowCorrectCount()
    {
        var category = new Category
        {
            Name = "Масла",
            Products = new List<Product>
            {
                new Product(),
                new Product(),
                new Product()
            }
        };

        Assert.Equal("Масла (товаров: 3)", category.DisplayName);
    }

    [Fact]
    public void DisplayName_ShouldWork_WhenNameIsNull()
    {
        var category = new Category
        {
            Name = null,
            Products = new List<Product>()
        };

        Assert.Equal(" (товаров: 0)", category.DisplayName);
    }

    [Fact]
    public void Category_ShouldAllowAddingProducts()
    {
        var category = new Category
        {
            Products = new List<Product>()
        };

        category.Products.Add(new Product());

        Assert.Single(category.Products);
    }

    [Fact]
    public void Category_ShouldAllowChangingName()
    {
        var category = new Category { Name = "Старое" };

        category.Name = "Новое";

        Assert.Equal("Новое", category.Name);
    }

    [Fact]
    public void Category_ShouldBeCreated_NotNull()
    {
        var category = new Category();

        Assert.NotNull(category);
    }

    [Fact]
    public void Category_DisplayName_ShouldUpdate_WhenProductsChange()
    {
        var category = new Category
        {
            Name = "Масла",
            Products = new List<Product>()
        };

        category.Products.Add(new Product());

        Assert.Equal("Масла (товаров: 1)", category.DisplayName);
    }

    [Fact]
    public void Category_DisplayName_ShouldUpdate_WhenNameChanges()
    {
        var category = new Category
        {
            Name = "Старое",
            Products = new List<Product>()
        };

        category.Name = "Новое";

        Assert.Equal("Новое (товаров: 0)", category.DisplayName);
    }

    [Fact]
    public void Category_ShouldStoreId()
    {
        var category = new Category { Id = System.Guid.NewGuid() };

        Assert.NotEqual(System.Guid.Empty, category.Id);
    }

    [Fact]
    public void Category_ShouldHandleLargeProductList()
    {
        var products = new List<Product>();

        for (int i = 0; i < 100; i++)
            products.Add(new Product());

        var category = new Category
        {
            Name = "Большая категория",
            Products = products
        };

        Assert.Equal("Большая категория (товаров: 100)", category.DisplayName);
    }
}