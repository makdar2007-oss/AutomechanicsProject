using Xunit;
using AutomechanicsProject.Classes;
using System;

namespace AutomechanicsProject.Tests.Classes
{
    public class SupplierTests
    {
        [Fact]
        public void CanCreateSupplier()
        {
            var supplier = new Supplier();
            Assert.NotNull(supplier);
        }

        [Fact]
        public void CanSetSupplierName()
        {
            var supplier = new Supplier();
            supplier.Name = "ООО Поставка";
            Assert.Equal("ООО Поставка", supplier.Name);
        }

        [Fact]
        public void CanSetPhoneAndAddress()
        {
            var supplier = new Supplier();
            supplier.Phone = "+7-999-123-45-67";
            supplier.Address = "г. Москва, ул. Ленина, д. 1";

            Assert.Equal("+7-999-123-45-67", supplier.Phone);
            Assert.Equal("г. Москва, ул. Ленина, д. 1", supplier.Address);
        }
    }
}