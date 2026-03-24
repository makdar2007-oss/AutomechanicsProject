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
        /// Коллекция товаров, принадлежащих данной категории
        /// </summary>
        public virtual ICollection<Product> Products { get; set; }

        /// <summary>
        /// Отображаемое имя категории с количеством товаров 
        /// </summary>
        [NotMapped]
        public string DisplayName => $"{Name} (товаров: {Products?.Count ?? 0})";
    }
}