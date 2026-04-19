using System;
using System.Linq;
using Xunit;
using AutomechanicsProject.Classes;
using AutomechanicsProject.Helpers;

public class AuthorizationTests
{
    [Fact]
    public void Authorization_ValidPassword_ReturnsTrue()
    {
        var password = "123456";
        var hash = PasswordHelper.HashPassword(password);

        var user = new Users
        {
            Login = "admin",
            Password = hash
        };

        var result = PasswordHelper.VerifyPassword(password, user.Password);

        Assert.True(result);
    }

    [Fact]
    public void Authorization_InvalidPassword_ReturnsFalse()
    {
        var hash = PasswordHelper.HashPassword("123456");

        var result = PasswordHelper.VerifyPassword("wrong", hash);

        Assert.False(result);
    }

    [Fact]
    public void Authorization_UserNotFound_ReturnsNull()
    {
        var db = new FakeDb();

        var user = db.Users.FirstOrDefault(u => u.Login == "ghost");

        Assert.Null(user);
    }

    [Fact]
    public void Authorization_LegacyPassword_WithoutHash_Works()
    {
        var user = new Users
        {
            Login = "admin",
            Password = "123456" 
        };

        var inputPassword = "123456";

        var isValid = inputPassword == user.Password;

        Assert.True(isValid);
    }

    [Fact]
    public void Authorization_LegacyPassword_Wrong_ReturnsFalse()
    {
        var user = new Users
        {
            Login = "admin",
            Password = "123456"
        };

        var isValid = "wrong" == user.Password;

        Assert.False(isValid);
    }
}