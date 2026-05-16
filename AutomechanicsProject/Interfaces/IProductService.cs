using AutomechanicsProject.Classes;
using AutomechanicsProject.Dtos.Service;
using AutomechanicsProject.Dtos.UI;
using AutomechanicsProject.ViewModels;
using System;
using System.Collections.Generic;

namespace AutomechanicsProject.Services.Interfaces
{
    /// <summary>
    /// Сервис товаров
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Получает категории для списка
        /// </summary>
        List<ComboItemDto> GetCategoriesForCombo();

        /// <summary>
        /// Получает единицы измерения для списка
        /// </summary>
        List<ComboItemDto> GetUnitsForCombo();

        /// <summary>
        /// Генерирует артикул по категории
        /// </summary>
        string GenerateArticle(string categoryName);

        /// <summary>
        /// Генерирует обычный артикул
        /// </summary>
        string GenerateDefaultArticle();

        /// <summary>
        /// Добавляет товар
        /// </summary>
        void AddProduct(CreateProductDto dto);

        /// <summary>
        /// Получает товар по id
        /// </summary>
        Product GetProductById(Guid id);

        /// <summary>
        /// Получает товар по артикулу
        /// </summary>
        Product GetProductByArticle(string article);

        /// <summary>
        /// Удаляет товар
        /// </summary>
        void DeleteProduct(Guid productId);

        /// <summary>
        /// Получает товары для отгрузки
        /// </summary>
        List<ProductComboViewModel> GetProductsForShipment();

        /// <summary>
        /// Получает все товары
        /// </summary>
        List<Product> GetAllProducts();

        /// <summary>
        /// Обновляет товар
        /// </summary>
        void UpdateProduct(Product product);

        /// <summary>
        /// Возвращает id категории по названию или создает новую категорию
        /// </summary>
        Guid GetOrCreateCategoryId(string categoryName);
    }
}