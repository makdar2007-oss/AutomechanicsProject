using Xunit;
using AutomechanicsProject.Classes;

namespace AutomechanicsProject.Tests.Classes
{
    public class ImportProductItemTests
    {
        [Fact]
        public void CanCreateImportProductItem()
        {
            var item = new ImportProductItem();
            Assert.NotNull(item);
        }

        [Fact]
        public void CanSetAllProperties()
        {
            var item = new ImportProductItem();
            item.Article = "ART-001";
            item.ProductName = "Моторное масло";
            item.CategoryName = "Смазочные материалы";
            item.UnitName = "Литр";
            item.SupplierName = "ООО Поставка";
            item.Quantity = 100;
            item.Price = 1500.50m;
            item.ExpiryDate = "2025-12-31";

            Assert.Equal("ART-001", item.Article);
            Assert.Equal("Моторное масло", item.ProductName);
            Assert.Equal("Смазочные материалы", item.CategoryName);
            Assert.Equal("Литр", item.UnitName);
            Assert.Equal("ООО Поставка", item.SupplierName);
            Assert.Equal(100, item.Quantity);
            Assert.Equal(1500.50m, item.Price);
            Assert.Equal("2025-12-31", item.ExpiryDate);
        }

        [Fact]
        public void ExpiryDate_CanBeEmpty()
        {
            var item = new ImportProductItem();
            item.ExpiryDate = "";

            Assert.Equal("", item.ExpiryDate);
        }
    }
}