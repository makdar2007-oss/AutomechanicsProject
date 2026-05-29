using Xunit;
using AutomechanicsProject.ViewModels;
using System;

namespace AutomechanicsProject.Tests.ViewModels
{
    public class ShipmentViewModelTests
    {
        [Fact]
        public void CanCreateShipmentViewModel()
        {
            var vm = new ShipmentViewModel();
            Assert.NotNull(vm);
        }

        [Fact]
        public void CanSetAllProperties()
        {
            var productId = Guid.NewGuid();

            var vm = new ShipmentViewModel
            {
                Article = "ART-001",
                Name = "Масло",
                Quantity = 5,
                Price = 1000m,
                Profit = 500m,
                Total = 5000m,
                RecipientName = "ООО Ромашка",
                ProductId = productId,
                IsMetal = true,
                IsScrapped = false,
                ScrapMetal = true
            };

            Assert.Equal("ART-001", vm.Article);
            Assert.Equal("Масло", vm.Name);
            Assert.Equal(5, vm.Quantity);
            Assert.Equal(1000m, vm.Price);
            Assert.Equal(500m, vm.Profit);
            Assert.Equal(5000m, vm.Total);
            Assert.Equal("ООО Ромашка", vm.RecipientName);
            Assert.Equal(productId, vm.ProductId);
            Assert.True(vm.IsMetal);
            Assert.False(vm.IsScrapped);
            Assert.True(vm.ScrapMetal);
        }

        [Fact]
        public void IsMetal_DefaultValue_IsFalse()
        {
            var vm = new ShipmentViewModel();
            Assert.False(vm.IsMetal);
        }

        [Fact]
        public void IsScrapped_DefaultValue_IsFalse()
        {
            var vm = new ShipmentViewModel();
            Assert.False(vm.IsScrapped);
        }

        [Fact]
        public void ScrapMetal_DefaultValue_IsFalse()
        {
            var vm = new ShipmentViewModel();
            Assert.False(vm.ScrapMetal);
        }

        [Fact]
        public void TotalText_FormatsTotalAsCurrency()
        {
            var vm = new ShipmentViewModel { Total = 15000m };
            Assert.Equal("15 000,00 ₽", vm.TotalText);
        }

        [Fact]
        public void TotalText_WhenTotalIsZero_ReturnsZeroCurrency()
        {
            var vm = new ShipmentViewModel { Total = 0m };
            Assert.Equal("0,00 ₽", vm.TotalText);
        }

        [Fact]
        public void ProfitText_FormatsProfitAsCurrency()
        {
            var vm = new ShipmentViewModel { Profit = 5000m };
            Assert.Equal("5 000,00 ₽", vm.ProfitText);
        }

        [Fact]
        public void ProfitText_WhenProfitIsNegative_ShowsNegativeCurrency()
        {
            var vm = new ShipmentViewModel { Profit = -100m };
            Assert.Equal("-100,00 ₽", vm.ProfitText);
        }
    }
}