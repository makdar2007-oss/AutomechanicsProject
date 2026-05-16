using System;

namespace AutomechanicsProject.Classes
{
    /// <summary>
    /// Ячейка склада для тепловой карты
    /// </summary>
    public class WarehouseCell
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
        /// Возвращает или задает идентификатор товара в ячейке
        /// </summary>
        public Guid? ProductId { get; set; }

        /// <summary>
        /// Возвращает или задает товар в ячейке
        /// </summary>
        public virtual Product Product { get; set; }
    }
}