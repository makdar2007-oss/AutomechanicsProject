using System;

namespace AutomechanicsProject.ViewModels
{
    /// <summary>
    /// для отображения товара в таблице
    /// </summary>
    public class ProductViewModel
    {
        /// <summary>
        /// Уникальный идентификатор товара
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Артикул товара
        /// </summary>
        public string Article { get; set; }

        /// <summary>
        /// Название товара
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Название категории
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Название ед измерения
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// Срок годности товара
        /// </summary>
        public string ExpiryDateText { get; set; }

        /// <summary>
        /// Остаток 
        /// </summary>
        public int Balance { get; set; }

        /// <summary>
        /// Цена товара
        /// </summary>
        public decimal PurchasePrice { get; set; }

        /// <summary>
        /// Закупочная цена товара
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Номер партии товара
        /// </summary>
        public string BatchNumber { get; set; }


        /// <summary>
        /// Отображаемое название товара
        /// </summary>
        public string DisplayName => $"{Article} - {Name}";

        /// <summary>
        /// отображаемый остаток товара
        /// </summary>
        public string BalanceWithUnit => $"{Balance} {UnitName}";
    }
}