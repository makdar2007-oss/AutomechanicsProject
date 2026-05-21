using Xunit;
using AutomechanicsProject.Classes;
using System;

namespace AutomechanicsProject.Tests.Classes
{
    public class ProductTests_Minimal
    {
        [Fact]
        public void Product_CanBeCreated()
        {
            var product = new Product();
            Assert.NotNull(product);
        }

        [Fact]
        public void Properties_WorkCorrectly()
        {
            var product = new Product
            {
                Article = "ART-001",
                Name = "Моторное масло",
                Price = 1500.50m,
                Balance = 50
            };

            Assert.Equal("ART-001", product.Article);
            Assert.Equal("Моторное масло", product.Name);
            Assert.Equal(1500.50m, product.Price);
            Assert.Equal(50, product.Balance);
        }

        [Fact]
        public void IsDeleted_DefaultValue_IsFalse()
        {
            var product = new Product();
            Assert.False(product.IsDeleted);
        }

        [Fact]
        public void HasExpiryDate_CanBeChanged()
        {
            var product = new Product();

            product.HasExpiryDate = true;
            Assert.True(product.HasExpiryDate);
        }

        [Fact]
        public void ExpiryDate_CanBeSet()
        {
            var product = new Product();
            var date = new DateTime(2025, 12, 31);

            product.ExpiryDate = date;

            Assert.Equal(date, product.ExpiryDate);
        }
    }
}