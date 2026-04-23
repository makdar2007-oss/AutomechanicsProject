using Xunit;
using System;
using AutomechanicsProject.Classes;

public class UsersTests
{
    [Fact]
    public void FullName_ReturnsCorrect()
    {
        var user = new Users
        {
            Surname = "Иванов",
            Name = "Иван",
            Lastname = "Иванович"
        };

        var result = user.FullName;

        Assert.Equal("Иванов Иван Иванович", result);
    }

    [Fact]
    public void FullName_WithoutLastname_Works()
    {
        var user = new Users
        {
            Surname = "Иванов",
            Name = "Иван",
            Lastname = null
        };

        var result = user.FullName;

        Assert.Equal("Иванов Иван", result);
    }

    [Fact]
    public void RoleName_WithRole_ReturnsRole()
    {
        var user = new Users
        {
            Role = new Role
            {
                Position = "Admin"
            }
        };

        var result = user.RoleName;

        Assert.Equal("Admin", result);
    }

    [Fact]
    public void RoleName_WithoutRole_ReturnsDefault()
    {
        var user = new Users
        {
            Role = null
        };

        var result = user.RoleName;

        Assert.Equal("Не назначена", result);
    }

    [Fact]
    public void Properties_SetCorrectly()
    {
        var user = new Users
        {
            Login = "admin",
            Password = "123",
            RoleId = Guid.NewGuid()
        };

        Assert.Equal("admin", user.Login);
        Assert.Equal("123", user.Password);
    }
}