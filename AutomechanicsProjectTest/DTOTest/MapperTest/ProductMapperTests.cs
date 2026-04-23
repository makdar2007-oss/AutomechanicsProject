using Xunit;
using System;
using AutomechanicsProject.Classes;
using AutomechanicsProject.Mappers;
using AutomechanicsProject.Dtos.Service;

namespace AutomechanicsProjectTest.Classes
{
    public class ProductMapperTests
    {
        [Fact]
        public void ToDto_Null_ReturnsNull()
        {
            Assert.Null(ProductMapper.ToDto(null));
        }

        [Fact]
        public void ToDto_ReturnsCorrect()
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Article = "A1",
                Name = "Масло",
                Price = 100,
                PurchasePrice = 80,
                Balance = 5
            };

            var dto = ProductMapper.ToDto(product);

            Assert.Equal("A1", dto.Article);
            Assert.Equal("Масло", dto.Name);
            Assert.Equal(100, dto.Price);
        }

        [Fact]
        public void ToListItemDto_Expired()
        {
            var product = new Product
            {
                ExpiryDate = DateTime.Today.AddDays(-1)
            };

            var dto = ProductMapper.ToListItemDto(product);

            Assert.True(dto.IsExpired);
        }

        [Fact]
        public void ToListItemDto_NearExpiry()
        {
            var product = new Product
            {
                ExpiryDate = DateTime.Today.AddDays(5)
            };

            var dto = ProductMapper.ToListItemDto(product);

            Assert.True(dto.RequiresDiscount);
        }

        [Fact]
        public void ToListItemDto_NoExpiry()
        {
            var product = new Product
            {
                ExpiryDate = null
            };

            var dto = ProductMapper.ToListItemDto(product);

            Assert.False(dto.IsExpired);
            Assert.False(dto.RequiresDiscount);
        }

        [Fact]
        public void ToEntity_CreatesNew()
        {
            var dto = new CreateProductDto
            {
                Article = "A1",
                Name = "Масло",
                Price = 100
            };

            var entity = ProductMapper.ToEntity(dto);

            Assert.NotEqual(Guid.Empty, entity.Id);
            Assert.Equal("A1", entity.Article);
        }

        [Fact]
        public void UpdateEntity_UpdatesFields()
        {
            var entity = new Product();

            var dto = new UpdateProductDto
            {
                Article = "A2",
                Name = "Фильтр",
                PurchasePrice = 200
            };

            ProductMapper.UpdateEntity(entity, dto);

            Assert.Equal("A2", entity.Article);
            Assert.Equal(200, entity.Price); 
        }

        [Fact]
        public void ToComboViewModel_ReturnsCorrectText()
        {
            var dto = new ProductDto
            {
                Id = Guid.NewGuid(),
                Article = "A1",
                Name = "Масло",
                Balance = 5,
                UnitName = "шт"
            };

            var vm = ProductMapper.ToComboViewModel(dto);

            Assert.Contains("A1 - Масло", vm.Text);
            Assert.Contains("остаток: 5", vm.Text);
        }
    }
}