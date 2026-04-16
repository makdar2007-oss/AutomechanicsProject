using AutomechanicsProject.Classes;
using AutomechanicsProject.Dtos.Service;
using AutomechanicsProject.ViewModels;
using System;

public static class ProductMapper
{
    public static ProductDto ToDto(Product entity)
    {
        if (entity == null) return null;

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

    public static ProductListItemDto ToListItemDto(Product entity)
    {
        if (entity == null) return null;

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

    public static Product ToEntity(CreateProductDto dto)
    {
        return new Product
        {
            Id = Guid.NewGuid(),
            Article = dto.Article,
            Name = dto.Name,
            CategoryId = dto.CategoryId,
            UnitId = dto.UnitId,
            PurchasePrice = dto.Price,
            Price = dto.Price,
            Balance = 0,
            ExpiryDate = dto.HasExpiryDate ? DateTime.Today.AddYears(1) : (DateTime?)null
        };
    }

    public static void UpdateEntity(Product entity, UpdateProductDto dto)
    {
        entity.Article = dto.Article;
        entity.Name = dto.Name;
        entity.CategoryId = dto.CategoryId;
        entity.UnitId = dto.UnitId;
        entity.PurchasePrice = dto.PurchasePrice;
        entity.Price = dto.PurchasePrice;
    }

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