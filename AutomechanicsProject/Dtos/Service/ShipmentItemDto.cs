using System;

namespace AutomechanicsProject.Dtos.Service
{
    /// <summary>
    /// DTO позиции отгрузки
    /// </summary>
    public class ShipmentItemDto
    {
        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Наименование товара
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Артикул
        /// </summary>
        public string Article { get; set; }

        /// <summary>
        /// Количество товаров в отгрузке
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Закупочная цена товара
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Цена товара
        /// </summary>
        public decimal PurchasePrice { get; set; }
    }
}