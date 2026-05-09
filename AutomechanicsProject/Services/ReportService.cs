using AutomechanicsProject.Classes;
using AutomechanicsProject.Services.Interfaces;
using System;
using System.Linq;

namespace AutomechanicsProject.Services
{
    /// <summary>
    /// Сервис отчётов
    /// </summary>
    public class ReportService : IReportService
    {
        private readonly DateBase _db;

        /// <summary>
        /// Конструктор
        /// </summary>
        public ReportService(DateBase db)
        {
            _db = db;
        }

        /// <summary>
        /// Получает отгрузки
        /// </summary>
        public IQueryable<Shipment> GetShipments(DateTime from, DateTime to)
        {
            return _db.Shipments
                .Where(s => s.Date >= from && s.Date <= to);
        }

        /// <summary>
        /// Получает поставки
        /// </summary>
        public IQueryable<Supply> GetSupplies(DateTime from, DateTime to)
        {
            return _db.Supplies
                .Where(s => s.DateCreated >= from && s.DateCreated <= to);
        }
    }
}