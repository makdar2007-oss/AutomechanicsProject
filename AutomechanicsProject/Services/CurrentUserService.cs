using AutomechanicsProject.Classes;
using AutomechanicsProject.Services.Interfaces;

namespace AutomechanicsProject.Services
{
    /// <summary>
    /// Сервис текущего пользователя
    /// </summary>
    public class CurrentUserService : ICurrentUserService
    {
        /// <summary>
        /// Возвращает текущего авторизованного пользователя
        /// </summary>
        public Users CurrentUser { get; private set; }

        /// <summary>
        /// Устанавливает текущего авторизованного пользователя
        /// </summary>
        public void SetCurrentUser(Users user)
        {
            CurrentUser = user;
        }

        /// <summary>
        /// Очищает текущего авторизованного пользователя
        /// </summary>
        public void Clear()
        {
            CurrentUser = null;
        }
    }
}