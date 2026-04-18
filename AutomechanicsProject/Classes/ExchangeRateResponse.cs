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
        /// <summary>
        /// Результат выполнения запроса
        /// </summary>
        [JsonPropertyName("result")]
        public string Result { get; set; }

        /// <summary>
        /// Название проайдера курса валют
        /// </summary>
        [JsonPropertyName("provider")]
        public string Provider { get; set; }

        /// <summary>
        /// Код базовой валюты
        /// </summary>
        [JsonPropertyName("base_code")]
        public string BaseCode { get; set; } 

        /// <summary>
        /// Словарь с курсами валют относительно базовой
        /// </summary>
        [JsonPropertyName("rates")]
        public Dictionary<string, decimal> Rates { get; set; }

        /// <summary>
        /// Временная метка последнего обновления курсов
        /// </summary>
        [JsonPropertyName("time_last_update_unix")]
        public long TimeLastUpdateUnix { get; set; }

        /// <summary>
        /// Временная метка следующего обновления курса
        /// </summary>
        [JsonPropertyName("time_next_update_unix")]
        public long TimeNextUpdateUnix { get; set; }
    }
}