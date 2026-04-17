using AutomechanicsProject.Classes;
using AutomechanicsProject.Dtos.Service;
using AutomechanicsProject.ViewModels;
using System;

namespace AutomechanicsProject.Mappers
{
    /// <summary>
    /// Предоставляет статические методы для преобразования объектов товаров между различными слоями приложения.
    /// </summary>
    public static class ProductMapper
    {
        /// <summary>
        /// Преобразует сущность товара из базы данных в DTO для передачи данных.
        /// </summary>
        public static ProductDto ToDto(Product entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new ProductDto
            {
                Id = entity.Id,
                Article = entity.Article,
                Name = entity.Name,
                CategoryId = entity.CategoryId,
                CategoryName = entity.Category?.Name ?? string.Empty,
                UnitId = entity.UnitId,
                UnitName = entity.Unit?.Name ?? string.Empty,
                PurchasePrice = entity.PurchasePrice,
                Price = entity.Price,
                Balance = entity.Balance,
                ExpiryDate = entity.ExpiryDate,
                BatchNumber = entity.BatchNumber
            };
        }

        /// <summary>
        /// Преобразует сущность товара в DTO для отображения в списке.
        /// </summary>
        public static ProductListItemDto ToListItemDto(Product entity)
        {
            if (entity == null)
            {
                return null;
            } 
                

            var today = DateTime.Today;
            var requiresDiscount = entity.ExpiryDate.HasValue &&
                                   entity.ExpiryDate.Value > today &&
                                   (entity.ExpiryDate.Value - today).Days <= 30;
            var isExpired = entity.ExpiryDate.HasValue && entity.ExpiryDate.Value < today;

            return new ProductListItemDto
            {
                Id = entity.Id,
                Article = entity.Article,
                Name = entity.Name,
                CategoryName = entity.Category?.Name ?? string.Empty,
                UnitName = entity.Unit?.Name ?? string.Empty,
                Balance = entity.Balance,
                ExpiryDate = entity.ExpiryDate,
                Price = entity.Price,
                PurchasePrice = entity.PurchasePrice,
                RequiresDiscount = requiresDiscount,
                IsExpired = isExpired
            };
        }

        /// <summary>
        /// Преобразует DTO создания товара в сущность товара для сохранения в базу данных.
        /// </summary>
        public static Product ToEntity(CreateProductDto dto)
        {
            return new Product
            {
                Id = Guid.NewGuid(),
                Article = dto.Article,
                Name = dto.Name,
                CategoryId = dto.CategoryId,
                UnitId = dto.UnitId,
                PurchasePrice = 0,
                Price = dto.Price,
                ExpiryDate = null,  
                BatchNumber = null
            };
        }

        /// <summary>
        /// Обновляет существующую сущность товара данными из DTO обновления.
        /// </summary>
        public static void UpdateEntity(Product entity, UpdateProductDto dto)
        {
            entity.Article = dto.Article;
            entity.Name = dto.Name;
            entity.CategoryId = dto.CategoryId;
            entity.UnitId = dto.UnitId;
            entity.PurchasePrice = dto.PurchasePrice;
            entity.Price = dto.PurchasePrice;
        }

        /// <summary>
        /// Преобразует DTO товара в ViewModel для выпадающего списка.
        /// </summary>
        public static ProductComboViewModel ToComboViewModel(ProductDto dto)
        {
            return new ProductComboViewModel
            {
                Id = dto.Id,
                Text = $"{dto.Article} - {dto.Name} (остаток: {dto.Balance} {dto.UnitName})",
                Article = dto.Article,
                Name = dto.Name,
                Price = dto.Price,
                Balance = dto.Balance,
                UnitName = dto.UnitName,
                UnitId = dto.UnitId
            };
        }
    }
}