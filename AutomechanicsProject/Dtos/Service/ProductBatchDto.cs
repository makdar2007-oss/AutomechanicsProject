using System;

namespace AutomechanicsProject.Dtos.Service
{
    /// <summary>
    /// DTO для партии товара 
    /// </summary>
    public class ProductBatchDto
    {
        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Номер партии товара
        /// </summary>
        public string BatchNumber { get; set; }

        /// <summary>
        /// Срок годности
        /// </summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// Остаток товара 
        /// </summary>
        public int Balance { get; set; }

        /// <summary>
        /// Цена 
        /// </summary>
        public decimal Price { get; set; }
    }
}