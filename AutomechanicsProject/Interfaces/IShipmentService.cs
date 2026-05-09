using AutomechanicsProject.Classes;
using AutomechanicsProject.Enum;
using System;
using System.Collections.Generic;

namespace AutomechanicsProject.Services.Interfaces
{
    /// <summary>
    /// Сервис отгрузок
    /// </summary>
    public interface IShipmentService
    {
        /// <summary>
        /// Создаёт отгрузку
        /// </summary>
        void CreateShipment(
            List<ShipmentItem> items,
            Guid? recipientId,
            Guid createdByUserId,
            decimal totalAmount,
            ShipmentTypeEnum shipmentType);
    }
}