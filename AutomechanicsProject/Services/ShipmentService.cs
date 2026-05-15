using AutomechanicsProject.Classes;
using AutomechanicsProject.Enum;
using AutomechanicsProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutomechanicsProject.Dtos;
using AutomechanicsProject.Dtos.UI;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Properties;
using AutomechanicsProject.ViewModels;

namespace AutomechanicsProject.Services
{
    /// <summary>
    /// Сервис отгрузок
    /// </summary>
    public class ShipmentService : IShipmentService
    {
        private readonly DateBase _db;
        private readonly IWarehouseHeatmapService _warehouseHeatmapService;

        /// <summary>
        /// Создает сервис отгрузок
        /// </summary>
        public ShipmentService(DateBase db, IWarehouseHeatmapService warehouseHeatmapService)
        {
            _db = db;
            _warehouseHeatmapService = warehouseHeatmapService ?? throw new ArgumentNullException(nameof(warehouseHeatmapService));
        }

        /// <summary>
        /// Получает неудаленные товары для формы отгрузки
        /// </summary>
        public List<ProductComboViewModel> GetProductsForShipment()
        {
            var productsList = _db.Products
                .Where(p => !p.IsDeleted && p.Balance > 0)
                .Select(p => new
                {
                    p.Id,
                    p.Article,
                    p.Name,
                    p.Balance,
                    p.Price,
                    UnitName = p.Unit != null ? p.Unit.Name : Resources.Unit_Piece_Short,
                    p.UnitId,
                    p.IsMetal
                })
                .ToList();

            return productsList
                .GroupBy(p => p.Name)
                .Select(g => new ProductComboViewModel
                {
                    Id = g.First().Id,
                    Text = FormatHelper.FormatProductWithBalance(
                        g.First().Article,
                        g.Key,
                        g.Sum(x => x.Balance),
                        g.First().UnitName),
                    Article = g.First().Article,
                    Name = g.Key,
                    Price = g.First().Price,
                    Balance = g.Sum(x => x.Balance),
                    UnitName = g.First().UnitName,
                    UnitId = g.First().UnitId,
                    IsMetal = g.First().IsMetal
                })
                .ToList();
        }

        /// <summary>
        /// Получает получателей для выпадающего списка
        /// </summary>
        public List<ComboItemDto> GetRecipientsForCombo()
        {
            var excludedNames = new[]
            {
        Resources.ShipmentType_Defect,
        Resources.ShipmentType_WriteOff,
        "-",
        ""
    };

            return _db.Addresses
                .Where(a => a.CompanyName != null &&
                            !excludedNames.Contains(a.CompanyName.Trim()))
                .OrderBy(a => a.CompanyName)
                .Select(a => new ComboItemDto
                {
                    Id = a.Id,
                    Text = a.CompanyName
                })
                .ToList();
        }

        /// <summary>
        /// Получает сроки годности для выбранного неудаленного товара
        /// </summary>
        public List<ExpiryItemDto> GetExpiryDatesForProduct(Guid productId)
        {
            var selectedProduct = _db.Products
                .FirstOrDefault(p => p.Id == productId && !p.IsDeleted);

            if (selectedProduct == null)
            {
                return new List<ExpiryItemDto>();
            }

            return _db.Products
                .Where(p => !p.IsDeleted &&
                            p.Name == selectedProduct.Name &&
                            p.Balance > 0)
                .Select(p => new ExpiryItemDto
                {
                    ProductId = p.Id,
                    DisplayText = FormatHelper.FormatExpiryDateDisplay(p.ExpiryDate, p.Balance),
                    ExpiryDate = p.ExpiryDate,
                    Balance = p.Balance
                })
                .OrderBy(p => p.ExpiryDate)
                .ToList();
        }

       
        /// <summary>
        /// Получает неудаленный товар для отгрузки по id
        /// </summary>
        public Product GetProductForShipmentById(Guid productId)
        {
            return _db.Products
                .Include(p => p.Unit)
                .FirstOrDefault(p => p.Id == productId && !p.IsDeleted);
        }

        /// <summary>
        /// Получает неудаленный товар для отгрузки по названию
        /// </summary>
        public Product GetProductForShipmentByName(string productName)
        {
            return _db.Products
                .Include(p => p.Unit)
                .FirstOrDefault(p => p.Name == productName &&
                                     !p.IsDeleted &&
                                     p.Balance > 0);
        }

        /// <summary>
        /// Проверяет, является ли неудаленный товар металлом
        /// </summary>
        public bool IsProductMetal(Guid productId)
        {
            var product = _db.Products.FirstOrDefault(p => p.Id == productId && !p.IsDeleted);

            return product?.IsMetal ?? false;
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
                    if (!item.ProductId.HasValue)
                    {
                        throw new Exception("Товар не найден");
                    }

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

                    var product = _db.Products.FirstOrDefault(p => p.Id == item.ProductId.Value && !p.IsDeleted);

                    if (product == null)
                    {
                        throw new Exception("Товар не найден или был удален");
                    }

                    product.Balance -= Math.Abs(item.Quantity);
                }

                _db.SaveChanges();
                transaction.Commit();
            }

            foreach (var item in items)
            {
                if (item.ProductId.HasValue)
                {
                    _warehouseHeatmapService.EnsureProductHasCell(item.ProductId.Value);
                }
            }
        }
        /// <summary>
        /// Возвращает отгрузки за период для истории
        /// </summary>
        public List<Shipment> GetShipmentsForHistory(DateTime from, DateTime to)
        {
            return _db.Shipments
                .Include(s => s.User)
                .Include(s => s.CreatedByUser)
                .Include(s => s.Items)
                .Where(s => s.Date >= from && s.Date <= to)
                .OrderByDescending(s => s.Date)
                .ToList();
        }
    }
}