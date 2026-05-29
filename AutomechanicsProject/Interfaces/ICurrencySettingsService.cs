using System;

namespace AutomechanicsProject.Services.Interfaces
{
    /// <summary>
    /// Сервис настроек выбранной валюты
    /// </summary>
    public interface ICurrencySettingsService
    {
        /// <summary>
        /// Возвращает код выбранной валюты
        /// </summary>
        string SelectedCurrencyCode { get; }

        /// <summary>
        /// Возвращает название выбранной валюты
        /// </summary>
        string SelectedCurrencyName { get; }

        /// <summary>
        /// Возвращает текущий курс валюты
        /// </summary>
        decimal CurrentExchangeRate { get; }

        /// <summary>
        /// Устанавливает выбранную валюту
        /// </summary>
        void SetCurrency(string currencyCode, string currencyName, decimal exchangeRate);

        /// <summary>
        /// Сбрасывает выбранную валюту на рубли
        /// </summary>
        void ResetToRub();

        /// <summary>
        /// Конвертирует цену из рублей в выбранную валюту
        /// </summary>
        decimal ConvertPrice(decimal priceInRub);
    }
}