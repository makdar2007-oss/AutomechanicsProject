using System;

namespace AutomechanicsProject.Classes
{
    /// <summary>
    /// Управление валютой и конвертация цен
    /// </summary>
    public static class CurrencyManager
    {
        public static string SelectedCurrency { get; private set; } = CurrencyCodes.RUB;
        public static decimal CurrentRate { get; private set; } = 1m;

        /// <summary>
        /// Устанавливает новую валюту и её курс
        /// </summary>
        public static void SetCurrency(string currencyCode, decimal rate)
        {
            SelectedCurrency = currencyCode;
            CurrentRate = rate;
        }

        /// <summary>
        /// Конвертирует цену из рублей в выбранную валюту
        /// </summary>
        public static decimal ConvertPrice(decimal priceInRub)
        {
            if (SelectedCurrency == CurrencyCodes.RUB)
            {
                return priceInRub;
            }

            return priceInRub * CurrentRate;
        }

        /// <summary>
        /// Конвертирует цену из указанной валюты обратно в рубли
        /// </summary>
        public static decimal ConvertPriceToRub(decimal priceInCurrency, string currencyCode, decimal rate)
        {
            if (currencyCode == CurrencyCodes.RUB)
            {
                return priceInCurrency;
            }

            return priceInCurrency / rate;
        }
    }
}