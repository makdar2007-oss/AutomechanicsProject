using AutomechanicsProject.Classes;
using Xunit;


public class RoleTests
{
    [Fact]
    public void Role_DefaultType_ShouldBeStorekeeper_WhenPositionIsNull()
    {
        var role = new Role();

        Assert.Equal(RoleType.Storekeeper, role.Type);
    }

    [Fact]
    public void Role_ShouldStoreAdministratorType()
    {
        var role = new Role
        {
            Type = RoleType.Administrator
        };

        Assert.Equal(RoleType.Administrator, role.Type);
    }

    [Fact]
    public void Role_ShouldStoreStorekeeperType()
    {
        var role = new Role
        {
            Type = RoleType.Storekeeper
        };

        Assert.Equal(RoleType.Storekeeper, role.Type);
    }

    [Fact]
    public void Role_ShouldAllowChangingType()
    {
        var role = new Role
        {
            Type = RoleType.Storekeeper
        };

        role.Type = RoleType.Administrator;

        Assert.Equal(RoleType.Administrator, role.Type);
    }

    [Theory]
    [InlineData(RoleType.Administrator)]
    [InlineData(RoleType.Storekeeper)]
    public void Role_ShouldAcceptAllEnumValues(RoleType type)
    {
        var role = new Role
        {
            Type = type
        };

        Assert.Equal(type, role.Type);
    }
}