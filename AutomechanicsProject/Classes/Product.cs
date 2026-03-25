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
        /// Единица измерения товара (по умолчанию "шт")
        /// </summary>
        [Column("unit")]
        public string Unit { get; set; } = "шт";

        /// <summary>
        /// Цена товара
        /// </summary>
        [Column("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Текущий остаток товара на складе
        /// </summary>
        [Column("balance")]
        public int Balance { get; set; }

        /// <summary>
        /// Категория товара (навигационное свойство)
        /// </summary>
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}