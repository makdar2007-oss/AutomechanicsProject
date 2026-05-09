using AutomechanicsProject.Classes;
using AutomechanicsProject.Enum;
using AutomechanicsProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomechanicsProject.Services
{
    /// <summary>
    /// Сервис отгрузок
    /// </summary>
    public class ShipmentService : IShipmentService
    {
        private readonly DateBase _db;

        /// <summary>
        /// Конструктор
        /// </summary>
        public ShipmentService(DateBase db)
        {
            _db = db;
        }

        /// <summary>
        /// Создаёт отгрузку
        /// </summary>
        public void CreateShipment(
            List<ShipmentItem> items,
            Guid? recipientId,
            Guid createdByUserId,
            decimal totalAmount,
            ShipmentTypeEnum shipmentType)
        {
            using (var transaction = _db.Database.BeginTransaction())
            {
                var shipment = new Shipment
                {
                    Id = Guid.NewGuid(),
                    Date = MoscowTime.Now,
                    UserId = recipientId,
                    CreatedByUserId = createdByUserId,
                    TotalAmount = totalAmount,
                    ShipmentType = shipmentType.ToString()
                };

                _db.Shipments.Add(shipment);
                _db.SaveChanges();

                foreach (var item in items)
                {
                    var shipmentItem = new ShipmentItem
                    {
                        Id = Guid.NewGuid(),
                        ShipmentId = shipment.Id,
                        ProductId = item.ProductId,
                        Quantity = shipmentType == ShipmentTypeEnum.Shipment
                            ? item.Quantity
                            : -Math.Abs(item.Quantity),
                        Price = item.Price,
                        PurchasePrice = shipmentType == ShipmentTypeEnum.WriteOff
                            ? 0
                            : item.PurchasePrice,
                        ProductName = item.ProductName,
                        Article = item.Article,
                        ScrapReturn = shipmentType == ShipmentTypeEnum.Defect
                            ? item.ScrapReturn
                            : 0
                    };

                    _db.ShipmentItems.Add(shipmentItem);

                    var product = _db.Products.Find(item.ProductId);

                    if (product != null)
                    {
                        product.Balance -= Math.Abs(item.Quantity);
                    }
                }

                _db.SaveChanges();
                transaction.Commit();
            }
        }
    }
}