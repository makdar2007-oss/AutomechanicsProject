using Xunit;
using AutomechanicsProject.Classes;

namespace AutomechanicsProjectTest.Classes
{
    public class RoleTests_Extended
    {
        [Fact]
        public void FromType_UnknownType_ReturnsDefault()
        {
            var role = Role.FromType((RoleType)999);

            Assert.NotNull(role);
        }

        [Fact]
        public void IsAdministrator_WhenFalse_ReturnsFalse()
        {
            var role = new Role { Position = "Кладовщик" };

            Assert.False(role.IsAdministrator);
        }

        [Fact]
        public void IsStorekeeper_WhenFalse_ReturnsFalse()
        {
            var role = new Role { Position = "Администратор" };

            Assert.False(role.IsStorekeeper);
        }

        [Fact]
        public void Type_WithUnknownPosition_ReturnsDefaultEnum()
        {
            var role = new Role { Position = "Что-то левое" };

            var result = role.Type;

            Assert.True(result == RoleType.Administrator || result == RoleType.Storekeeper);
        }
    }
}