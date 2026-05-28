using AutomechanicsProject.Properties;
using AutomechanicsProject.Services.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AutomechanicsProject.Services
{
    /// <summary>
    /// Сервис для проверки погоды
    /// </summary>
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient = new HttpClient();

        /// <summary>
        /// Проверяет плохую погоду в городе на ближайшие дни
        /// </summary>
        public async Task<bool> HasBadWeatherAsync(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new Exception("Введите город");
            }

            if (string.IsNullOrWhiteSpace(Settings.Default.OpenWeatherApiKey))
            {
                throw new Exception("Не указан API-ключ OpenWeatherMap");
            }

            var url = "https://api.openweathermap.org/data/2.5/forecast?q="
                + Uri.EscapeDataString(city)
                + "&appid="
                + Settings.Default.OpenWeatherApiKey
                + "&units=metric&lang=ru";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var responseText = await response.Content.ReadAsStringAsync();
            var data = JObject.Parse(responseText);
            var list = data["list"];

            if (list == null)
            {
                throw new Exception("Не удалось получить прогноз погоды");
            }

            var limitDate = DateTime.Now.AddDays(3);

            foreach (var item in list)
            {
                var dateText = item["dt_txt"]?.ToString();

                if (!DateTime.TryParse(dateText, out var date))
                {
                    continue;
                }

                if (date > limitDate)
                {
                    continue;
                }

                var weather = item["weather"]?[0]?["main"]?.ToString();
                var temperature = item["main"]?["temp"]?.Value<decimal>() ?? 0;

                if (weather == "Rain" ||
                    weather == "Snow" ||
                    weather == "Thunderstorm" ||
                    temperature < -10 ||
                    temperature > 30)
                {
                    return true;
                }
            }

            return false;
        }
    }
}