using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutomechanicsProject.Classes
{
    /// <summary>
    /// Представляет товар на складе
    /// </summary>
    [Table("product")]
    public class Product
    {
        /// <summary>
        /// Уникальный идентификатор товара
        /// </summary>
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Артикул товара 
        /// </summary>
        [Column("article")]
        public string Article { get; set; }

        /// <summary>
        /// Наименование товара
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор категории товара
        /// </summary>
        [Column("category_id")]
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Идентификатор единицы измерения
        /// </summary>
        [Column("unit_id")]
        public Guid UnitId { get; set; }

        /// <summary>
        /// Цена продажи товара
        /// </summary>
        [Column("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Цена закупки товара
        /// </summary>
        [Column("purchase_price")]
        public decimal PurchasePrice { get; set; }

        /// <summary>
        /// Текущий остаток товара на складе
        /// </summary>
        [Column("balance")]
        public int Balance { get; set; }

        /// <summary>
        /// Номер партии
        /// </summary>
        [Column("batch_number")]
        public string BatchNumber { get; set; }

        /// <summary>
        /// Срок годности
        /// </summary>
        [Column("expiry_date")]
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// Категория товара (навигационное свойство)
        /// </summary>
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        /// <summary>
        /// Единица измерения (навигационное свойство)
        /// </summary>
        [ForeignKey("UnitId")]
        public virtual Unit Unit { get; set; }

        /// <summary>
        /// Флаг на наличие срока годности
        /// </summary>
        [Column("has_expiry_date")]
        public bool HasExpiryDate { get; set; }
    }
}