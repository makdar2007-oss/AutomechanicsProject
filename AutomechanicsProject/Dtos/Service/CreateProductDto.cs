using System;

namespace AutomechanicsProject.Dtos.Service
{
    /// <summary>
    /// Для создания товара 
    /// </summary>
    public class CreateProductDto
    {
        /// <summary>
        /// Артикул товара
        /// </summary>
        public string Article { get; set; }

        /// <summary>
        /// Название товара
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор категории товаров
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Идентификатор ед измерения
        /// </summary>
        public Guid UnitId { get; set; }

        /// <summary>
        /// Цена товара
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Флаг на налтичие срока годности
        /// </summary>
        public bool HasExpiryDate { get; set; }
    }
}