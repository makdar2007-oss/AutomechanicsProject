using Xunit;
using AutomechanicsProject.Classes;

namespace AutomechanicsProject.Tests.Classes
{
    public class AddressTests
    {
        [Fact]
        public void CanCreateAddress()
        {
            var address = new Address();
            Assert.NotNull(address);
        }

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
    }
}