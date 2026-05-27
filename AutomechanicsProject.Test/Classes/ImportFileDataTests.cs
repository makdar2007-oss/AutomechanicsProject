using Xunit;
using AutomechanicsProject.Classes;
using System.Collections.Generic;

namespace AutomechanicsProject.Tests.Classes
{
    public class ImportFileDataTests
    {
        [Fact]
        public void CanCreateImportFileData()
        {
            var data = new ImportFileData();
            Assert.NotNull(data);
        }

        [Fact]
        public void CanSetCurrency()
        {
            var data = new ImportFileData();
            data.Currency = "USD";

            Assert.Equal("USD", data.Currency);
        }

        [Fact]
        public void ProductsCollection_CanBeInitialized()
        {
            var data = new ImportFileData();
            data.Products = new List<ImportProductItem>();

            Assert.NotNull(data.Products);
        }

        [Fact]
        public void ProductsCollection_CanAddItems()
        {
            var data = new ImportFileData();
            data.Products = new List<ImportProductItem>
            {
                new ImportProductItem { Article = "ART-001", ProductName = "Товар 1" },
                new ImportProductItem { Article = "ART-002", ProductName = "Товар 2" }
            };

            Assert.Equal(2, data.Products.Count);
            Assert.Equal("ART-001", data.Products[0].Article);
            Assert.Equal("Товар 2", data.Products[1].ProductName);
        }
    }
}