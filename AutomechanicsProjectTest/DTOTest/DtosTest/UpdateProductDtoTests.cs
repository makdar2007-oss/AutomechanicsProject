using Xunit;
using System;
using AutomechanicsProject.Dtos.Service;

public class UpdateProductDtoTests
{
    [Fact]
    public void Properties_SetCorrectly()
    {
        var dto = new UpdateProductDto
        {
            Name = "Фильтр",
            PurchasePrice = 300
        };

        Assert.Equal("Фильтр", dto.Name);
        Assert.Equal(300, dto.PurchasePrice);
    }
}