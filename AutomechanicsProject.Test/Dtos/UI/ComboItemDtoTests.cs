using Xunit;
using AutomechanicsProject.Dtos.UI;
using System;

namespace AutomechanicsProject.Tests.Dtos.UI
{
    public class ComboItemDtoTests
    {
        [Fact]
        public void CanCreateComboItemDto()
        {
            var dto = new ComboItemDto();
            Assert.NotNull(dto);
        }

        [Fact]
        public void CanSetIdAndText()
        {
            var id = Guid.NewGuid();
            var dto = new ComboItemDto
            {
                Id = id,
                Text = "Автозапчасти"
            };

            Assert.Equal(id, dto.Id);
            Assert.Equal("Автозапчасти", dto.Text);
        }

        [Fact]
        public void CanSetTooltip()
        {
            var dto = new ComboItemDto
            {
                Tooltip = "Дополнительная информация"
            };

            Assert.Equal("Дополнительная информация", dto.Tooltip);
        }

        [Fact]
        public void Tooltip_CanBeNull()
        {
            var dto = new ComboItemDto { Tooltip = null };
            Assert.Null(dto.Tooltip);
        }

        [Fact]
        public void Tooltip_CanBeEmpty()
        {
            var dto = new ComboItemDto { Tooltip = "" };
            Assert.Equal("", dto.Tooltip);
        }

        [Fact]
        public void Text_CanBeNull()
        {
            var dto = new ComboItemDto { Text = null };
            Assert.Null(dto.Text);
        }
    }
}