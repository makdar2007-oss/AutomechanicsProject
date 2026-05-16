using AutomechanicsProject.Classes;
using AutomechanicsProject.Enum;
using AutomechanicsProject.Services.Interfaces;
using System;
using System.Linq;

namespace AutomechanicsProject.Services
{
    
    /// <summary>
    /// Сервис для работы с просроченными товарами
    /// </summary>
    public class ExpiredProductsService : IExpiredProductsService
    {
        private readonly DateBase _db;
        private readonly ICurrentUserService _currentUserService;

        /// <summary>
        /// Создает сервис просроченных товаров
        /// </summary>
        public ExpiredProductsService(DateBase db, ICurrentUserService currentUserService)
        {
            _db = db;
            _currentUserService = currentUserService;
        }

        /// <summary>
        /// Выполняет автоматическое списание просроченных товаров
        /// </summary>
        public int AutoWriteOffExpiredProducts()
        {
            var today = MoscowTime.Today;

            var expiredProducts = _db.Products
                .Where(p => !p.IsDeleted
                    && p.ExpiryDate.HasValue
                    && p.ExpiryDate.Value <= today
                    && p.Balance > 0)
                .ToList();

            if (!expiredProducts.Any())
            {
                return 0;
            }

            if (_currentUserService.CurrentUser == null)
            {
                throw new InvalidOperationException("Текущий пользователь не найден");
            }

            foreach (var product in expiredProducts)
            {
                var quantity = product.Balance;

                var shipment = new Shipment
                {
                    Id = Guid.NewGuid(),
                    Date = MoscowTime.Now,
                    UserId = null,
                    CreatedByUserId = _currentUserService.CurrentUser.Id,
                    ShipmentType = ShipmentTypeEnum.WriteOff.ToString(),
                    TotalAmount = -(product.PurchasePrice * quantity)
                };

                _db.Shipments.Add(shipment);
                _db.SaveChanges();

                var shipmentItem = new ShipmentItem
                {
                    ShipmentId = shipment.Id,
                    ProductId = product.Id,
                    Product = product,
                    Article = product.Article,
                    ProductName = product.Name,
                    Quantity = -quantity,
                    Price = product.Price,
                    PurchasePrice = product.PurchasePrice
                };

                _db.ShipmentItems.Add(shipmentItem);
                product.Balance = 0;
            }

            _db.SaveChanges();

            return expiredProducts.Count;
        }
    }
}