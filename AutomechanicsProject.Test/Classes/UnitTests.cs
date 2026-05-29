using Xunit;
using AutomechanicsProject.Classes;

namespace AutomechanicsProject.Tests.Classes
{
    public class UnitTests
    {
        [Fact]
        public void CanCreateUnit()
        {
            var unit = new Unit();
            Assert.NotNull(unit);
        }

        [Fact]
        public void DisplayName_ReturnsFormattedName()
        {
            var unit = new Unit
            {
                Name = "Литр",
                ShortName = "л"
            };

            Assert.Equal("Литр (л)", unit.DisplayName);
        }

        [Fact]
        public void DisplayName_WhenShortNameEmpty_ReturnsNameWithEmptyParentheses()
        {
            var unit = new Unit
            {
                Name = "Штука",
                ShortName = ""
            };

            Assert.Equal("Штука ()", unit.DisplayName);
        }

        [Fact]
        public void DisplayName_WhenNameNull_DoesNotThrowException()
        {
            var unit = new Unit
            {
                Name = null,
                ShortName = "шт"
            };

            var result = unit.DisplayName;

            Assert.NotNull(result);
        }
    }
}