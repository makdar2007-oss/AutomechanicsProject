using Xunit;
using System;
using AutomechanicsProject.Dtos.UI;

public class ComboItemDtoTests
{
    [Fact]
    public void Properties_SetCorrectly()
    {
        var dto = new ComboItemDto
        {
            Id = Guid.NewGuid(),
            Text = "Категория",
            Tooltip = "Подсказка"
        };

        Assert.Equal("Категория", dto.Text);
        Assert.Equal("Подсказка", dto.Tooltip);
    }
}