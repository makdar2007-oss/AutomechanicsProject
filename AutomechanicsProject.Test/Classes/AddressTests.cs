using Xunit;
using AutomechanicsProject.Classes;
using System;

namespace AutomechanicsProject.Tests.Classes
{
    public class AddressTests
    {
        [Fact]
        public void CanSetCompanyName()
        {
            var address = new Address();
            address.CompanyName = "ООО Ромашка";
            Assert.Equal("ООО Ромашка", address.CompanyName);
        }

        [Fact]
        public void FullNameReturnsCompanyName()
        {
            var address = new Address();
            address.CompanyName = "ИП Иванов";
            Assert.Equal("ИП Иванов", address.FullName);
        }

        [Fact]
        public void CompanyNameCanBeEmpty()
        {
            var address = new Address();
            address.CompanyName = "";
            Assert.Equal("", address.CompanyName);
            Assert.Equal("", address.FullName);
        }
    }
}