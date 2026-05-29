using AutomechanicsProject.Dtos;
using System.Threading.Tasks;

namespace AutomechanicsProject.Services.Interfaces
{
    /// <summary>
    /// Описывает сервис для проверки контрагента по ИНН
    /// </summary>
    public interface IDaDataService
    {
        /// <summary>
        /// Проверяет контрагента по ИНН
        /// </summary>
        Task<CounterpartyCheckResultDto> CheckCounterpartyByInnAsync(string inn);
    }
}