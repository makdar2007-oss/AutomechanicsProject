using System;

namespace AutomechanicsProject.Helpers
{
    /// <summary>
    /// Работа с датами и временем
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// Возвращает начало дня
        /// </summary>
        public static DateTime StartOfDay(DateTime date)
        {
            return date.Date;
        }

        /// <summary>
        /// Возвращает конец дня 
        /// </summary>
        public static DateTime EndOfDay(DateTime date)
        {
            return date.Date.AddDays(1).AddSeconds(-1);
        }

        /// <summary>
        /// Проверяет, просрочен ли товар
        /// </summary>
        public static bool IsExpired(DateTime? expiryDate)
        {
            return expiryDate.HasValue && expiryDate.Value.Date < DateTime.Today;
        }

        /// <summary>
        /// Проверяет, истекает ли срок годности в ближайшие дни
        /// </summary>
        public static bool IsExpiringSoon(DateTime? expiryDate, int daysThreshold = 30)
        {
            if (!expiryDate.HasValue)
                return false;

            var daysLeft = (expiryDate.Value.Date - DateTime.Today).Days;
            return daysLeft > 0 && daysLeft <= daysThreshold;
        }

        /// <summary>
        /// Возвращает количество дней до истечения срока
        /// </summary>
        public static int? DaysUntilExpiry(DateTime? expiryDate)
        {
            if (!expiryDate.HasValue)
                return null;

            return (expiryDate.Value.Date - DateTime.Today).Days;
        }

        /// <summary>
        /// Проверяет, что дата не меньше сегодняшней
        /// </summary>
        public static bool IsValidExpiryDate(DateTime? expiryDate)
        {
            return expiryDate.HasValue && expiryDate.Value.Date >= DateTime.Today;
        }
    }
}