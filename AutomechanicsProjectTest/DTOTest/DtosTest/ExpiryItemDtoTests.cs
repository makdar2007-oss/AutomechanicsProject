using Xunit;
using System;
using AutomechanicsProject.Dtos;

public class ExpiryItemDtoTests
{
    [Fact]
    public void NullableExpiryDate_Works()
    {
        var dto = new ExpiryItemDto
        {
            ExpiryDate = null
        };

        Assert.Null(dto.ExpiryDate);
    }

    [Fact]
    public void Properties_SetCorrectly()
    {
        var dto = new ExpiryItemDto
        {
            Balance = 5,
            DisplayText = "Масло"
        };

        Assert.Equal("Масло", dto.DisplayText);
        Assert.Equal(5, dto.Balance);
    }
}