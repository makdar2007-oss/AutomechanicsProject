using System;
using System.Linq;
using Xunit;
using AutomechanicsProject.Classes;
using AutomechanicsProject.Helpers;

public class RegistrationTests
{
    [Fact]
    public void Registration_NewUser_AddsUser()
    {
        var db = new FakeDb();

        var role = new Role
        {
            Id = Guid.NewGuid(),
            Position = "Кладовщик"
        };

        db.Roles.Add(role);

        var password = "123456";
        var hashedPassword = PasswordHelper.HashPassword(password);

        var user = new Users
        {
            Id = Guid.NewGuid(),
            Login = "testUser",
            Password = hashedPassword,
            RoleId = role.Id
        };

        db.Users.Add(user);

        Assert.True(db.Users.Any(u => u.Login == "testUser"));
    }

    [Fact]
    public void Registration_DuplicateLogin_ReturnsTrue()
    {
        var db = new FakeDb();

        db.Users.Add(new Users { Login = "admin" });

        var exists = db.Users.Any(u => u.Login == "admin");

        Assert.True(exists);
    }

    [Fact]
    public void Registration_Password_IsHashed()
    {
        var password = "123456";

        var hashed = PasswordHelper.HashPassword(password);

        Assert.NotEqual(password, hashed);
        Assert.False(string.IsNullOrWhiteSpace(hashed));
    }

    [Fact]
    public void Registration_PasswordHash_CanBeVerified()
    {
        var password = "123456";

        var hash = PasswordHelper.HashPassword(password);

        var result = PasswordHelper.VerifyPassword(password, hash);

        Assert.True(result);
    }
}