using Xunit;
using AutomechanicsProject.Classes;
using System;

namespace AutomechanicsProjectTest.Classes
{
    public class CurrentUserTests
    {
        [Fact]
        public void Clear_ShouldResetAllFields()
        {
            CurrentUser.Id = Guid.NewGuid();
            CurrentUser.Username = "test";
            CurrentUser.Role = "admin";
            CurrentUser.IsAuthenticated = true;

            CurrentUser.Clear();

            Assert.Equal(Guid.Empty, CurrentUser.Id);
            Assert.Null(CurrentUser.Username);
            Assert.Null(CurrentUser.Role);
            Assert.False(CurrentUser.IsAuthenticated);
        }
    }
}