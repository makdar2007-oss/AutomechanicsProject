using Xunit;
using System;
using AutomechanicsProject.Dtos.Service;

public class ProductListItemDtoTests
{
    [Fact]
    public void DiscountAndExpiredFlags_Work()
    {
        var dto = new ProductListItemDto
        {
            RequiresDiscount = true,
            IsExpired = false
        };

        Assert.True(dto.RequiresDiscount);
        Assert.False(dto.IsExpired);
    }
}