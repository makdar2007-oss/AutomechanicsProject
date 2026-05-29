using Xunit;
using AutomechanicsProject.Classes;

namespace AutomechanicsProject.Tests.Classes
{
    public class RoleTests_Minimal
    {
        [Fact]
        public void IsAdministrator_ReturnsTrue_ForAdministrator()
        {
            var role = new Role { Type = RoleType.Administrator };
            Assert.True(role.IsAdministrator);
        }

        [Fact]
        public void IsStorekeeper_ReturnsTrue_ForStorekeeper()
        {
            var role = new Role { Type = RoleType.Storekeeper };
            Assert.True(role.IsStorekeeper);
        }

        [Fact]
        public void FromType_ReturnsRoleWithCorrectType()
        {
            var adminRole = Role.FromType(RoleType.Administrator);
            Assert.Equal(RoleType.Administrator, adminRole.Type);

            var storekeeperRole = Role.FromType(RoleType.Storekeeper);
            Assert.Equal(RoleType.Storekeeper, storekeeperRole.Type);
        }

        [Fact]
        public void ToString_ReturnsPosition()
        {
            var role = new Role { Type = RoleType.Administrator };
            Assert.Equal(role.Position, role.ToString());
        }
    }
}