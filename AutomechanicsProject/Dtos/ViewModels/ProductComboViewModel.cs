using System;

namespace AutomechanicsProject.ViewModels
{
    /// <summary>
    /// Для выпадающего списка 
    /// </summary>
    public class ProductComboViewModel
    {
        /// <summary>
        /// Уникальный идентификатор товара
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Отображаемый текст элемента из списка
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Артикул
        /// </summary>
        public string Article { get; set; }

        /// <summary>
        /// Название товара
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Цена товара
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Остаток товара
        /// </summary>
        public int Balance { get; set; }

        /// <summary>
        /// Ед измерения
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// Уникальный идентификатор товара
        /// </summary>
        public Guid UnitId { get; set; }
    }
}