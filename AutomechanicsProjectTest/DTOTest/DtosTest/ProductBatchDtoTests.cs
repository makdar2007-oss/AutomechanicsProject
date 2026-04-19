using Xunit;
using System;
using AutomechanicsProject.Dtos.Service;

public class ProductBatchDtoTests
{
    [Fact]
    public void Properties_SetCorrectly()
    {
        var dto = new ProductBatchDto
        {
            BatchNumber = "B1",
            Balance = 10,
            Price = 200
        };

        Assert.Equal("B1", dto.BatchNumber);
        Assert.Equal(10, dto.Balance);
        Assert.Equal(200, dto.Price);
    }
}