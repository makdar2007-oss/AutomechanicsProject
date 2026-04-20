using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace AutomechanicsProject.Helpers
{
    /// <summary>
    /// Кэширует курсы валют в локальный JSON-файл
    /// </summary>
    public static class CurrencyStorage
    {
        private static string FilePath => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "AutomechanicsProject",
            "currency_cache.json"
        );
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Сохраняет курсы валют и выбранную валюту в файл
        /// </summary>
        public static void SaveRates(Dictionary<string, decimal> rates, string selectedCurrency)

        {
            try
            {
                var directory = Path.GetDirectoryName(FilePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var cache = new
                {
                    ExchangeRates = rates,
                    LastSelectedCurrency = selectedCurrency,
                    LastUpdate = DateTime.Now
                };

                var json = JsonSerializer.Serialize(cache);
                File.WriteAllText(FilePath, json);
            }
            catch (Exception)
            {
                logger.Error("Ошибка сохранения курсов валют");
            }
        }

        /// <summary>
        /// Загружает курсы валют из файла
        /// </summary>
        public static (Dictionary<string, decimal> ExchangeRates, string LastSelectedCurrency, DateTime LastUpdate)? LoadRates()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    var json = File.ReadAllText(FilePath);
                    using (var doc = JsonDocument.Parse(json))
                    {
                        var root = doc.RootElement;

                        var rates = JsonSerializer.Deserialize<Dictionary<string, decimal>>(root.GetProperty("ExchangeRates").GetRawText());
                        var selectedCurrency = root.GetProperty("LastSelectedCurrency").GetString();
                        var lastUpdate = root.GetProperty("LastUpdate").GetDateTime();

                        if (rates != null && selectedCurrency != null && (DateTime.Now - lastUpdate).TotalHours < 24)
                        {
                            return (rates, selectedCurrency, lastUpdate);

                        }
                    }
                }
            }
            catch (Exception)
            {
                logger.Error("Ошибка загрузки кэша курсов валют");
            }
            return null;
        }
    }
}