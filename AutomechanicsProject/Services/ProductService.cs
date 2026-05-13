using AutomechanicsProject.Classes;
using AutomechanicsProject.Dtos.Service;
using AutomechanicsProject.Dtos.UI;
using AutomechanicsProject.Mappers;
using AutomechanicsProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using AutomechanicsProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AutomechanicsProject.Services
{
    /// <summary>
    /// Сервис для работы с товарами
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly DateBase _db;

        /// <summary>
        /// Конструктор сервиса
        /// </summary>
        public ProductService(DateBase db)
        {
            _db = db;
        }

        /// <summary>
        /// Получает список категорий для ComboBox
        /// </summary>
        public List<ComboItemDto> GetCategoriesForCombo()
        {
            return _db.Categories
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
            string prefix = GetCategoryPrefix(categoryName);

            var lastProduct = _db.Products
                .Where(p => p.Article.StartsWith(prefix))
                .OrderByDescending(p => p.Article)
                .FirstOrDefault();

            if (lastProduct != null)
            {
                string numberPart = lastProduct.Article.Substring(prefix.Length);

                if (int.TryParse(numberPart, out int number))
                    return $"{prefix}{(number + 1):D4}";
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
                string numberPart = lastProduct.Article.Substring(4);

                if (int.TryParse(numberPart, out int number))
                    return $"ART-{(number + 1):D4}";
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
        /// Получает товар по id
        /// </summary>
        public Product GetProductById(Guid id)
        {
            return _db.Products
                .Include(p => p.Category)
                .Include(p => p.Unit)
                .FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Получает товар по артикулу
        /// </summary>
        public Product GetProductByArticle(string article)
        {
            return _db.Products
                .Include(p => p.Category)
                .Include(p => p.Unit)
                .FirstOrDefault(p => p.Article == article.Trim());
        }

        /// <summary>
        /// Удаляет товар
        /// </summary>
        public void DeleteProduct(Guid productId)
        {
            var product = _db.Products.FirstOrDefault(p => p.Id == productId);

            if (product == null)
                throw new Exception("Товар не найден");


            _db.Products.Remove(product);
            _db.SaveChanges();
        }

        /// <summary>
        /// Получает товары для отгрузки
        /// </summary>
        public List<ProductComboViewModel> GetProductsForShipment()
        {
            var productsList = _db.Products
                .Where(p => p.Balance > 0)
                .Select(p => new
                {
                    p.Id,
                    p.Article,
                    p.Name,
                    p.Balance,
                    p.Price,
                    UnitName = p.Unit != null ? p.Unit.Name : "шт",
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
            string prefix = categoryName.Length >= 3
                ? categoryName.Substring(0, 3).ToUpper()
                : categoryName.ToUpper();

            return $"{prefix}-";

        }
        /// <summary>
        /// Возвращает id категории по названию или создает новую категорию
        /// </summary>
        public Guid GetOrCreateCategoryId(string categoryName)
        {
            var category = _db.Categories.FirstOrDefault(c => c.Name == categoryName);

            if (category == null)
            {
                category = new Category
                {
                    Id = Guid.NewGuid(),
                    Name = categoryName
                };

                _db.Categories.Add(category);
            }

            return category.Id;
        }
        /// <summary>
        /// Получает список товаров с остатком на складе
        /// </summary>
        public List<Product> GetAllProducts()
        {
            return _db.Products
                .Include(p => p.Category)
                .Include(p => p.Unit)
                .Where(p => p.Balance > 0)
                .AsNoTracking()
                .ToList();
        

        }
    }
}