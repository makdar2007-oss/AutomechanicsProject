using System.Threading.Tasks;

namespace AutomechanicsProject.Services.Interfaces
{
    /// <summary>
    /// Описывает сервис для проверки поставщика по ИНН
    /// </summary>
    public interface IDaDataService
    {
        /// <summary>
        /// Проверяет поставщика по ИНН
        /// </summary>
        Task<string> CheckSupplierByInnAsync(string inn);
    }
}