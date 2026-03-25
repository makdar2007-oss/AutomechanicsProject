using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutomechanicsProject.Classes
{
    /// <summary>
    /// Представляет пользователя системы
    /// </summary>
    [Table("users")]
    public class Users
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        [Column("surname")]
        public string Surname { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// Отчество пользователя
        /// </summary>
        [Column("lastname")]
        public string Lastname { get; set; }

        /// <summary>
        /// Логин для входа в систему
        /// </summary>
        [Column("login")]
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Column("password")]
        public string Password { get; set; }

        /// <summary>
        /// Идентификатор роли пользователя
        /// </summary>
        [Column("role_id")]
        public Guid RoleId { get; set; }

        /// <summary>
        /// Роль пользователя (навигационное свойство)
        /// </summary>
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        /// <summary>
        /// Полное имя пользователя 
        /// </summary>
        [NotMapped]
        public string FullName => $"{Surname} {Name} {Lastname}".Trim();

        /// <summary>
        /// Наименование роли пользователя 
        /// </summary>
        [NotMapped]
        public string RoleName => Role?.Position ?? "Не назначена";
    }
}