using AutomechanicsProject.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutomechanicsProject.Classes
{
    /// <summary>
    /// Представляет категорию товаров
    /// </summary>
    [Table("category")]
    public class Category
    {
        /// <summary>
        /// Уникальный идентификатор категории
        /// </summary>
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование категории
        /// </summary>
        [Column("name")]
        public string Name { get; set; }
        /// <summary>
        /// Показывает, является ли категория металлоломом
        /// </summary>
        [Column("is_scrap_metal")]
        public bool IsScrapMetal { get; set; }

        /// <summary>
        /// Коллекция товаров, принадлежащих данной категории
        /// </summary>
        public virtual ICollection<Product> Products { get; set; }

        /// <summary>
        /// Отображаемое имя категории с количеством товаров 
        /// </summary>
        [NotMapped]
        public string DisplayName => string.Format(Resources.CategoryDisplayFormat_WithCount, Name, Products?.Count ?? 0);

        /// <summary>
        /// Показывает, удалена ли категория из каталога
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}