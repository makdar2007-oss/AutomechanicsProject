using System;

namespace AutomechanicsProject.Classes
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
        /// Флаг (в наличии ли товар или нет)
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Срок годности
        /// </summary>
        public DateTime? ProductExpiryDate { get; set; } 

        /// <summary>
        /// Есть ли у товара срок годности
        /// </summary>
        public bool HasExpiryDate => ProductExpiryDate.HasValue; 

        /// <summary>
        /// Отображает товар с учетом наличия и остатка 
        /// </summary>
        public string DisplayName => IsActive
            ? $"{Name} (в наличии: {Balance})"
            : $"{Name} (ожидается поставка)";

        /// <summary>
        /// Строка для поиска товара
        /// </summary>
        public string SearchString => $"{Article} {Name}".ToLower();
    }
}