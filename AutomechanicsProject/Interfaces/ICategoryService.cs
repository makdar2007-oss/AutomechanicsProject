namespace AutomechanicsProject.Services.Interfaces
{
    /// <summary>
    /// Сервис категорий
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Проверяет существование категории
        /// </summary>
        bool CategoryExists(string categoryName);

        /// <summary>
        /// Добавляет категорию
        /// </summary>
        void AddCategory(string categoryName);
    }
}