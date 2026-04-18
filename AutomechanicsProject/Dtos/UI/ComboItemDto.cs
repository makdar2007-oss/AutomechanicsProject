using System;

namespace AutomechanicsProject.Dtos.UI
{
    /// <summary>
    /// Универсальный DTO для выпадающих списков
    /// </summary>
    public class ComboItemDto
    {
        /// <summary>
        /// Уникальный идентификатор 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наззвание 
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Информация для отображения 
        /// </summary>
        public string Tooltip { get; set; }
    }
}