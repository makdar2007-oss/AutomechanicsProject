using System;

namespace AutomechanicsProject.ViewModels
{
    /// <summary>
    /// Модель ячейки склада для тепловой карты
    /// </summary>
    public class WarehouseCellViewModel
    {
        /// <summary>
        /// Возвращает или задает идентификатор ячейки
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Возвращает или задает номер строки склада
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Возвращает или задает номер столбца склада
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// Возвращает или задает название ячейки
        /// </summary>
        public string CellCode { get; set; }

        /// <summary>
        /// Возвращает или задает идентификатор товара
        /// </summary>
        public Guid? ProductId { get; set; }

        /// <summary>
        /// Возвращает или задает название товара
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Возвращает или задает артикул товара
        /// </summary>
        public string Article { get; set; }

        /// <summary>
        /// Возвращает или задает название категории
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Возвращает или задает остаток товара
        /// </summary>
        public int Balance { get; set; }

        /// <summary>
        /// Возвращает или задает срок годности товара
        /// </summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// Показывает, требуется ли контроль срока годности
        /// </summary>
        public bool HasExpiryDate { get; set; }

        /// <summary>
        /// Показывает, является ли ячейка пустой
        /// </summary>
        public bool IsEmpty => ProductId == null;
    }
}