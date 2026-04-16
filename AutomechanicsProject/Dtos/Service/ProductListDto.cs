using System;

namespace AutomechanicsProject.Dtos.Service
{
    /// <summary>
    /// Для списка товаров 
    /// </summary>
    public class ProductListItemDto
    {
        public Guid Id { get; set; }
        public string Article { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string UnitName { get; set; }
        public int Balance { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public decimal Price { get; set; }
        public decimal PurchasePrice { get; set; }
        public bool RequiresDiscount { get; set; }
        public bool IsExpired { get; set; }
    }
}