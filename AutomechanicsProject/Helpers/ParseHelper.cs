using System;
using System.Globalization;

namespace AutomechanicsProject.Helpers
{
    /// <summary>
    /// Парсинг строковых значений в числа
    /// </summary>
    public static class ParseHelper
    {
        /// <summary>
        /// Безопасное преобразование в int
        /// </summary>
        public static int ToInt(string value, int defaultValue = 0)
        {
            if (string.IsNullOrWhiteSpace(value))
                return defaultValue;

            return int.TryParse(value, out var result) ? result : defaultValue;
        }

        /// <summary>
        /// Безопасное преобразование в decimal с поддержкой разных разделителей
        /// </summary>
        public static decimal ToDecimal(string value, decimal defaultValue = 0)
        {
            if (string.IsNullOrWhiteSpace(value))
                return defaultValue;

            value = value.Replace(',', '.');
            return decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var result)
                ? result : defaultValue;
        }

        /// <summary>
        /// Безопасное преобразование в double
        /// </summary>
        public static double ToDouble(string value, double defaultValue = 0)
        {
            if (string.IsNullOrWhiteSpace(value))
                return defaultValue;

            value = value.Replace(',', '.');
            return double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var result)
                ? result : defaultValue;
        }

        /// <summary>
        /// Преобразование с проверкой  для int
        /// </summary>
        public static bool TryParseInt(string value, out int result)
        {
            return int.TryParse(value, out result);
        }

        /// <summary>
        /// Преобразование с проверкой для decimal
        /// </summary>
        public static bool TryParseDecimal(string value, out decimal result)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                result = 0;
                return false;
            }

            value = value.Replace(',', '.');
            return decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result);
        }

        /// <summary>
        /// Безопасное преобразование Guid из строки
        /// </summary>
        public static Guid ToGuid(string value)
        {
            return Guid.TryParse(value, out var result) ? result : Guid.Empty;
        }

        /// <summary>
        /// Преобразование с проверкой для Guid
        /// </summary>
        public static bool TryParseGuid(string value, out Guid result)
        {
            return Guid.TryParse(value, out result);
        }
    }
}