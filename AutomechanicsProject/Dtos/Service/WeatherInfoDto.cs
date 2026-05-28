using System;

namespace AutomechanicsProject.Dtos.Service
{
    /// <summary>
    /// DTO для информации о погоде
    /// </summary>
    public class WeatherInfoDto
    {
        /// <summary>
        /// Температура воздуха в градусах Цельсия
        /// </summary>
        public double Temperature { get; set; }

        /// <summary>
        /// Описание погоды (ясно, облачно, дождь и т.д.)
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Влажность воздуха в процентах
        /// </summary>
        public int Humidity { get; set; }

        /// <summary>
        /// Скорость ветра в м/с
        /// </summary>
        public double WindSpeed { get; set; }

        /// <summary>
        /// Атмосферное давление в мм рт. ст.
        /// </summary>
        public int Pressure { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Время получения данных
        /// </summary>
        public DateTime RetrievedAt { get; set; }

        /// <summary>
        /// Отображаемый текст погоды
        /// </summary>
        public string DisplayText => $"{City}: {Temperature:F1}°C, {Description}, 💧{Humidity}%, 💨{WindSpeed:F1} м/с, 🎯{Pressure} мм рт.ст.";
    }
}