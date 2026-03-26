using Xunit;
using AutomechanicsProject.Classes;

public class UsersTests
{
    [Fact]
    public void Users_ShouldStoreLogin()
    {
        var user = new Users
        {
            Login = "user123"
        };

        Assert.Equal("user123", user.Login);
    }

    [Fact]
    public void Users_ShouldStorePassword()
    {
        var user = new Users
        {
            Password = "hashed_password"
        };

        Assert.Equal("hashed_password", user.Password);
    }

    [Fact]
    public void Users_ShouldHaveRole()
    {
        var role = new Role
        {
            Type = RoleType.Administrator
        };

        var user = new Users
        {
            Role = role
        };

        Assert.Equal(RoleType.Administrator, user.Role.Type);
    }

    [Fact]
    public void Users_ShouldReturnFullName()
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
    public void Users_ShouldReturnRoleName()
    {
        var role = new Role
        {
            Type = RoleType.Administrator
        };

        var user = new Users
        {
            Role = role
        };

        Assert.Equal("Администратор", user.RoleName);
    }

    [Fact]
    public void Users_ShouldReturnDefaultRoleName_WhenRoleIsNull()
    {
        var user = new Users();

        Assert.Equal("Не назначена", user.RoleName);
    }
}