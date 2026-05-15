using AutomechanicsProject.Classes;
using AutomechanicsProject.Dtos.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using AutomechanicsProject.Services.Interfaces;

namespace AutomechanicsProject.Services
{
    /// <summary>
    /// Сервис для работы с категориями товаров
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly DateBase _db;
        private readonly IWarehouseHeatmapService _warehouseHeatmapService;

        /// <summary>
        /// Создает сервис категорий
        /// </summary>
        public CategoryService(DateBase db, IWarehouseHeatmapService warehouseHeatmapService)
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
                    Text = $"{c.Name} (товаров: {_db.Products.Count(p => p.CategoryId == c.Id && !p.IsDeleted)})"
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
                throw new Exception("Категория не найдена");
            }

            return category.Name;
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
            var name = categoryName.Trim();

            var category = _db.Categories
                .FirstOrDefault(c => c.Name.ToLower() == name.ToLower());

            if (category != null)
            {
                if (!category.IsDeleted)
                {
                    throw new Exception("Категория с таким названием уже существует");
                }

                category.IsDeleted = false;
                _db.SaveChanges();
                return;
            }

            category = new Category
            {
                Id = Guid.NewGuid(),
                Name = name,
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
            var category = _db.Categories
                .FirstOrDefault(c => c.Id == categoryId && !c.IsDeleted);

            if (category == null)
            {
                throw new Exception("Категория не найдена");
            }

            var name = newName.Trim();

            var nameExists = _db.Categories.Any(c =>
                c.Name == name &&
                c.Id != categoryId &&
                !c.IsDeleted);

            if (nameExists)
            {
                throw new Exception("Категория с таким названием уже существует");
            }

            category.Name = name;
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
                throw new Exception("Категория не найдена");
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