using Xunit;
using System;
using AutomechanicsProject.Classes;

public class AddressTests
{
    [Fact]
    public void FullName_ReturnsCompanyName()
    {
        var address = new Address
        {
            CompanyName = "ООО Ромашка"
        };

        Assert.Equal("ООО Ромашка", address.FullName);
    }
}