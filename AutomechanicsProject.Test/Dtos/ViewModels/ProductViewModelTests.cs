using Xunit;
using AutomechanicsProject.ViewModels;
using System;

namespace AutomechanicsProject.Tests.ViewModels
{
    public class ProductViewModelTests
    {
        [Fact]
        public void CanCreateProductViewModel()
        {
            var vm = new ProductViewModel();
            Assert.NotNull(vm);
        }

        [Fact]
        public void CanSetAllProperties()
        {
            var id = Guid.NewGuid();

            var vm = new ProductViewModel
            {
                Id = id,
                Article = "ART-001",
                Name = "Масло",
                Category = "Смазочные",
                UnitName = "л",
                ExpiryDateText = "31.12.2025",
                Balance = 50,
                PurchasePrice = 1000m,
                Price = 1500m,
                BatchNumber = "BATCH-001"
            };

            Assert.Equal(id, vm.Id);
            Assert.Equal("ART-001", vm.Article);
            Assert.Equal("Масло", vm.Name);
            Assert.Equal("Смазочные", vm.Category);
            Assert.Equal("л", vm.UnitName);
            Assert.Equal("31.12.2025", vm.ExpiryDateText);
            Assert.Equal(50, vm.Balance);
            Assert.Equal(1000m, vm.PurchasePrice);
            Assert.Equal(1500m, vm.Price);
            Assert.Equal("BATCH-001", vm.BatchNumber);
        }

        [Fact]
        public void DisplayName_FormatsArticleAndName()
        {
            var vm = new ProductViewModel
            {
                Article = "ART-001",
                Name = "Масло"
            };

            Assert.Equal("ART-001 - Масло", vm.DisplayName);
        }

        [Fact]
        public void DisplayName_WhenArticleIsNull_ShowsNullDashName()
        {
            var vm = new ProductViewModel
            {
                Article = null,
                Name = "Масло"
            };

            Assert.Equal(" - Масло", vm.DisplayName);
        }

        [Fact]
        public void DisplayName_WhenNameIsNull_ShowsArticleDashNull()
        {
            var vm = new ProductViewModel
            {
                Article = "ART-001",
                Name = null
            };

            Assert.Equal("ART-001 - ", vm.DisplayName);
        }

        [Fact]
        public void BalanceWithUnit_FormatsBalanceAndUnit()
        {
            var vm = new ProductViewModel
            {
                Balance = 25,
                UnitName = "л"
            };

            Assert.Equal("25 л", vm.BalanceWithUnit);
        }

        [Fact]
        public void BalanceWithUnit_WhenUnitNameIsNull_ShowsBalanceOnly()
        {
            var vm = new ProductViewModel
            {
                Balance = 10,
                UnitName = null
            };

            Assert.Equal("10 ", vm.BalanceWithUnit);
        }
    }
}