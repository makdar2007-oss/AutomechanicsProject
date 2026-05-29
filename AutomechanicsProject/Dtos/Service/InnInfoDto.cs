namespace AutomechanicsProject.Dtos.Service
{
    /// <summary>
    /// Информация о контрагенте по ИНН
    /// </summary>
    public class InnInfoDto
    {
        /// <summary>
        /// ИНН контрагента
        /// </summary>
        public string Inn { get; set; }

        /// <summary>
        /// Наименование организации
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Город из адреса
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Является ли ИНН действительным
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// Сообщение о результате проверки
        /// </summary>
        public string Message { get; set; }
    }
}