using System;

namespace AutomechanicsProject.Dtos.Service
{
    /// <summary>
    /// Для создания товара 
    /// </summary>
    public class CreateProductDto
    {
        public string Article { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public Guid UnitId { get; set; }
        public decimal Price { get; set; }
        public bool HasExpiryDate { get; set; }
    }
}