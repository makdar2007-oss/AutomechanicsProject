using System;

namespace AutomechanicsProject.Dtos
{
    /// <summary>
    /// DTO результата создания отгрузки
    /// </summary>
    public class ShipmentResultDto
    {
        public bool Success { get; set; }
        public Guid ShipmentId { get; set; }
        public decimal TotalAmount { get; set; }
        public int ItemsCount { get; set; }
        public string ErrorMessage { get; set; }
    }
}