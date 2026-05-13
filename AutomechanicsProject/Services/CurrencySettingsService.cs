using AutomechanicsProject.Classes;
using AutomechanicsProject.Formes;
using AutomechanicsProject.Properties;
using AutomechanicsProject.Services.Interfaces;

namespace AutomechanicsProject.Services
{
    /// <summary>
    /// Сервис настроек выбранной валюты
    /// </summary>
    public class CurrencySettingsService : ICurrencySettingsService
    {
        /// <summary>
        /// Возвращает код выбранной валюты
        /// </summary>
        public string SelectedCurrencyCode { get; private set; } = CurrencyCodes.RUB;

        /// <summary>
        /// Возвращает название выбранной валюты
        /// </summary>
        public string SelectedCurrencyName { get; private set; } = Resources.RussianRuble;

        /// <summary>
        /// Возвращает текущий курс валюты
        /// </summary>
        public decimal CurrentExchangeRate { get; private set; } = 1m;

        /// <summary>
        /// Устанавливает выбранную валюту
        /// </summary>
        public void SetCurrency(string currencyCode, string currencyName, decimal exchangeRate)
        {
            SelectedCurrencyCode = currencyCode;
            SelectedCurrencyName = currencyName;
            CurrentExchangeRate = exchangeRate <= 0 ? 1m : exchangeRate;
        }

        /// <summary>
        /// Сбрасывает выбранную валюту на рубли
        /// </summary>
        public void ResetToRub()
        {
            SelectedCurrencyCode = CurrencyCodes.RUB;
            SelectedCurrencyName = Resources.RussianRuble;
            CurrentExchangeRate = 1m;
        }

        /// <summary>
        /// Конвертирует цену из рублей в выбранную валюту
        /// </summary>
        public decimal ConvertPrice(decimal priceInRub)
        {
            if (SelectedCurrencyCode == CurrencyCodes.RUB || CurrentExchangeRate <= 0)
            {
                return priceInRub;
            }

            return priceInRub * CurrentExchangeRate;
        }
    }
}