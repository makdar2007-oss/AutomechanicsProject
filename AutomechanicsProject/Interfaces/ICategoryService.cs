using AutomechanicsProject.Dtos.UI;
using System;
using System.Collections.Generic;

namespace AutomechanicsProject.Services.Interfaces
{
    /// <summary>
    /// Сервис категорий
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Возвращает список категорий для выпадающего списка
        /// </summary>
        List<ComboItemDto> GetCategoriesForCombo();

        /// <summary>
        /// Возвращает список категорий с количеством товаров для выпадающего списка
        /// </summary>
        List<ComboItemDto> GetCategoriesWithProductCountForCombo();

        /// <summary>
        /// Возвращает название категории по идентификатору
        /// </summary>
        string GetCategoryNameById(Guid categoryId);

        /// <summary>
        /// Возвращает количество товаров в категории
        /// </summary>
        int GetProductsCountByCategory(Guid categoryId);

        /// <summary>
        /// Проверяет существование категории
        /// </summary>
        bool CategoryExists(string categoryName);

        /// <summary>
        /// Добавляет категорию
        /// </summary>
        void AddCategory(string categoryName);

        /// <summary>
        /// Изменяет название выбранной категории
        /// </summary>
        void EditCategory(Guid categoryId, string newName);

        /// <summary>
        /// Удаляет выбранную категорию
        /// </summary>
        void DeleteCategory(Guid categoryId);
    }
}