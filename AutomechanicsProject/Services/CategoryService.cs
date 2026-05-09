using AutomechanicsProject.Classes;
using AutomechanicsProject.Dtos.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using AutomechanicsProject.Services.Interfaces;

namespace AutomechanicsProject.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DateBase _db;

        public CategoryService(DateBase db)
        {
            _db = db;
        }

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

        public bool CategoryExists(string categoryName)
        {
            return _db.Categories
                .Any(c => c.Name.ToLower() == categoryName.ToLower());
        }

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

        public void DeleteCategory(Guid categoryId)
        {
            var category = _db.Categories.FirstOrDefault(c => c.Id == categoryId);

            if (category == null)
                throw new Exception("Категория не найдена");

            var products = _db.Products.Where(p => p.CategoryId == categoryId).ToList();

            if (products.Any())
            {
                var productIds = products.Select(p => p.Id).ToList();


                _db.Products.RemoveRange(products);
            }

            _db.Categories.Remove(category);
            _db.SaveChanges();
        }
    }
}