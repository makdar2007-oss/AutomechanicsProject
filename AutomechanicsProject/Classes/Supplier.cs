using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutomechanicsProject.Classes
{
    /// <summary>
    /// Представляет поставщика товаров
    /// </summary>
    [Table("supplier")]
    public class Supplier
    {
        /// <summary>
        /// Уникальный идентификатор поставщика
        /// </summary>
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование поставщика
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// Телефон поставщика
        /// </summary>
        [Column("phone")]
        public string Phone { get; set; }

        /// <summary>
        /// Адрес поставщика
        /// </summary>
        [Column("address")]
        public string Address { get; set; }
    }
}