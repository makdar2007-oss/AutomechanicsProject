using AutomechanicsProject.Classes;

namespace AutomechanicsProject.Services.Interfaces
{
    /// <summary>
    /// Сервис текущего пользователя
    /// </summary>
    public interface ICurrentUserService
    {
        /// <summary>
        /// Возвращает текущего авторизованного пользователя
        /// </summary>
        Users CurrentUser { get; }

        /// <summary>
        /// Устанавливает текущего авторизованного пользователя
        /// </summary>
        void SetCurrentUser(Users user);

        /// <summary>
        /// Очищает текущего авторизованного пользователя
        /// </summary>
        void Clear();
    }
}