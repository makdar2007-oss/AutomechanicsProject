using System;

namespace AutomechanicsProject.Dtos.Service
{
    /// <summary>
    /// DTO для передачи данных о товаре между слоями
    /// </summary>
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Article { get; set; }
        public string CategoryName { get; set; }
        public Guid CategoryId { get; set; }
        public string UnitName { get; set; }
        public Guid UnitId { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal Price { get; set; }
        public int Balance { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string BatchNumber { get; set; }
    }
}