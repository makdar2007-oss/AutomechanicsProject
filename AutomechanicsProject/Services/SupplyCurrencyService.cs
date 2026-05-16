using AutomechanicsProject.Classes;
using AutomechanicsProject.Formes;
using AutomechanicsProject.Services.Interfaces;
using NLog;
using System;
using System.Linq;

namespace AutomechanicsProject.Services
{
    /// <summary>
    /// Сервис для работы с валютой поставок
    /// </summary>
    public class SupplyCurrencyService : ISupplyCurrencyService
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly DateBase _db;
        private readonly ICurrencySettingsService _currencySettingsService;

        /// <summary>
        /// Создает сервис валюты поставок
        /// </summary>
        public SupplyCurrencyService(DateBase db, ICurrencySettingsService currencySettingsService)
        {
            _db = db;
            _currencySettingsService = currencySettingsService;
        }

        /// <summary>
        /// Получает информацию о валюте для товара по его ID
        /// </summary>
        public (string CurrencyCode, decimal ExchangeRate, DateTime RateDate) GetProductCurrency(Guid productId)
        {
            try
            {
                var lastSupply = _db.SupplyPositions
                    .Where(sp => sp.ProductId == productId)
                    .OrderByDescending(sp => sp.Supply.DateCreated)
                    .Select(sp => sp.Supply)
                    .FirstOrDefault();

                if (lastSupply != null)
                {
                    return (lastSupply.CurrencyCode, lastSupply.ExchangeRate, lastSupply.RateDate);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Ошибка получения валюты для товара {productId}");
            }

            return (CurrencyCodes.RUB, 1.00m, MoscowTime.Now);
        }

        /// <summary>
        /// Получает текст для подсказки при наведении на товар
        /// </summary>
        public string GetTooltipText(Guid productId, string productName)
        {
            var (currency, rate, date) = GetProductCurrency(productId);

            if (currency == CurrencyCodes.RUB && rate == 1.00m && date == MoscowTime.Now)
            {
                var hasSupply = _db.SupplyPositions.Any(sp => sp.ProductId == productId);

                if (!hasSupply)
                {
                    return $"{productName}\nНет данных о поставках\nЦена отображается в {_currencySettingsService.SelectedCurrencyCode}";
                }
            }

            if (currency == CurrencyCodes.RUB || rate == 1.00m)
            {
                return $"{productName}\nЗакупка в рублях\nТекущая валюта: {_currencySettingsService.SelectedCurrencyCode}";
            }

            return $"{productName}\n" +
                   $"Валюта поставки: {currency}\n" +
                   $"Курс на момент поставки: 1 RUB = {rate:F4} {currency}\n";
        }
    }
}