using Xunit;
using System;
using AutomechanicsProject.Dtos.Service;

public class ShipmentResultDtoTests
{
    [Fact]
    public void SuccessFlag_Works()
    {
        var dto = new ShipmentResultDto
        {
            Success = true,
            ItemsCount = 3
        };

        Assert.True(dto.Success);
        Assert.Equal(3, dto.ItemsCount);
    }
}