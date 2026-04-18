using System;

namespace AutomechanicsProject.Dtos.Service
{
    /// <summary>
    /// Для списка товаров 
    /// </summary>
    public class ProductListItemDto
    {
        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Артикул товара
        /// </summary>
        public string Article { get; set; }

        /// <summary>
        /// Название товара
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Название категории
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Ед измерения
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// Остаток
        /// </summary>
        public int Balance { get; set; }

        /// <summary>
        /// Срок годности
        /// </summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Цена товара
        /// </summary>
        public decimal PurchasePrice { get; set; }

        /// <summary>
        /// Требуется ли скидка
        /// </summary>
        public bool RequiresDiscount { get; set; }

        /// <summary>
        /// Списан ли товар
        /// </summary>
        public bool IsExpired { get; set; }
    }
}