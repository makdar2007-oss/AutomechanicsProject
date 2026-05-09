using AutomechanicsProject.Classes;
using AutomechanicsProject.Properties;
using AutomechanicsProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomechanicsProject.Services
{
    /// <summary>
    /// Сервис поставок
    /// </summary>
    public class SupplyService : ISupplyService
    {
        private readonly DateBase _db;

        /// <summary>
        /// Конструктор
        /// </summary>
        public SupplyService(DateBase db)
        {
            _db = db;
        }

        /// <summary>
        /// Создаёт поставку
        /// </summary>
        public Supply CreateSupply(
            List<SupplyPosition> positions,
            Guid userId,
            string currencyCode,
            decimal exchangeRate)
        {
            using (var transaction = _db.Database.BeginTransaction())
            {
                var supply = new Supply
                {
                    Id = Guid.NewGuid(),
                    DateCreated = MoscowTime.Now,
                    UserId = userId,
                    CurrencyCode = currencyCode,
                    ExchangeRate = exchangeRate,
                    RateDate = MoscowTime.Now,
                    TotalAmount = positions.Sum(p => p.Quantity * p.Price),
                    Status = Resources.SupplyStatusCompleted,
                    OrderNumber = GenerateOrderNumber(),
                };

                _db.Supplies.Add(supply);
                _db.SaveChanges();

                foreach (var position in positions)
                {
                    position.Id = Guid.NewGuid();
                    position.SupplyId = supply.Id;

                    _db.SupplyPositions.Add(position);

                    var product = _db.Products.FirstOrDefault(p => p.Id == position.ProductId);

                    if (product != null)
                    {
                        product.Balance += position.Quantity;
                        product.Price = position.Price;

                        if (position.ExpiryDate.HasValue)
                        {
                            product.ExpiryDate = position.ExpiryDate;
                        }
                    }
                }

                _db.SaveChanges();
                transaction.Commit();
                return supply;
            }
        }
        /// <summary>
        /// Генерирует номер заказа
        /// </summary>
        private string GenerateOrderNumber()
        {
            return $"PO-{MoscowTime.Now:yyyyMMddHHmmss}-{new Random().Next(1000, 9999)}";
        }
    }


}