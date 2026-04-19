using Xunit;
using System;
using System.Collections.Generic;
using AutomechanicsProject.Classes;
using AutomechanicsProject.Mappers;

public class CategoryMapperTests
{
    [Fact]
    public void ToDto_ReturnsCorrectData()
    {
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Двигатель",
            Products = new List<Product>
            {
                new Product(),
                new Product()
            }
        };

        var dto = CategoryMapper.ToDto(category);

        Assert.Equal(category.Id, dto.Id);
        Assert.Equal("Двигатель", dto.Name);
        Assert.Equal(2, dto.ProductsCount);
    }

    [Fact]
    public void ToComboItem_ReturnsCorrectData()
    {
        var dto = new AutomechanicsProject.Dtos.Service.CategoryDto
        {
            Id = Guid.NewGuid(),
            Name = "Фильтры"
        };

        var combo = CategoryMapper.ToComboItem(dto);

        Assert.Equal(dto.Id, combo.Id);
        Assert.Equal("Фильтры", combo.Text);
    }
}