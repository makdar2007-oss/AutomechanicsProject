using AutomechanicsProject.Classes;
using System;
using System.Linq;

namespace AutomechanicsProject.Services.Interfaces
{
    /// <summary>
    /// Сервис отчётов
    /// </summary>
    public interface IReportService
    {
        /// <summary>
        /// Получает отгрузки за период
        /// </summary>
        IQueryable<Shipment> GetShipments(DateTime from, DateTime to);

        /// <summary>
        /// Получает поставки за период
        /// </summary>
        IQueryable<Supply> GetSupplies(DateTime from, DateTime to);
    }
}