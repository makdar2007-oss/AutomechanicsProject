using System;
using System.Linq;
using System.Windows.Forms;
using AutomechanicsProject.Classes;

namespace AutomechanicsProject.Services
{
    public static class ExpiredProductsService
    {
        /// <summary>
        /// Выполняет автоматическое списание просроченных товаров
        /// </summary>
        public static int AutoWriteOffExpiredProducts(DateBase db)
        {
            var today = MoscowTime.Today;

            var expiredProducts = db.Products
                .Where(p => p.ExpiryDate.HasValue
                    && p.ExpiryDate.Value <= today
                    && p.Balance > 0)
                .ToList();

            if (!expiredProducts.Any())
            {
                return 0;
            }

            var writeOffUserId = new Guid("4adf792a-247b-435d-a15e-37314224c761");
            var writeOffAddressId = new Guid("dc40ff88-af12-4841-b101-9da423f7f777");

            foreach (var product in expiredProducts)
            {
                var shipment = new Shipment
                {
                    Id = Guid.NewGuid(),
                    Date = MoscowTime.Now,
                    UserId = writeOffAddressId,
                    CreatedByUserId = writeOffUserId,
                    ShipmentType = "Списание",
                    TotalAmount = -(product.PurchasePrice * product.Balance)
                };

                db.Shipments.Add(shipment);
                db.SaveChanges();

                var shipmentItem = new ShipmentItem
                {
                    ShipmentId = shipment.Id,
                    Product = product,
                    Article = product.Article,
                    ProductName = product.Name,
                    Quantity = -product.Balance,
                    Price = product.Price,
                    PurchasePrice = 0
                };

                db.ShipmentItems.Add(shipmentItem);
                product.Balance = 0;
            }

            db.SaveChanges();

            return expiredProducts.Count;
        }
    }
}