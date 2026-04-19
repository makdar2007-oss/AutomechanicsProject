using Xunit;
using System;
using AutomechanicsProject.Dtos.Service;

public class CategoryDtoTests
{
    [Fact]
    public void Properties_SetCorrectly()
    {
        var dto = new CategoryDto
        {
            Id = Guid.NewGuid(),
            Name = "Фильтры",
            ProductsCount = 5
        };

        Assert.Equal("Фильтры", dto.Name);
        Assert.Equal(5, dto.ProductsCount);
    }
}