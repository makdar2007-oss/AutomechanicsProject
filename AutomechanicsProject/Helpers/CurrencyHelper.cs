using System.Collections.Generic;
using AutomechanicsProject.Classes;

namespace AutomechanicsProject.Helpers
{
    public static class CurrencyHelper
    {
        /// <summary>
        /// Возвращает список доступных валют для выбора
        /// </summary>
        public static List<CurrencyInfo> GetCurrencies()
        {
            return new List<CurrencyInfo>
            {
                new CurrencyInfo { Code = "RUB", Rate = 1.00m, DisplayText = "RUB - Российский рубль" },
                new CurrencyInfo { Code = "USD", Rate = 0.011m, DisplayText = "USD - Доллар США" },
                new CurrencyInfo { Code = "EUR", Rate = 0.010m, DisplayText = "EUR - Евро" },
                new CurrencyInfo { Code = "CNY", Rate = 0.079m, DisplayText = "CNY - Китайский юань" },
                new CurrencyInfo { Code = "KZT", Rate = 4.90m, DisplayText = "KZT - Казахстанский тенге" },
                new CurrencyInfo { Code = "BYN", Rate = 0.036m, DisplayText = "BYN - Белорусский рубль" }
            };
        }

        /// <summary>
        /// Конвертация из выбранной валюты в рубли (для сохранения в БД)
        /// </summary>
        public static decimal ConvertToRUB(decimal priceInCurrency, decimal rate)
        {
            return priceInCurrency / rate;
        }

        /// <summary>
        /// Конвертация из рублей в выбранную валюту (для отображения)
        /// </summary>
        public static decimal ConvertFromRUB(decimal priceInRUB, decimal rate)
        {
            return priceInRUB * rate;
        }
    }
}