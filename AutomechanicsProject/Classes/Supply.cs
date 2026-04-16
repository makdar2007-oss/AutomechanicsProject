using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutomechanicsProject.Classes
{
    /// <summary>
    /// Представляет поставку товаров
    /// </summary>
    [Table("supply")]
    public class Supply
    {
        /// <summary>
        /// Уникальный идентификатор поставки
        /// </summary>
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Номер заказа
        /// </summary>
        [Column("order_number")]
        public string OrderNumber { get; set; }

        /// <summary>
        /// Дата создания поставки
        /// </summary>
        [Column("date_created")]
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Идентификатор поставщика
        /// </summary>
        [Column("supplier_id")]
        public Guid? SupplierId { get; set; }

        /// <summary>
        /// Идентификатор пользователя, создавшего поставку
        /// </summary>
        [Column("user_id")]
        public Guid UserId { get; set; }

        /// <summary>
        /// Статус поставки
        /// </summary>
        [Column("status")]
        public string Status { get; set; }

        /// <summary>
        /// Общая сумма поставки
        /// </summary>
        [Column("total_amount")]
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Поставщик (навигационное свойство)
        /// </summary>
        [ForeignKey("SupplierId")]
        public virtual Supplier Supplier { get; set; }

        /// <summary>
        /// Пользователь (навигационное свойство)
        /// </summary>
        [ForeignKey("UserId")]
        public virtual Users User { get; set; }

        /// <summary>
        /// Коллекция позиций поставки
        /// </summary>
        public virtual List<SupplyPosition> Positions { get; set; } = new List<SupplyPosition>();
    }
}