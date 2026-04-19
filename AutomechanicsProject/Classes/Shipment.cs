using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutomechanicsProject.Classes
{
    /// <summary>
    /// Представляет отгрузку товаров получателю
    /// </summary>
    [Table("shipment")]
    public class Shipment
    {
        /// <summary>
        /// Уникальный идентификатор отгрузки
        /// </summary>
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Дата и время создания отгрузки
        /// </summary>
        [Column("date_created")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Идентификатор получателя 
        /// </summary>
        [Column("user_id")]
        public Guid UserId { get; set; }

        /// <summary>
        /// Идентификатор кладовщика, создавшего отгрузку
        /// </summary>
        [Column("created_by_user_id")]
        public Guid CreatedByUserId { get; set; }

        /// <summary>
        /// Общая сумма отгрузки
        /// </summary>
        [Column("total_amount")]
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Тип отгрузки 
        /// </summary>
        [Column("shipment_type")]
        public string ShipmentType { get; set; } = "Shipment";

        /// <summary>
        /// Получатель (навигационное свойство)
        /// </summary>
        [ForeignKey("UserId")]
        public virtual Address User { get; set; }

        /// <summary>
        /// Кладовщик, создавший отгрузку (навигационное свойство)
        /// </summary>
        [ForeignKey("CreatedByUserId")]
        public virtual Users CreatedByUser { get; set; }

        /// <summary>
        /// Коллекция позиций в отгрузке
        /// </summary>
        public virtual List<ShipmentItem> Items { get; set; } = new List<ShipmentItem>();
    }
}