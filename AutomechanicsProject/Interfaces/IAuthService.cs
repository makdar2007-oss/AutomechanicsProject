using AutomechanicsProject.Classes;

namespace AutomechanicsProject.Services.Interfaces
{
    /// <summary>
    /// Сервис авторизации
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Выполняет вход пользователя
        /// </summary>
        Users Login(string login, string password);
    }
}