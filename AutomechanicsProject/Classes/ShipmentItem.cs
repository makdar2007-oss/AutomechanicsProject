using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutomechanicsProject.Classes
{
    /// <summary>
    /// Представляет товар в отгрузке
    /// </summary>
    [Table("shipment_positions")]
    public class ShipmentItem
    {
        /// <summary>
        /// Уникальный идентификатор позиции
        /// </summary>
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор отгрузки
        /// </summary>
        [Column("shipment_id")]
        public Guid ShipmentId { get; set; }

        /// <summary>
        /// Идентификатор товара
        /// </summary>
        [Column("product_id")]
        public Guid ProductId { get; set; }

        /// <summary>
        /// Количество товара
        /// </summary>
        [Column("quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// Цена товара на момент отгрузки
        /// </summary>
        [Column("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Наименование товара 
        /// </summary>
        [Column("product_name")]
        public string ProductName { get; set; }

        /// <summary>
        /// Артикул товара
        /// </summary>
        [Column("article")]
        public string Article { get; set; }

        /// <summary>
        /// Товар (навигационное свойство)
        /// </summary>
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        /// <summary>
        /// Отгрузка (навигационное свойство)
        /// </summary>
        [ForeignKey("ShipmentId")]
        public virtual Shipment Shipment { get; set; }
    }
}