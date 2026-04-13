using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace AutomechanicsProject.Helpers
{
    public static class CurrencyStorage
    {
        private static string FilePath => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "AutomechanicsProject",
            "currency_cache.json"
        );

        public class CurrencyCache
        {
            public Dictionary<string, decimal> ExchangeRates { get; set; }
            public string LastSelectedCurrency { get; set; }
            public DateTime LastUpdate { get; set; }
        }

        public static void SaveRates(Dictionary<string, decimal> rates, string selectedCurrency)
        {
            try
            {
                var directory = Path.GetDirectoryName(FilePath);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                var cache = new CurrencyCache
                {
                    ExchangeRates = rates,
                    LastSelectedCurrency = selectedCurrency,
                    LastUpdate = DateTime.Now
                };

                var json = JsonSerializer.Serialize(cache);
                File.WriteAllText(FilePath, json);
            }
            catch (Exception ex)
            {
                Program.LogError("Ошибка сохранения курсов валют", ex);
            }
        }

        public static CurrencyCache LoadRates()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    var json = File.ReadAllText(FilePath);
                    var cache = JsonSerializer.Deserialize<CurrencyCache>(json);

                    // Проверяем актуальность (например, не старше 24 часов)
                    if (cache != null && (DateTime.Now - cache.LastUpdate).TotalHours < 24)
                        return cache;
                }
            }
            catch (Exception ex)
            {
                Program.LogError("Ошибка загрузки кэша курсов валют", ex);
            }
            return null;
        }
    }
}