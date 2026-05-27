using Xunit;
using AutomechanicsProject.ViewModels;
using System;

namespace AutomechanicsProject.Tests.ViewModels
{
    public class WarehouseCellViewModelTests
    {
        [Fact]
        public void CanCreateWarehouseCellViewModel()
        {
            var vm = new WarehouseCellViewModel();
            Assert.NotNull(vm);
        }

        [Fact]
        public void CanSetAllProperties()
        {
            var id = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var expiryDate = new DateTime(2025, 12, 31);

            var vm = new WarehouseCellViewModel
            {
                Id = id,
                Row = 5,
                Column = 3,
                CellCode = "A-5-3",
                ProductId = productId,
                ProductName = "Масло",
                Article = "ART-001",
                CategoryName = "Смазочные",
                Balance = 50,
                ExpiryDate = expiryDate,
                HasExpiryDate = true
            };

            Assert.Equal(id, vm.Id);
            Assert.Equal(5, vm.Row);
            Assert.Equal(3, vm.Column);
            Assert.Equal("A-5-3", vm.CellCode);
            Assert.Equal(productId, vm.ProductId);
            Assert.Equal("Масло", vm.ProductName);
            Assert.Equal("ART-001", vm.Article);
            Assert.Equal("Смазочные", vm.CategoryName);
            Assert.Equal(50, vm.Balance);
            Assert.Equal(expiryDate, vm.ExpiryDate);
            Assert.True(vm.HasExpiryDate);
        }

        [Fact]
        public void HasExpiryDate_DefaultValue_IsFalse()
        {
            var vm = new WarehouseCellViewModel();
            Assert.False(vm.HasExpiryDate);
        }

        [Fact]
        public void IsEmpty_WhenProductIdIsNull_ReturnsTrue()
        {
            var vm = new WarehouseCellViewModel { ProductId = null };
            Assert.True(vm.IsEmpty);
        }

        [Fact]
        public void IsEmpty_WhenProductIdHasValue_ReturnsFalse()
        {
            var vm = new WarehouseCellViewModel { ProductId = Guid.NewGuid() };
            Assert.False(vm.IsEmpty);
        }

        [Fact]
        public void Balance_DefaultValue_IsZero()
        {
            var vm = new WarehouseCellViewModel();
            Assert.Equal(0, vm.Balance);
        }

        [Fact]
        public void RowAndColumn_DefaultValue_IsZero()
        {
            var vm = new WarehouseCellViewModel();
            Assert.Equal(0, vm.Row);
            Assert.Equal(0, vm.Column);
        }

        [Fact]
        public void CellCode_CanBeNull()
        {
            var vm = new WarehouseCellViewModel { CellCode = null };
            Assert.Null(vm.CellCode);
        }
    }
}