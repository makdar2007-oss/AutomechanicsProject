namespace AutomechanicsProject.Enums
{
    /// <summary>
    /// Хранит статус проверки контрагента
    /// </summary>
    public enum CounterpartyCheckStatus
    {
        /// <summary>
        /// Контрагент разрешен
        /// </summary>
        Allowed,

        /// <summary>
        /// У контрагента найден риск
        /// </summary>
        Risk,

        /// <summary>
        /// Контрагент запрещен
        /// </summary>
        Blocked
    }
}