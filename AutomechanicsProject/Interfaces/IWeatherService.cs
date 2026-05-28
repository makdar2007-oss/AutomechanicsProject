using System.Threading.Tasks;

namespace AutomechanicsProject.Services.Interfaces
{
    /// <summary>
    /// Описывает сервис для проверки погоды
    /// </summary>
    public interface IWeatherService
    {
        /// <summary>
        /// Проверяет плохую погоду в городе на ближайшие дни
        /// </summary>
        Task<bool> HasBadWeatherAsync(string city);
    }
}