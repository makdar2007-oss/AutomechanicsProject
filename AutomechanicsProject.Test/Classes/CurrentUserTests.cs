using Xunit;
using AutomechanicsProject.Classes;
using System;

namespace AutomechanicsProject.Tests.Classes
{
    public class CurrentUserTests
    {
        [Fact]
        public void Clear_ResetsAllUserData()
        {
            CurrentUser.Id = Guid.NewGuid();
            CurrentUser.Username = "testuser";
            CurrentUser.Role = "Administrator";
            CurrentUser.IsAuthenticated = true;

            CurrentUser.Clear();

            Assert.Equal(Guid.Empty, CurrentUser.Id);
            Assert.Null(CurrentUser.Username);
            Assert.Null(CurrentUser.Role);
            Assert.False(CurrentUser.IsAuthenticated);
        }

        [Fact]
        public void IsAuthenticated_DefaultValue_IsFalse()
        {
            CurrentUser.Clear();
            Assert.False(CurrentUser.IsAuthenticated);
        }
    }
}