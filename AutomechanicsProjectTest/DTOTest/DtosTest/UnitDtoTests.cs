using Xunit;
using System;
using AutomechanicsProject.Dtos.Service;

public class UnitDtoTests
{
    [Fact]
    public void Properties_SetCorrectly()
    {
        var dto = new UnitDto
        {
            Name = "Штука",
            ShortName = "шт"
        };

        Assert.Equal("Штука", dto.Name);
        Assert.Equal("шт", dto.ShortName);
    }
}