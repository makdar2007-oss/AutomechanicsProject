using AutomechanicsProject.Classes;
using AutomechanicsProject.Dtos.Service;
using AutomechanicsProject.Dtos.UI;
using AutomechanicsProject.Mappers;
using AutomechanicsProject.Properties;
using AutomechanicsProject.Services.Interfaces;
using AutomechanicsProject.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomechanicsProject.Services
{
    /// <summary>
    /// Сервис для работы с товарами
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IDateBaseContext _db;
        private readonly IWarehouseHeatmapService _warehouseHeatmapService;

        /// <summary>
        /// Создает сервис товаров
        /// </summary>
        public ProductService(IDateBaseContext db, IWarehouseHeatmapService warehouseHeatmapService)
        {
            _db = db;
            _warehouseHeatmapService = warehouseHeatmapService ?? throw new ArgumentNullException(nameof(warehouseHeatmapService));
        }

        /// <summary>
        /// Получает список неудаленных категорий для ComboBox
        /// </summary>
        public List<ComboItemDto> GetCategoriesForCombo()
        {
            return _db.Categories
                .Where(c => !c.IsDeleted)
                .OrderBy(c => c.Name)
                .Select(c => new ComboItemDto
                {
                    Id = c.Id,
                    Text = c.Name
                })
                .ToList();
        }

        /// <summary>
        /// Получает список единиц измерения для ComboBox
        /// </summary>
        public List<ComboItemDto> GetUnitsForCombo()
        {
            return _db.Units
                .OrderBy(u => u.Name)
                .Select(u => new ComboItemDto
                {
                    Id = u.Id,
                    Text = $"{u.Name} ({u.ShortName})"
                })
                .ToList();
        }

        /// <summary>
        /// Генерирует артикул по категории
        /// </summary>
        public string GenerateArticle(string categoryName)
        {
            var prefix = GetCategoryPrefix(categoryName);

            var lastProduct = _db.Products
                .Where(p => p.Article.StartsWith(prefix))
                .OrderByDescending(p => p.Article)
                .FirstOrDefault();

            if (lastProduct != null)
            {
                var numberPart = lastProduct.Article.Substring(prefix.Length);

                if (int.TryParse(numberPart, out var number))
                {
                    return $"{prefix}{(number + 1):D4}";
                }
            }

            return $"{prefix}0001";
        }

        /// <summary>
        /// Генерирует обычный артикул
        /// </summary>
        public string GenerateDefaultArticle()
        {
            var lastProduct = _db.Products
                .Where(p => p.Article.StartsWith("ART-"))
                .OrderByDescending(p => p.Article)
                .FirstOrDefault();

            if (lastProduct != null)
            {
                var numberPart = lastProduct.Article.Substring(4);

                if (int.TryParse(numberPart, out var number))
                {
                    return $"ART-{(number + 1):D4}";
                }
            }

            return "ART-0001";
        }

        /// <summary>
        /// Добавляет новый товар
        /// </summary>
        public void AddProduct(CreateProductDto dto)
        {
            var product = ProductMapper.ToEntity(dto);

            _db.Products.Add(product);
            _db.SaveChanges();
        }

        /// <summary>
        /// Обновляет товар
        /// </summary>
        public void UpdateProduct(Product product)
        {
            _db.SaveChanges();
        }

        /// <summary>
        /// Получает неудаленный товар по id
        /// </summary>
        public Product GetProductById(Guid id)
        {
            return _db.Products
                .Include(p => p.Category)
                .Include(p => p.Unit)
                .FirstOrDefault(p => p.Id == id && !p.IsDeleted);
        }

        /// <summary>
        /// Получает неудаленный товар по артикулу
        /// </summary>
        public Product GetProductByArticle(string article)
        {
            return _db.Products
                .Include(p => p.Category)
                .Include(p => p.Unit)
                .FirstOrDefault(p => p.Article == article.Trim() && !p.IsDeleted);
        }

        /// <summary>
        /// Помечает товар как удаленный
        /// </summary>
        public void DeleteProduct(Guid productId)
        {
            var product = _db.Products.FirstOrDefault(p => p.Id == productId);

            if (product == null)
            {
                throw new Exception(Resources.ErrorProductNotFoundGeneric);
            }

            if (product.IsDeleted)
            {
                throw new Exception(Resources.ErrorProductAlreadyDeleted);
            }

            product.IsDeleted = true;
            _db.SaveChanges();
            _warehouseHeatmapService.FreeDeletedProductCells();
        }

        /// <summary>
        /// Получает товары для отгрузки
        /// </summary>
        public List<ProductComboViewModel> GetProductsForShipment()
        {
            var productsList = _db.Products
                .Where(p => !p.IsDeleted && p.Balance > 0)
                .Select(p => new
                {
                    p.Id,
                    p.Article,
                    p.Name,
                    p.Balance,
                    p.Price,
                    UnitName = p.Unit != null ? p.Unit.Name : Resources.Unit_Piece_Short,
                    p.UnitId,
                    p.IsMetal
                })
                .ToList();

            return productsList
                .GroupBy(p => p.Name)
                .Select(g => new ProductComboViewModel
                {
                    Id = g.First().Id,
                    Article = g.First().Article,
                    Name = g.Key,
                    Price = g.First().Price,
                    Balance = g.Sum(x => x.Balance),
                    UnitName = g.First().UnitName,
                    UnitId = g.First().UnitId,
                    IsMetal = g.First().IsMetal,
                    Text = $"{g.First().Article} - {g.Key} ({g.Sum(x => x.Balance)} {g.First().UnitName})"
                })
                .ToList();
        }

        /// <summary>
        /// Получает префикс категории
        /// </summary>
        private string GetCategoryPrefix(string categoryName)
        {
            var prefix = categoryName.Length >= 3
                ? categoryName.Substring(0, 3).ToUpper()
                : categoryName.ToUpper();

            return $"{prefix}-";

        }
        /// <summary>
        /// Возвращает id категории по названию или создает новую категорию
        /// </summary>
        public Guid GetOrCreateCategoryId(string categoryName)
        {
            var name = categoryName.Trim();

            var category = _db.Categories.FirstOrDefault(c => c.Name == name);

            if (category == null)
            {
                category = new Category
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    IsDeleted = false
                };

                _db.Categories.Add(category);
            }
            else if (category.IsDeleted)
            {
                category.IsDeleted = false;
            }

            return category.Id;
        }

        /// <summary>
        /// Получает список неудаленных товаров с остатком на складе
        /// </summary>
        public List<Product> GetAllProducts()
        {
            return _db.Products
                .Include(p => p.Category)
                .Include(p => p.Unit)
                .Where(p => !p.IsDeleted && p.Balance > 0)
                .AsNoTracking()
                .ToList();
        }
    }
}