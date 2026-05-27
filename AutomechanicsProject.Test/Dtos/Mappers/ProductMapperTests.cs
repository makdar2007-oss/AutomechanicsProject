using Xunit;
using AutomechanicsProject.Classes;
using AutomechanicsProject.Mappers;
using AutomechanicsProject.Dtos.Service;
using System;

namespace AutomechanicsProject.Tests.Mappers
{
    public class ProductMapperTests
    {
        [Fact]
        public void ToDto_WhenEntityIsNull_ReturnsNull()
        {
            var dto = ProductMapper.ToDto(null);
            Assert.Null(dto);
        }

        [Fact]
        public void ToDto_ConvertsEntityToDto()
        {
            var productId = Guid.NewGuid();
            var categoryId = Guid.NewGuid();
            var unitId = Guid.NewGuid();
            var expiryDate = new DateTime(2025, 12, 31);

            var product = new Product
            {
                Id = productId,
                Article = "ART-001",
                Name = "Масло",
                CategoryId = categoryId,
                UnitId = unitId,
                PurchasePrice = 1000m,
                Price = 1500m,
                Balance = 50,
                ExpiryDate = expiryDate,
                BatchNumber = "BATCH-001"
            };

            var dto = ProductMapper.ToDto(product);

            Assert.Equal(productId, dto.Id);
            Assert.Equal("ART-001", dto.Article);
            Assert.Equal("Масло", dto.Name);
            Assert.Equal(categoryId, dto.CategoryId);
            Assert.Equal(unitId, dto.UnitId);
            Assert.Equal(1000m, dto.PurchasePrice);
            Assert.Equal(1500m, dto.Price);
            Assert.Equal(50, dto.Balance);
            Assert.Equal(expiryDate, dto.ExpiryDate);
            Assert.Equal("BATCH-001", dto.BatchNumber);
        }

        [Fact]
        public void ToDto_WhenCategoryIsNull_UsesEmptyString()
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Article = "ART-001",
                Name = "Масло",
                Category = null,
                Unit = null
            };

            var dto = ProductMapper.ToDto(product);

            Assert.Equal(string.Empty, dto.CategoryName);
            Assert.Equal(string.Empty, dto.UnitName);
        }

        [Fact]
        public void ToListItemDto_WhenEntityIsNull_ReturnsNull()
        {
            var dto = ProductMapper.ToListItemDto(null);
            Assert.Null(dto);
        }

        [Fact]
        public void ToListItemDto_ConvertsEntityToListItemDto()
        {
            var productId = Guid.NewGuid();
            var expiryDate = new DateTime(2025, 12, 31);

            var product = new Product
            {
                Id = productId,
                Article = "ART-001",
                Name = "Масло",
                Balance = 50,
                ExpiryDate = expiryDate,
                Price = 1500m,
                PurchasePrice = 1000m
            };

            var dto = ProductMapper.ToListItemDto(product);

            Assert.Equal(productId, dto.Id);
            Assert.Equal("ART-001", dto.Article);
            Assert.Equal("Масло", dto.Name);
            Assert.Equal(50, dto.Balance);
            Assert.Equal(expiryDate, dto.ExpiryDate);
            Assert.Equal(1500m, dto.Price);
            Assert.Equal(1000m, dto.PurchasePrice);
        }

        [Fact]
        public void ToListItemDto_WhenExpiryDateIs30DaysOrLess_SetsRequiresDiscountTrue()
        {
            var expiryDate = MoscowTime.Today.AddDays(15);
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Article = "ART-001",
                Name = "Масло",
                ExpiryDate = expiryDate
            };

            var dto = ProductMapper.ToListItemDto(product);

            Assert.True(dto.RequiresDiscount);
            Assert.False(dto.IsExpired);
        }

        [Fact]
        public void ToListItemDto_WhenExpiryDateIsMoreThan30Days_SetsRequiresDiscountFalse()
        {
            var expiryDate = MoscowTime.Today.AddDays(45);
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Article = "ART-001",
                Name = "Масло",
                ExpiryDate = expiryDate
            };

            var dto = ProductMapper.ToListItemDto(product);

            Assert.False(dto.RequiresDiscount);
            Assert.False(dto.IsExpired);
        }

        [Fact]
        public void ToListItemDto_WhenExpiryDateIsPast_SetsIsExpiredTrue()
        {
            var expiryDate = MoscowTime.Today.AddDays(-5);
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Article = "ART-001",
                Name = "Масло",
                ExpiryDate = expiryDate
            };

            var dto = ProductMapper.ToListItemDto(product);

            Assert.True(dto.IsExpired);
            Assert.False(dto.RequiresDiscount);
        }

        [Fact]
        public void ToListItemDto_WhenNoExpiryDate_SetsRequiresDiscountFalseAndIsExpiredFalse()
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Article = "ART-001",
                Name = "Масло",
                ExpiryDate = null
            };

            var dto = ProductMapper.ToListItemDto(product);

            Assert.False(dto.RequiresDiscount);
            Assert.False(dto.IsExpired);
        }

        [Fact]
        public void ToEntity_ConvertsCreateDtoToEntity()
        {
            var categoryId = Guid.NewGuid();
            var unitId = Guid.NewGuid();

            var dto = new CreateProductDto
            {
                Article = "ART-NEW",
                Name = "Новый товар",
                CategoryId = categoryId,
                UnitId = unitId,
                Price = 2000m,
                HasExpiryDate = true
            };

            var entity = ProductMapper.ToEntity(dto);

            Assert.Equal(dto.Article, entity.Article);
            Assert.Equal(dto.Name, entity.Name);
            Assert.Equal(categoryId, entity.CategoryId);
            Assert.Equal(unitId, entity.UnitId);
            Assert.Equal(dto.Price, entity.Price);
            Assert.True(entity.HasExpiryDate);
            Assert.Equal(0, entity.PurchasePrice);
            Assert.Null(entity.ExpiryDate);
            Assert.Null(entity.BatchNumber);
        }

        [Fact]
        public void UpdateEntity_UpdatesExistingEntity()
        {
            var entityId = Guid.NewGuid();
            var newCategoryId = Guid.NewGuid();
            var newUnitId = Guid.NewGuid();

            var entity = new Product
            {
                Id = entityId,
                Article = "OLD",
                Name = "OLD NAME",
                CategoryId = Guid.NewGuid(),
                UnitId = Guid.NewGuid(),
                PurchasePrice = 100m,
                Price = 100m
            };

            var dto = new UpdateProductDto
            {
                Article = "NEW-ART",
                Name = "NEW NAME",
                CategoryId = newCategoryId,
                UnitId = newUnitId,
                PurchasePrice = 500m
            };

            ProductMapper.UpdateEntity(entity, dto);

            Assert.Equal("NEW-ART", entity.Article);
            Assert.Equal("NEW NAME", entity.Name);
            Assert.Equal(newCategoryId, entity.CategoryId);
            Assert.Equal(newUnitId, entity.UnitId);
            Assert.Equal(500m, entity.PurchasePrice);
            Assert.Equal(500m, entity.Price);
            Assert.Equal(entityId, entity.Id);
        }

        [Fact]
        public void ToComboViewModel_ConvertsDtoToViewModel()
        {
            var productId = Guid.NewGuid();
            var unitId = Guid.NewGuid();

            var dto = new ProductDto
            {
                Id = productId,
                Article = "ART-001",
                Name = "Масло",
                Price = 1500m,
                Balance = 10,
                UnitName = "л",
                UnitId = unitId
            };

            var vm = ProductMapper.ToComboViewModel(dto);

            Assert.Equal(productId, vm.Id);
            Assert.Equal("ART-001", vm.Article);
            Assert.Equal("Масло", vm.Name);
            Assert.Equal(1500m, vm.Price);
            Assert.Equal(10, vm.Balance);
            Assert.Equal("л", vm.UnitName);
            Assert.Equal(unitId, vm.UnitId);
        }
    }
}