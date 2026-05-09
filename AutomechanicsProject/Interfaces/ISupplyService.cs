using AutomechanicsProject.Classes;
using System;
using System.Collections.Generic;

namespace AutomechanicsProject.Services.Interfaces
{
    /// <summary>
    /// Сервис поставок
    /// </summary>
    public interface ISupplyService
    {
        /// <summary>
        /// Создаёт поставку
        /// </summary>
        
        Supply CreateSupply(
            List<SupplyPosition> positions,
            Guid userId,
            string currencyCode,
            decimal exchangeRate);
    }
}