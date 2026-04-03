using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutomechanicsProject.Classes
{
    /// <summary>
    /// Представляет позицию в поставке
    /// </summary>
    [Table("supply_positions")]
    public class SupplyPosition
    {
        /// <summary>
        /// Уникальный идентификатор позиции
        /// </summary>
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор поставки
        /// </summary>
        [Column("supply_id")]
        public Guid SupplyId { get; set; }

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
        /// Цена товара на момент поставки
        /// </summary>
        [Column("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Наименование товара (для истории)
        /// </summary>
        [Column("product_name")]
        public string ProductName { get; set; }

        /// <summary>
        /// Артикул товара (для истории)
        /// </summary>
        [Column("article")]
        public string Article { get; set; }

        /// <summary>
        /// Товар (навигационное свойство)
        /// </summary>
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        /// <summary>
        /// Поставка (навигационное свойство)
        /// </summary>
        [ForeignKey("SupplyId")]
        public virtual Supply Supply { get; set; }
    }
}