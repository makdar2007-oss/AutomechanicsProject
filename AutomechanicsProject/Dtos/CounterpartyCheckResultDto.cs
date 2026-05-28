using AutomechanicsProject.Enums;

namespace AutomechanicsProject.Dtos
{
    /// <summary>
    /// Хранит результат проверки контрагента
    /// </summary>
    public class CounterpartyCheckResultDto
    {
        /// <summary>
        /// Получает или задает статус проверки
        /// </summary>
        public CounterpartyCheckStatus Status { get; set; }

        /// <summary>
        /// Получает или задает название контрагента
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Получает или задает сообщение для пользователя
        /// </summary>
        public string Message { get; set; }
    }
}