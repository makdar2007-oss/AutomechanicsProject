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

        /// <summary>
        /// Создает сервис категорий
        /// </summary>
        public CategoryService(DateBase db)
        {
            _db = db;
        }

        /// <summary>
        /// Возвращает список категорий для выпадающего списка
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
        /// Возвращает список категорий с количеством товаров для выпадающего списка
        /// </summary>
        public List<ComboItemDto> GetCategoriesWithProductCountForCombo()
        {
            return _db.Categories
                .OrderBy(c => c.Name)
                .Select(c => new ComboItemDto
                {
                    Id = c.Id,
                    Text = $"{c.Name} (товаров: {_db.Products.Count(p => p.CategoryId == c.Id)})"
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
        /// Возвращает количество товаров в категории
        /// </summary>
        public int GetProductsCountByCategory(Guid categoryId)
        {
            return _db.Products.Count(p => p.CategoryId == categoryId);
        }
        /// <summary>
        /// Проверяет, существует ли категория с таким названием
        /// </summary>
        public bool CategoryExists(string categoryName)
        {
            return _db.Categories
                .Any(c => c.Name.ToLower() == categoryName.ToLower());
        }

        /// <summary>
        /// Добавляет новую категорию
        /// </summary>
        public void AddCategory(string categoryName)
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = categoryName.Trim()
            };

            _db.Categories.Add(category);
            _db.SaveChanges();
        }

        /// <summary>
        /// Изменяет название выбранной категории
        /// </summary>
        public void EditCategory(Guid categoryId, string newName)
        {
            var category = _db.Categories.FirstOrDefault(c => c.Id == categoryId);

            if (category == null)
                throw new Exception("Категория не найдена");

            bool nameExists = _db.Categories.Any(c => c.Name == newName && c.Id != categoryId);

            if (nameExists)
                throw new Exception("Категория с таким названием уже существует");

            category.Name = newName.Trim();
            _db.SaveChanges();
        }

        /// <summary>
        /// Удаляет выбранную категорию
        /// </summary>
        public void DeleteCategory(Guid categoryId)
        {
            var category = _db.Categories.FirstOrDefault(c => c.Id == categoryId);

            if (category == null)
            {
                throw new Exception("Категория не найдена");
            }

            var products = _db.Products
                .Where(p => p.CategoryId == categoryId)
                .ToList();

            if (products.Any())
            {
                _db.Products.RemoveRange(products);
            }

            _db.Categories.Remove(category);
            _db.SaveChanges();
        }
    }
}