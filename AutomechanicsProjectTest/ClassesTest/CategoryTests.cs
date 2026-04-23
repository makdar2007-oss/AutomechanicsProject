using Xunit;
using System;
using System.Collections.Generic;
using AutomechanicsProject.Classes;

public class CategoryTests
{
    [Fact]
    public void DisplayName_WithProducts_ReturnsCorrect()
    {
        var category = new Category
        {
            Name = "Фильтры",
            Products = new List<Product>
            {
                new Product(),
                new Product()
            }
        };

        var result = category.DisplayName;

        Assert.Equal("Фильтры (товаров: 2)", result);
    }

    [Fact]
    public void DisplayName_NoProducts_ReturnsZero()
    {
        var category = new Category
        {
            Name = "Фильтры",
            Products = null
        };

        var result = category.DisplayName;

        Assert.Contains("0", result);
    }
}