using Xunit;
using AutomechanicsProject.Classes;
using System;

public class AddressTests
{
    [Fact]
    public void Address_ShouldStoreCompanyName()
    {
        var address = new Address { CompanyName = "ООО Рога" };

        Assert.Equal("ООО Рога", address.CompanyName);
    }

    [Fact]
    public void Address_CompanyNameCanBeNull()
    {
        var address = new Address { CompanyName = null };

        Assert.Null(address.CompanyName);
    }

    [Fact]
    public void Address_ShouldStoreId()
    {
        var address = new Address { Id = Guid.NewGuid() };

        Assert.NotEqual(Guid.Empty, address.Id);
    }

    [Fact]
    public void Address_ShouldBeCreated_NotNull()
    {
        var address = new Address();

        Assert.NotNull(address);
    }

    [Fact]
    public void FullName_ShouldReturnCompanyName()
    {
        var address = new Address
        {
            CompanyName = "ООО Авто"
        };

        Assert.Equal("ООО Авто", address.FullName);
    }

    [Fact]
    public void FullName_ShouldBeNull_WhenCompanyNameIsNull()
    {
        var address = new Address
        {
            CompanyName = null
        };

        Assert.Null(address.FullName);
    }

    [Fact]
    public void FullName_ShouldUpdate_WhenCompanyNameChanges()
    {
        var address = new Address
        {
            CompanyName = "Старое"
        };

        address.CompanyName = "Новое";

        Assert.Equal("Новое", address.FullName);
    }

    [Fact]
    public void Address_ShouldAllowChangingCompanyName()
    {
        var address = new Address
        {
            CompanyName = "A"
        };

        address.CompanyName = "B";

        Assert.Equal("B", address.CompanyName);
    }

    [Fact]
    public void Address_ShouldHandleEmptyCompanyName()
    {
        var address = new Address
        {
            CompanyName = ""
        };

        Assert.Equal("", address.CompanyName);
    }

    [Fact]
    public void FullName_ShouldHandleEmptyCompanyName()
    {
        var address = new Address
        {
            CompanyName = ""
        };

        Assert.Equal("", address.FullName);
    }
}