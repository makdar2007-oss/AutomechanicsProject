using System;

namespace AutomechanicsProject.Dtos
{
    /// <summary>
    /// DTO позиции отгрузки для сервиса
    /// </summary>
    public class ShipmentItemDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string Article { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal PurchasePrice { get; set; }
    }
}