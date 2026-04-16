using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AutomechanicsProject.Classes
{
    /// <summary>
    /// Представляет ответ от API обменных курсов
    /// Используется для десериализации JSON-ответа
    /// </summary>
    internal class ExchangeRateResponse
    {
        [JsonPropertyName("result")]
        public string Result { get; set; }

        [JsonPropertyName("provider")]
        public string Provider { get; set; }

        [JsonPropertyName("base_code")]
        public string BaseCode { get; set; } 

        [JsonPropertyName("rates")]
        public Dictionary<string, decimal> Rates { get; set; }

        [JsonPropertyName("time_last_update_unix")]
        public long TimeLastUpdateUnix { get; set; }

        [JsonPropertyName("time_next_update_unix")]
        public long TimeNextUpdateUnix { get; set; }
    }
}