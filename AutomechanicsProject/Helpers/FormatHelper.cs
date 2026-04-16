using System;
using AutomechanicsProject.Formes;

namespace AutomechanicsProject.Helpers
{
    /// <summary>
    /// Форматирование данных для отображения
    /// </summary>
    public static class FormatHelper
    {
        /// <summary>
        /// Форматирует дату в формат dd.MM.yyyy
        /// </summary>
        public static string FormatDate(DateTime? date)
        {
            return date?.ToString("dd.MM.yyyy") ?? "—";
        }

        /// <summary>
        /// Форматирует дату с пользовательским форматом
        /// </summary>
        public static string FormatDate(DateTime? date, string format)
        {
            return date?.ToString(format) ?? "—";
        }

        /// <summary>
        /// Форматирует дату и время
        /// </summary>
        public static string FormatDateTime(DateTime dateTime)
        {
            return dateTime.ToString("dd.MM.yyyy HH:mm");
        }

        /// <summary>
        /// Форматирует отображение товара: "Артикул - Название"
        /// </summary>
        public static string FormatProductShort(string article, string name)
        {
            return $"{article} - {name}";
        }

        /// <summary>
        /// Форматирует отображение товара с остатком
        /// </summary>
        public static string FormatProductWithBalance(string article, string name, int balance, string unitName)
        {
            return $"{article} - {name} (остаток: {balance} {unitName})";
        }

        /// <summary>
        /// Форматирует отображение товара с ценой
        /// </summary>
        public static string FormatProductWithPrice(string article, string name, decimal price, string currencyCode = "RUB")
        {
            return $"{article} - {name} ({price:F2} {currencyCode})";
        }

        /// <summary>
        /// Форматирует цену с валютой
        /// </summary>
        public static string FormatPrice(decimal price)
        {
            return $"{price:F2} {ChoosingCurrency.SelectedCurrencyCode}";
        }

        /// <summary>
        /// Форматирует цену с указанием валюты
        /// </summary>
        public static string FormatPrice(decimal price, string currencyCode)
        {
            return $"{price:F2} {currencyCode}";
        }

        /// <summary>
        /// Форматирует цену закупки (всегда в рублях)
        /// </summary>
        public static string FormatPurchasePrice(decimal price)
        {
            return $"{price:F2} ₽";
        }

        /// <summary>
        /// Форматирует остаток с единицей измерения
        /// </summary>
        public static string FormatBalance(int balance, string unitName)
        {
            return $"{balance} {unitName}";
        }

        /// <summary>
        /// Форматирует единицу измерения для отображения в ComboBox
        /// </summary>
        public static string FormatUnitDisplay(string name, string shortName)
        {
            return $"{name} ({shortName})";
        }

        /// <summary>
        /// Форматирует категорию с количеством товаров
        /// </summary>
        public static string FormatCategoryWithCount(string categoryName, int productCount)
        {
            return $"{categoryName} (товаров: {productCount})";
        }

        /// <summary>
        /// Форматирует срок годности для отображения в выпадающем списке
        /// </summary>
        public static string FormatExpiryDateDisplay(DateTime? expiryDate, int balance)
        {
            if (expiryDate.HasValue)
                return $"Срок: {expiryDate.Value:dd.MM.yyyy} (остаток: {balance})";
            return $"Без срока (остаток: {balance})";
        }
    }
}