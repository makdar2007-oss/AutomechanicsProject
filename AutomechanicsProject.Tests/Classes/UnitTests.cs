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
        public void CanSetUnitName()
        {
            var unit = new Unit();
            unit.Name = "Килограмм";

            Assert.Equal("Килограмм", unit.Name);
        }

        [Fact]
        public void CanSetShortName()
        {
            var unit = new Unit();
            unit.ShortName = "кг";

            Assert.Equal("кг", unit.ShortName);
        }

        [Fact]
        public void DisplayNameShowsFullName()
        {
            var unit = new Unit();
            unit.Name = "Литр";
            unit.ShortName = "л";

            Assert.Equal("Литр (л)", unit.DisplayName);
        }
    }
}