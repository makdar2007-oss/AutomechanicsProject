using System;

namespace AutomechanicsProject.Dtos.Service
{
    /// <summary>
    /// DTO для передачи данных о товаре между слоями
    /// </summary>
    public class ProductDto
    {
        /// <summary>
        /// Уникальный идентификатор товара
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название товара
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Артикул товара
        /// </summary>
        public string Article { get; set; }

        /// <summary>
        /// Название категории
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Идентификатор категории товаров
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Название ед измерения
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// Идентификатор ед измерения
        /// </summary>
        public Guid UnitId { get; set; }

        /// <summary>
        /// Цена товара
        /// </summary>
        public decimal PurchasePrice { get; set; }

        /// <summary>
        /// Закупочная цена
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Остаток
        /// </summary>
        public int Balance { get; set; }

        /// <summary>
        /// Срок годности
        /// </summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// Номер партии
        /// </summary>
        public string BatchNumber { get; set; }
    }
}