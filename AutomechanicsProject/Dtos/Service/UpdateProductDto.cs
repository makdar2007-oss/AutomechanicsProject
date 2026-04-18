using System;

namespace AutomechanicsProject.Dtos.Service
{
    /// <summary>
    /// Для обновления товара
    /// </summary>
    public class UpdateProductDto
    {
        /// <summary>
        /// Уникальный идентификатор товара
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Артикул
        /// </summary>
        public string Article { get; set; }

        /// <summary>
        /// Название товара
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Уникальный идентификатор категории
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Уникальный идентификатор ед измерения
        /// </summary>
        public Guid UnitId { get; set; }

        /// <summary>
        /// Цена товара
        /// </summary>
        public decimal PurchasePrice { get; set; }
    }
}