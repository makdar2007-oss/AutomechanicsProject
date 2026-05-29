using Xunit;
using AutomechanicsProject.Classes;

namespace AutomechanicsProject.Tests.Classes
{
    public class UsersTests
    {
        [Fact]
        public void User_CanBeCreated()
        {
            var user = new Users();
            Assert.NotNull(user);
        }

        [Fact]
        public void FullName_CombinesAllNames()
        {
            var user = new Users
            {
                Surname = "Иванов",
                Name = "Иван",
                Lastname = "Иванович"
            };

            Assert.Equal("Иванов Иван Иванович", user.FullName);
        }

        [Fact]
        public void FullName_HandlesMissingParts()
        {
            var user = new Users
            {
                Surname = "Петров",
                Name = "Петр",
                Lastname = null
            };

            Assert.Equal("Петров Петр", user.FullName);
        }
    }
}