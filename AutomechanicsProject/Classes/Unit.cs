using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutomechanicsProject.Classes
{
    /// <summary>
    /// Представляет единицу измерения товара
    /// </summary>
    [Table("unit")]
    public class Unit
    {
        /// <summary>
        /// Уникальный идентификатор единицы измерения
        /// </summary>
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Полное наименование единицы измерения
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// Краткое наименование единицы измерения
        /// </summary>
        [Column("short_name")]
        public string ShortName { get; set; }

        /// <summary>
        /// Отображаемое имя 
        /// </summary>
        [NotMapped]
        public string DisplayName => $"{Name} ({ShortName})";
    }
}