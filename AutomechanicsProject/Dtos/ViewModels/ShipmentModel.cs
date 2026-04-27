using System;
using System.ComponentModel;

namespace AutomechanicsProject.ViewModels
{
    /// <summary>
    /// Модель для отображения отгрузки
    /// </summary>
    public class ShipmentViewModel
    {
        /// <summary>
        /// Артикул товара
        /// </summary>
        [DisplayName("Артикул")]
        public string Article { get; set; }

        /// <summary>
        /// Название товара
        /// </summary>
        [DisplayName("Наименование")]
        public string Name { get; set; }

        /// <summary>
        /// Количество товара в отгрузке
        /// </summary>
        [DisplayName("Количество")]
        public int Quantity { get; set; }

        /// <summary>
        /// Цена товара
        /// </summary>
        [DisplayName("Цена")]
        public decimal Price { get; set; }

        /// <summary>
        /// Прибыль от продажи товара
        /// </summary>
        [DisplayName("Прибыль")]
        public decimal Profit { get; set; }

        /// <summary>
        /// Общая сумма по позиции
        /// </summary>
        [DisplayName("Сумма")]
        public decimal Total { get; set; }

        /// <summary>
        /// Название компании
        /// </summary>
        [DisplayName("Получатель")]
        public string RecipientName { get; set; }

        /// <summary>
        /// Идентификатор товара (для связи с оригинальным списком)
        /// </summary>
        [Browsable(false)]
        public Guid ProductId { get; set; }

        /// <summary>
        /// Является ли товар металлическим
        /// </summary>
        [Browsable(false)]
        public bool IsMetal { get; set; }

        /// <summary>
        /// Был ли товар сдан на металлолом
        /// </summary>
        [Browsable(false)]
        public bool IsScrapped { get; set; }

        /// <summary>
        /// Отметка о сдаче на металлолом (для UI)
        /// </summary>
        [DisplayName("Металлолом")]
        public bool ScrapMetal { get; set; }

        /// <summary>
        /// Отображение общей суммы в формате валюты
        /// </summary>
        [Browsable(false)]
        public string TotalText => $"{Total:C}";

        /// <summary>
        /// Отображение прибыли в формате валюты
        /// </summary>
        [Browsable(false)]
        public string ProfitText => $"{Profit:C}";
    }
}