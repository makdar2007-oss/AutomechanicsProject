using System;

namespace AutomechanicsProject.Dtos.Service
{
    /// <summary>
    /// DTO результата отгрузки
    /// </summary>
    public class ShipmentResultDto
    {
        /// <summary>
        /// Флаг на успешность выполнения отгрузки
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Уникальный идентификатор созданной отгрузки
        /// </summary>
        public Guid ShipmentId { get; set; }

        /// <summary>
        /// Общая сумма отгрузки
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Количество позиций в отгрузке
        /// </summary>
        public int ItemsCount { get; set; }

        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}