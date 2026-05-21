using Xunit;
using AutomechanicsProject.Classes;

namespace AutomechanicsProject.Tests.Classes
{
    public class RoleTests
    {
        [Fact]
        public void AdministratorHasValue1()
        {
            Assert.Equal(1, (int)RoleType.Administrator);
        }

        [Fact]
        public void StorekeeperHasValue2()
        {
            Assert.Equal(2, (int)RoleType.Storekeeper);
        }

        [Fact]
        public void RoleCanBeCreated()
        {
            var role = new Role();
            Assert.NotNull(role);
        }

        [Fact]
        public void CanSetRolePosition()
        {
            var role = new Role();
            role.Position = "Администратор";

            Assert.Equal("Администратор", role.Position);
        }
    }
}