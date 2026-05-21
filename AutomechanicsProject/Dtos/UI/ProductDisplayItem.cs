using AutomechanicsProject.Properties;
using System;

namespace AutomechanicsProject.Dtos.UI
{
    /// <summary>
    /// Для отображения товара в комбобоксе
    /// </summary>
    public class ProductDisplayItem
    {
        /// <summary>
        /// Уникальный идентификатор товара
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Артикул товара.
        /// </summary>
        public string Article { get; set; }

        /// <summary>
        /// Название товара
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Текущий остаток
        /// </summary>
        public int Balance { get; set; }

        /// <summary>
        /// Флаг на наличие товара
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Срок годности
        /// </summary>
        public DateTime? ProductExpiryDate { get; set; } 

        /// <summary>
        /// Флаг на срок годности
        /// </summary>
        public bool HasExpiryDate { get; set; }

        /// <summary>
        /// Отображает товар с учетом наличия и остатка 
        /// </summary>
        public string DisplayName => IsActive
            ? string.Format(Resources.ProductDisplay_InStockFormat, Name, Balance)
            : string.Format(Resources.ProductDisplay_WaitingSupplyFormat, Name);

        /// <summary>
        /// Строка для поиска товара
        /// </summary>
        public string SearchString => $"{Article} {Name}".ToLower();
    }
}