using AutomechanicsProject.Dtos;
using AutomechanicsProject.Enums;
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
    /// Сервис для проверки контрагента через DaData
    /// </summary>
    public class DaDataService : IDaDataService
    {
        private readonly HttpClient _httpClient = new HttpClient();

        /// <summary>
        /// Проверяет контрагента по ИНН
        /// </summary>
        public async Task<CounterpartyCheckResultDto> CheckCounterpartyByInnAsync(string inn)
        {
            if (string.IsNullOrWhiteSpace(inn))
            {
                throw new Exception(Resources.ErrorEnterInn);
            }

            if (string.IsNullOrWhiteSpace(Settings.Default.DaDataApiKey))
            {
                throw new Exception(Resources.ErrorDaDataApiKeyMissing);
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
            var suggestions = data["suggestions"] as JArray;

            if (suggestions == null || suggestions.Count == 0)
            {
                return new CounterpartyCheckResultDto
                {
                    Status = CounterpartyCheckStatus.Blocked,
                    Name = string.Empty,
                    Message = Resources.ErrorCounterpartyNotFound
                };
            }

            var suggestion = suggestions[0];

            var name = suggestion["value"]?.ToString() ?? string.Empty;
            var status = suggestion["data"]?["state"]?["status"]?.ToString();

            if (status == "ACTIVE")
            {
                return new CounterpartyCheckResultDto
                {
                    Status = CounterpartyCheckStatus.Allowed,
                    Name = name,
                    Message = Resources.CounterpartyAllowedMessage
                };
            }

            if (status == "LIQUIDATING" ||
                status == "REORGANIZING")
            {
                return new CounterpartyCheckResultDto
                {
                    Status = CounterpartyCheckStatus.Risk,
                    Name = name,
                    Message = Resources.CounterpartyRiskMessage
                };
            }

            return new CounterpartyCheckResultDto
            {
                Status = CounterpartyCheckStatus.Blocked,
                Name = name,
                Message = Resources.CounterpartyBlockedMessage
            };
        }
    }
}