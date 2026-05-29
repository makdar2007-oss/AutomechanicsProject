using AutomechanicsProject.Classes;
using AutomechanicsProject.Enum;
using System;
using System.Collections.Generic;
using AutomechanicsProject.Dtos;
using AutomechanicsProject.Dtos.UI;
using AutomechanicsProject.ViewModels;

namespace AutomechanicsProject.Services.Interfaces
{
    /// <summary>
    /// Сервис отгрузок
    /// </summary>
    public interface IShipmentService
    {
        /// <summary>
        /// Получает товары для формы отгрузки
        /// </summary>
        List<ProductComboViewModel> GetProductsForShipment();

        /// <summary>
        /// Получает получателей для выпадающего списка
        /// </summary>
        List<ComboItemDto> GetRecipientsForCombo();

        /// <summary>
        /// Получает сроки годности для выбранного товара
        /// </summary>
        List<ExpiryItemDto> GetExpiryDatesForProduct(Guid productId);

        /// <summary>
        /// Получает товар для отгрузки по id
        /// </summary>
        Product GetProductForShipmentById(Guid productId);

        /// <summary>
        /// Получает товар для отгрузки по названию
        /// </summary>
        Product GetProductForShipmentByName(string productName);

        /// <summary>
        /// Проверяет, является ли товар металлом
        /// </summary>
        bool IsProductMetal(Guid productId);

        /// <summary>
        /// Получает термоконтейнер для отгрузки
        /// </summary>
        Product GetThermoContainerProduct();

        /// <summary>
        /// Проверяет, хватает ли термоконтейнеров на складе
        /// </summary>
        bool HasEnoughThermoContainers(int count);
        /// <summary>
        /// Создаёт отгрузку
        /// </summary>
        void CreateShipment(
            List<ShipmentItem> items,
            Guid? recipientId,
            Guid createdByUserId,
            decimal totalAmount,
            ShipmentTypeEnum shipmentType);

        /// <summary>
        /// Возвращает отгрузки за период для истории
        /// </summary>
        List<Shipment> GetShipmentsForHistory(DateTime from, DateTime to);
    }
}