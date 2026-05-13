using AutomechanicsProject.Classes;
using AutomechanicsProject.Services.Interfaces;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
        /// Получает отгрузки за период
        /// </summary>
        public IQueryable<Shipment> GetShipments(DateTime from, DateTime to)
        {
            return _db.Shipments
                .Include(s => s.User)
                .Include(s => s.CreatedByUser)
                .Include(s => s.Items)
                .Where(s => s.Date >= from && s.Date < to);
        }

        /// <summary>
        /// Получает поставки за период
        /// </summary>
        public IQueryable<Supply> GetSupplies(DateTime from, DateTime to)
        {
            return _db.Supplies
                .Include(s => s.User)
                .Include(s => s.Positions)
                .Where(s => s.DateCreated >= from && s.DateCreated < to);
        }
    }
}