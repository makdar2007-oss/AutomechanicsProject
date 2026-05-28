using AutomechanicsProject.Properties;
using AutomechanicsProject.Services.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AutomechanicsProject.Services
{
    /// <summary>
    /// Сервис для проверки погоды через OpenWeatherMap
    /// </summary>
    public class WeatherService : IWeatherService
    {
        private const decimal MinAllowedTemperature = -15m;
        private const decimal MaxAllowedTemperature = 25m;

        private readonly HttpClient _httpClient = new HttpClient();

        /// <summary>
        /// Проверяет, нужна ли термоупаковка по погоде
        /// </summary>
        public async Task<bool> IsThermoContainerNeededAsync(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new Exception(Resources.ErrorEnterCity);
            }

            if (string.IsNullOrWhiteSpace(Settings.Default.OpenWeatherApiKey))
            {
                throw new Exception(Resources.ErrorOpenWeatherApiKeyMissing);
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
                throw new Exception(Resources.ErrorWeatherForecastLoad);
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

                var temperature = item["main"]?["temp"]?.Value<decimal>() ?? 0m;

                if (temperature < MinAllowedTemperature ||
                    temperature > MaxAllowedTemperature)
                {
                    return true;
                }
            }

            return false;
        }
    }
}