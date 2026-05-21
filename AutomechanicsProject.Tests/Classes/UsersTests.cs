using Xunit;
using AutomechanicsProject.Classes;

namespace AutomechanicsProject.Tests.Classes
{
    public class UsersTests
    {
        [Fact]
        public void CanCreateUser()
        {
            var user = new Users();
            Assert.NotNull(user);
        }

        [Fact]
        public void CanSetUserNames()
        {
            var user = new Users();
            user.Surname = "Иванов";
            user.Name = "Иван";
            user.Lastname = "Иванович";

            Assert.Equal("Иванов", user.Surname);
            Assert.Equal("Иван", user.Name);
            Assert.Equal("Иванович", user.Lastname);
        }

        [Fact]
        public void FullNameCombinesAllNames()
        {
            var user = new Users();
            user.Surname = "Петров";
            user.Name = "Петр";
            user.Lastname = "Петрович";

            Assert.Equal("Петров Петр Петрович", user.FullName);
        }

        [Fact]
        public void CanSetLoginAndPassword()
        {
            var user = new Users();
            user.Login = "admin";
            user.Password = "12345";

            Assert.Equal("admin", user.Login);
            Assert.Equal("12345", user.Password);
        }
    }
}