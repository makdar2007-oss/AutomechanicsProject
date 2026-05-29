using System.Threading.Tasks;

namespace AutomechanicsProject.Services.Interfaces
{
    /// <summary>
    /// Описывает сервис для проверки погоды
    /// </summary>
    public interface IWeatherService
    {
        /// <summary>
        /// Проверяет, нужна ли термоупаковка по погоде
        /// </summary>
        Task<bool> IsThermoContainerNeededAsync(string city);
    }
}