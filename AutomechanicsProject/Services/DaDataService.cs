using AutomechanicsProject.Properties;
using AutomechanicsProject.Services.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AutomechanicsProject.Services
{
    /// <summary>
    /// Сервис для проверки поставщика через DaData
    /// </summary>
    public class DaDataService : IDaDataService
    {
        private readonly HttpClient _httpClient = new HttpClient();

        /// <summary>
        /// Проверяет поставщика по ИНН
        /// </summary>
        public async Task<string> CheckSupplierByInnAsync(string inn)
        {
            if (string.IsNullOrWhiteSpace(inn))
            {
                throw new Exception("Введите ИНН");
            }

            if (string.IsNullOrWhiteSpace(Settings.Default.DaDataApiKey))
            {
                throw new Exception("Не указан API-ключ DaData");
            }

            var request = new HttpRequestMessage(
                HttpMethod.Post,
                "https://suggestions.dadata.ru/suggestions/api/4_1/rs/findById/party");

            request.Headers.Add("Authorization", "Token " + Settings.Default.DaDataApiKey);

            var json = "{ \"query\": \"" + inn + "\" }";
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseText = await response.Content.ReadAsStringAsync();
            var data = JObject.Parse(responseText);
            var suggestion = data["suggestions"]?[0];

            if (suggestion == null)
            {
                throw new Exception("Поставщик с таким ИНН не найден");
            }

            return suggestion["value"]?.ToString();
        }
    }
}