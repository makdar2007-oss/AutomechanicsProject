using Xunit;
using System;
using AutomechanicsProject.Dtos.Service;

public class RecipientDtoTests
{
    [Fact]
    public void Properties_SetCorrectly()
    {
        var dto = new RecipientDto
        {
            CompanyName = "ООО Тест"
        };

        Assert.Equal("ООО Тест", dto.CompanyName);
    }
}