using System;

namespace AutomechanicsProject.Classes
{
    /// <summary>
    /// Класс для работы с московским временем (UTC+3)
    /// </summary>
    public static class MoscowTime
    {
        private static readonly TimeZoneInfo MoscowTimeZone;

        static MoscowTime()
        {
            try
            {
                MoscowTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time");
            }
            catch (TimeZoneNotFoundException)
            {
                MoscowTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Moscow");
            }
        }

        /// <summary>
        /// Текущее время в Москве 
        /// </summary>
        public static DateTime Now
        {
            get
            {
                DateTime utcNow = DateTime.UtcNow;
                return TimeZoneInfo.ConvertTimeFromUtc(utcNow, MoscowTimeZone);
            }
        }

        /// <summary>
        /// Текущая дата в Москве 
        /// </summary>
        public static DateTime Today
        {
            get
            {
                return Now.Date;
            }
        }

        /// <summary>
        /// Конвертирует любую дату в московское время
        /// </summary>
        public static DateTime ConvertToMoscow(DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Utc)
            {
                return TimeZoneInfo.ConvertTimeFromUtc(dateTime, MoscowTimeZone);
            }

            return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.Local, MoscowTimeZone);
        }

        /// <summary>
        /// Форматирует дату для отображения в московском времени
        /// </summary>
        public static string Format(DateTime dateTime, string format = "dd.MM.yyyy HH:mm")
        {
            if (dateTime.Kind == DateTimeKind.Utc)
            {
                DateTime moscowTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime, MoscowTimeZone);
                return moscowTime.ToString(format);
            }
            return dateTime.ToString(format);
        }

        /// <summary>
        /// Получить московское время из UTC
        /// </summary>
        public static DateTime FromUtc(DateTime utcDateTime)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, MoscowTimeZone);
        }

        /// <summary>
        /// Конвертировать московское время в UTC
        /// </summary>
        public static DateTime ToUtc(DateTime moscowDateTime)
        {
            return TimeZoneInfo.ConvertTimeToUtc(moscowDateTime, MoscowTimeZone);
        }
    }
}