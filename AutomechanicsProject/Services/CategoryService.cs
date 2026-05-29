using AutomechanicsProject.Classes;
using AutomechanicsProject.Dtos.UI;
using AutomechanicsProject.Properties;
using AutomechanicsProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomechanicsProject.Services
{
    /// <summary>
    /// Сервис для работы с категориями товаров
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly IDateBaseContext _db;
        private readonly IWarehouseHeatmapService _warehouseHeatmapService;

        /// <summary>
        /// Создает сервис категорий
        /// </summary>
        public CategoryService(IDateBaseContext db, IWarehouseHeatmapService warehouseHeatmapService)
        {
            _db = db;
            _warehouseHeatmapService = warehouseHeatmapService ?? throw new ArgumentNullException(nameof(warehouseHeatmapService));
        }

        /// <summary>
        /// Возвращает список неудаленных категорий для выпадающего списка
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
        /// Возвращает список неудаленных категорий с количеством неудаленных товаров для выпадающего списка
        /// </summary>
        public List<ComboItemDto> GetCategoriesWithProductCountForCombo()
        {
            return _db.Categories
                .Where(c => !c.IsDeleted)
                .OrderBy(c => c.Name)
                .Select(c => new ComboItemDto
                {
                    Id = c.Id,
                    Text = string.Format(
                        Resources.CategoryDisplayFormat_WithCount,c.Name,_db.Products.Count(p => p.CategoryId == c.Id && !p.IsDeleted))
                })
                .ToList();
        }

        /// <summary>
        /// Возвращает название категории по id
        /// </summary>
        public string GetCategoryNameById(Guid categoryId)
        {
            var category = _db.Categories.FirstOrDefault(c => c.Id == categoryId);

            if (category == null)
            {
                throw new Exception(Resources.ErrorCategoryNotFound);
            }

            return category.Name;
        }
        /// <summary>
        /// Возвращает признак металлолома категории по id
        /// </summary>
        public bool GetCategoryIsScrapMetalById(Guid categoryId)
        {
            var category = _db.Categories.FirstOrDefault(c => c.Id == categoryId);

            if (category == null)
            {
                throw new Exception(Resources.ErrorCategoryNotFound);
            }

            return category.IsScrapMetal;
        }
        /// <summary>
        /// Возвращает количество неудаленных товаров в категории
        /// </summary>
        public int GetProductsCountByCategory(Guid categoryId)
        {
            return _db.Products.Count(p => p.CategoryId == categoryId && !p.IsDeleted);
        }

        /// <summary>
        /// Проверяет, существует ли неудаленная категория с таким названием
        /// </summary>
        public bool CategoryExists(string categoryName)
        {
            var name = categoryName.Trim().ToLower();

            return _db.Categories
                .Any(c => c.Name.ToLower() == name && !c.IsDeleted);
        }

        /// <summary>
        /// Добавляет новую категорию или восстанавливает удаленную
        /// </summary>
        public void AddCategory(string categoryName)
        {
            AddCategory(categoryName, false);
        }

        /// <summary>
        /// Добавляет новую категорию или восстанавливает удаленную с признаком металлолома
        /// </summary>
        public void AddCategory(string categoryName, bool isScrapMetal)
        {
            var name = categoryName.Trim();

            var category = _db.Categories
                .FirstOrDefault(c => c.Name.ToLower() == name.ToLower());

            if (category != null)
            {
                if (!category.IsDeleted)
                {
                    throw new Exception(Resources.ErrorCategoryExists);
                }

                category.IsDeleted = false;
                category.IsScrapMetal = isScrapMetal;

                _db.SaveChanges();
                return;
            }

            category = new Category
            {
                Id = Guid.NewGuid(),
                Name = name,
                IsScrapMetal = isScrapMetal,
                IsDeleted = false
            };

            _db.Categories.Add(category);
            _db.SaveChanges();
        }

        /// <summary>
        /// Изменяет название выбранной неудаленной категории
        /// </summary>
        public void EditCategory(Guid categoryId, string newName)
        {
            EditCategory(categoryId, newName, false);
        }

        /// <summary>
        /// Изменяет выбранную неудаленную категорию с признаком металлолома
        /// </summary>
        public void EditCategory(Guid categoryId, string newName, bool isScrapMetal)
        {
            var category = _db.Categories
                .FirstOrDefault(c => c.Id == categoryId && !c.IsDeleted);

            if (category == null)
            {
                throw new Exception(Resources.ErrorCategoryNotFound);
            }

            var name = newName.Trim();

            var nameExists = _db.Categories.Any(c =>
                c.Name == name &&
                c.Id != categoryId &&
                !c.IsDeleted);

            if (nameExists)
            {
                throw new Exception(Resources.ErrorCategoryExists);
            }

            category.Name = name;
            category.IsScrapMetal = isScrapMetal;

            _db.SaveChanges();
        }

        /// <summary>
        /// Помечает категорию и ее товары как удаленные
        /// </summary>
        public void DeleteCategory(Guid categoryId)
        {
            var category = _db.Categories
                .FirstOrDefault(c => c.Id == categoryId && !c.IsDeleted);

            if (category == null)
            {
                throw new Exception(Resources.ErrorCategoryNotFound);
            }

            var products = _db.Products
                .Where(p => p.CategoryId == categoryId && !p.IsDeleted)
                .ToList();

            foreach (var product in products)
            {
                product.IsDeleted = true;
            }

            category.IsDeleted = true;

            _db.SaveChanges();

            _warehouseHeatmapService.FreeDeletedProductCells();
        }
    }
}