using System;

namespace AutomechanicsProject.Classes
{
    /// <summary>
    /// Хоранение информации о текущем пользователе
    /// </summary>
    public static class CurrentUser
    {
        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// </summary>
        public static Guid Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public static string Username { get; set; }

        /// <summary>
        /// Роль пользователя
        /// </summary>
        public static string Role { get; set; }

        /// <summary>
        /// Флаг на авторизованность пользователя
        /// </summary>
        public static bool IsAuthenticated { get; set; } = false;

        /// <summary>
        /// Очищает данные о пользователе
        /// </summary>
        public static void Clear()
        {
            Id = Guid.Empty;
            Username = null;
            Role = null;
            IsAuthenticated = false;
        }
    }
}