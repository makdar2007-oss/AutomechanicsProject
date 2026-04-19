using Xunit;
using System;
using AutomechanicsProject.Dtos.Service;

public class ProductDtoTests
{
    [Fact]
    public void Properties_SetCorrectly()
    {
        var dto = new ProductDto
        {
            Name = "Масло",
            Article = "A1",
            Balance = 3
        };

        Assert.Equal("Масло", dto.Name);
        Assert.Equal("A1", dto.Article);
        Assert.Equal(3, dto.Balance);
    }
}