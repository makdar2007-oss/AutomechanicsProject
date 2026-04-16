using System;

namespace AutomechanicsProject.Dtos.Service
{
    /// <summary>
    /// Для обновления товара
    /// </summary>
    public class UpdateProductDto
    {
        public Guid Id { get; set; }
        public string Article { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public Guid UnitId { get; set; }
        public decimal PurchasePrice { get; set; }
    }
}