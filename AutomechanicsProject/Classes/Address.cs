using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutomechanicsProject.Classes
{
    /// <summary>
    /// Представляет адрес в системе
    /// </summary>
    [Table("address")]
    public class Address
    {
        /// <summary>
        /// Уникальный идентификатор адреса
        /// </summary>
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование компании
        /// </summary>
        [Column("company_name")]
        public string CompanyName { get; set; }

        /// <summary>
        /// Полное наименование компании 
        /// </summary>
        [NotMapped]
        public string FullName => CompanyName;
    }
}