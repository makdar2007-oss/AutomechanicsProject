using AutomechanicsProject.Classes;
using AutomechanicsProject.Formes;
using NLog;
using System;
using System.Linq;

namespace AutomechanicsProject.Services
{
    public static class SupplyCurrencyService
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Получает информацию о валюте для товара по его ID
        /// </summary>
        public static (string CurrencyCode, decimal ExchangeRate, DateTime RateDate) GetProductCurrency(Guid productId, DateBase db)
        {
            try
            {
                var lastSupply = db.SupplyPositions
                    .Where(sp => sp.ProductId == productId)
                    .OrderByDescending(sp => sp.Supply.DateCreated)
                    .Select(sp => sp.Supply)
                    .FirstOrDefault();

                if (lastSupply != null)
                {
                    return (lastSupply.CurrencyCode, lastSupply.ExchangeRate, lastSupply.RateDate);
                }
            }
            catch (Exception)
            {
                logger.Error($"Ошибка получения валюты для товара {productId}");
            }

            return ("RUB", 1.00m, DateTime.Now);
        }

        /// <summary>
        /// Получает текст для ToolTip при наведении на товар
        /// </summary>
        public static string GetTooltipText(Guid productId, string productName, DateBase db)
        {
            var (currency, rate, date) = GetProductCurrency(productId, db);

            if (currency == "RUB" && rate == 1.00m && date == DateTime.Now)
            {
                var hasSupply = db.SupplyPositions.Any(sp => sp.ProductId == productId);
                if (!hasSupply)
                {
                    return $"{productName}\nНет данных о поставках\nЦена отображается в {ChoosingCurrency.SelectedCurrencyCode}";
                }
            }

            if (currency == "RUB" || rate == 1.00m)
            {
                return $"{productName}\nЗакупка в рублях\nТекущая валюта: {ChoosingCurrency.SelectedCurrencyCode}";
            }

            return $"{productName}\n" +
                   $"Валюта поставки: {currency}\n" +
                   $"Курс на момент поставки: 1 RUB = {rate:F4} {currency}\n";
        }
    }
}