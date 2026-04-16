using System;

namespace AutomechanicsProject.Dtos
{
    /// <summary>
    /// DTO для партии товара с разными сроками годности
    /// </summary>
    public class ProductBatchDto
    {
        public Guid ProductId { get; set; }
        public string DisplayText { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public decimal Balance { get; set; }
        public decimal Price { get; set; }
    }
}