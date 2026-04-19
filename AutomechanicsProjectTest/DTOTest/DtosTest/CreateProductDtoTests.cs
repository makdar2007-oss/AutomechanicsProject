using Xunit;
using System;
using AutomechanicsProject.Dtos.Service;

public class CreateProductDtoTests
{
    [Fact]
    public void Properties_SetCorrectly()
    {
        var dto = new CreateProductDto
        {
            Article = "A1",
            Name = "Масло",
            CategoryId = Guid.NewGuid(),
            UnitId = Guid.NewGuid(),
            Price = 150,
            HasExpiryDate = true
        };

        Assert.Equal("A1", dto.Article);
        Assert.True(dto.HasExpiryDate);
    }
}