using Xunit;
using AutomechanicsProject.ViewModels;

namespace AutomechanicsProjectTest.Classes
{
    public class ViewModelTests
    {
        [Fact]
        public void ProductViewModel_DisplayName()
        {
            var vm = new ProductViewModel
            {
                Article = "A1",
                Name = "Масло"
            };

            Assert.Equal("A1 - Масло", vm.DisplayName);
        }

        [Fact]
        public void ProductViewModel_Balance()
        {
            var vm = new ProductViewModel
            {
                Balance = 10,
                UnitName = "шт"
            };

            Assert.Equal("10 шт", vm.BalanceWithUnit);
        }

        [Fact]
        public void ShipmentViewModel_Texts()
        {
            var vm = new ShipmentViewModel
            {
                Total = 100,
                Profit = 50
            };

            Assert.False(string.IsNullOrWhiteSpace(vm.TotalText));
            Assert.False(string.IsNullOrWhiteSpace(vm.ProfitText));
        }

        [Fact]
        public void ProductDisplayItem_Active()
        {
            var item = new AutomechanicsProject.Dtos.UI.ProductDisplayItem
            {
                Name = "Масло",
                Balance = 5,
                IsActive = true
            };

            Assert.Contains("в наличии", item.DisplayName);
        }

        [Fact]
        public void ProductDisplayItem_SearchString()
        {
            var item = new AutomechanicsProject.Dtos.UI.ProductDisplayItem
            {
                Article = "A1",
                Name = "Масло"
            };

            Assert.Equal("a1 масло", item.SearchString);
        }

        [Fact]
        public void ShipmentViewModel_TotalText_ContainsValue()
        {
            var vm = new ShipmentViewModel
            {
                Total = 100
            };

            Assert.Contains("100", vm.TotalText);
        }

        [Fact]
        public void ShipmentViewModel_ProfitText_ContainsValue()
        {
            var vm = new ShipmentViewModel
            {
                Profit = 50
            };

            Assert.Contains("50", vm.ProfitText);
        }

        [Fact]
        public void ProductViewModel_DisplayName_Partial()
        {
            var vm = new ProductViewModel
            {
                Article = "A1"
            };

            Assert.Contains("A1", vm.DisplayName);
        }

        [Fact]
        public void ProductDisplayItem_SearchString_Lowercase()
        {
            var item = new AutomechanicsProject.Dtos.UI.ProductDisplayItem
            {
                Article = "A1",
                Name = "МАСЛО"
            };

            Assert.Equal("a1 масло", item.SearchString);
        }
    }
}