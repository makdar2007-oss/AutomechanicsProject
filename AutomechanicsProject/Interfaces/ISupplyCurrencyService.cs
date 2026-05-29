using System;

namespace AutomechanicsProject.Services.Interfaces
{
    /// <summary>
    /// Сервис для работы с валютой поставок
    /// </summary>
    public interface ISupplyCurrencyService
    {
        /// <summary>
        /// Получает информацию о валюте для товара по его ID
        /// </summary>
        (string CurrencyCode, decimal ExchangeRate, DateTime RateDate) GetProductCurrency(Guid productId);

        /// <summary>
        /// Получает текст для подсказки при наведении на товар
        /// </summary>
        string GetTooltipText(Guid productId, string productName);
    }
}