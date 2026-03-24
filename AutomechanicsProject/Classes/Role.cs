using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutomechanicsProject.Classes
{
    /// <summary>
    /// Типы ролей пользователей
    /// </summary>
    public enum RoleType
    {
        Administrator = 1,
        Storekeeper = 2
    }

    /// <summary>
    /// Представляет роль пользователя в системе
    /// </summary>
    [Table("role")]
    public class Role
    {
        /// <summary>
        /// Уникальный идентификатор роли
        /// </summary>
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование должности
        /// </summary>
        [Column("position")]
        public string Position { get; set; }

        /// <summary>
        /// Тип роли 
        /// </summary>
        [NotMapped]
        public RoleType Type
        {
            get => Position == "Администратор" ? RoleType.Administrator : RoleType.Storekeeper;
            set => Position = value == RoleType.Administrator ? "Администратор" : "Кладовщик";
        }

        /// <summary>
        /// Коллекция пользователей с данной ролью
        /// </summary>
        public virtual ICollection<Users> Users { get; set; }

        /// <summary>
        /// Определяет, является ли роль администратором
        /// </summary>
        public bool IsAdministrator => Type == RoleType.Administrator;

        /// <summary>
        /// Определяет, является ли роль кладовщиком
        /// </summary>
        public bool IsStorekeeper => Type == RoleType.Storekeeper;

        /// <summary>
        /// Создает экземпляр роли на основе указанного типа
        /// </summary>
        public static Role FromType(RoleType type) => new Role { Type = type };

        /// <summary>
        /// Возвращает строковое представление роли
        /// </summary>
        public override string ToString() => Type == RoleType.Administrator ? "Администратор" : "Кладовщик";
    }
}