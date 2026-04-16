using System;

namespace AutomechanicsProject.Dtos.Service
{
    /// <summary>
    /// DTO для партии товара 
    /// </summary>
    public class ProductBatchDto
    {
        public Guid ProductId { get; set; }
        public string BatchNumber { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int Balance { get; set; }
        public decimal Price { get; set; }
    }
}