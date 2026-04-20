using AutomechanicsProject.Classes;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;

namespace AutomechanicsProject.Helpers
{
    public static class CurrencyHelper
    {
        private static Dictionary<string, decimal> _cachedRates;
        private static bool _isLoaded = false;

        /// <summary>
        /// Возвращает список доступных валют
        /// </summary>
        public static List<CurrencyInfo> GetCurrenciesFromApi()
        {
            var rates = GetExchangeRates();
            var currencies = new List<CurrencyInfo>();

            foreach (var rate in rates)
            {
                currencies.Add(new CurrencyInfo
                {
                    Code = rate.Key,
                    Rate = rate.Value,
                    DisplayText = $"{rate.Key} - {GetCurrencyName(rate.Key)} (1 RUB = {rate.Value:F4} {rate.Key})"
                });
            }

            return currencies;
        }

        /// <summary>
        /// Получает курсы валют из API
        /// </summary>
        public static Dictionary<string, decimal> GetExchangeRates()
        {
            if (_cachedRates != null && _isLoaded)
            {
                return _cachedRates;
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(10);
                    var response = client.GetAsync("https://open.er-api.com/v6/latest/RUB").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var json = response.Content.ReadAsStringAsync().Result;
                        var data = JsonSerializer.Deserialize<Dictionary<string, object>>(json);

                        if (data != null && data.ContainsKey("rates"))
                        {
                            var ratesJson = JsonSerializer.Serialize(data["rates"]);
                            var rates = JsonSerializer.Deserialize<Dictionary<string, decimal>>(ratesJson);

                            if (rates != null)
                            {
                                if (!rates.ContainsKey("RUB"))
                                {
                                    rates["RUB"] = 1.00m;
                                }

                                _cachedRates = rates;
                                _isLoaded = true;
                                return rates;
                            }
                        }
                    }
                }
            }
            catch { }

            return GetFallbackRates();
        }

        /// <summary>
        /// Возвращает резервный список валют
        /// </summary>
        public static List<CurrencyInfo> GetFallbackCurrencies()
        {
            var rates = GetFallbackRates();
            var currencies = new List<CurrencyInfo>();

            foreach (var rate in rates)
            {
                currencies.Add(new CurrencyInfo
                {
                    Code = rate.Key,
                    Rate = rate.Value,
                    DisplayText = $"{rate.Key} - {GetCurrencyName(rate.Key)}"
                });
            }

            return currencies;
        }

        /// <summary>
        /// Возвращает фиксированный курс
        /// </summary>
        public  static Dictionary<string, decimal> GetFallbackRates()
        {
            return new Dictionary<string, decimal>
            {
                { "RUB", 1.00m },
                { "USD", 0.011m },
                { "EUR", 0.010m },
                { "CNY", 0.079m },
                { "KZT", 4.90m },
                { "BYN", 0.036m },
                { "GBP", 0.0087m },
                { "JPY", 1.63m }
            };
        }

        /// <summary>
        /// Возвращает наименование валют
        /// </summary>
        public static string GetCurrencyName(string code)
        {
            switch (code)
            {
                case "RUB": return "Российский рубль";
                case "USD": return "Доллар США";
                case "EUR": return "Евро";
                case "CNY": return "Китайский юань";
                case "KZT": return "Казахстанский тенге";
                case "BYN": return "Белорусский рубль";
                case "GBP": return "Фунт стерлингов";
                case "JPY": return "Японская иена";
                default: return code;
            }
        }

        /// <summary>
        /// Конвертирует в рубли
        /// </summary>
        public static decimal ConvertToRUB(decimal amount, decimal rate)
        {
            if (rate == 0)
            {
                return 0;
            }

            return amount / rate;
        }

        /// <summary>
        /// Конвертирует из рублей
        /// </summary>
        public static decimal ConvertFromRUB(decimal amount, decimal rate)
        {
            if (rate == 0)
            {
                return 0;
            }    

            return amount * rate;
        }
    }
}