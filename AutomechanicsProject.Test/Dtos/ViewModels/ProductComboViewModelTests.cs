using Xunit;
using AutomechanicsProject.ViewModels;
using System;

namespace AutomechanicsProject.Tests.ViewModels
{
    public class ProductComboViewModelTests
    {
        [Fact]
        public void CanCreateProductComboViewModel()
        {
            var vm = new ProductComboViewModel();
            Assert.NotNull(vm);
        }

        [Fact]
        public void CanSetAllProperties()
        {
            var id = Guid.NewGuid();
            var unitId = Guid.NewGuid();

            var vm = new ProductComboViewModel
            {
                Id = id,
                Text = "ART-001 - Масло (10 шт)",
                Article = "ART-001",
                Name = "Масло",
                Price = 1500m,
                Balance = 10,
                UnitName = "шт",
                UnitId = unitId,
                IsMetal = true
            };

            Assert.Equal(id, vm.Id);
            Assert.Equal("ART-001 - Масло (10 шт)", vm.Text);
            Assert.Equal("ART-001", vm.Article);
            Assert.Equal("Масло", vm.Name);
            Assert.Equal(1500m, vm.Price);
            Assert.Equal(10, vm.Balance);
            Assert.Equal("шт", vm.UnitName);
            Assert.Equal(unitId, vm.UnitId);
            Assert.True(vm.IsMetal);
        }

        [Fact]
        public void IsMetal_DefaultValue_IsFalse()
        {
            var vm = new ProductComboViewModel();
            Assert.False(vm.IsMetal);
        }

        [Fact]
        public void Balance_DefaultValue_IsZero()
        {
            var vm = new ProductComboViewModel();
            Assert.Equal(0, vm.Balance);
        }
    }
}