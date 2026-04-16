using System;

namespace AutomechanicsProject.Dtos.UI
{
    /// <summary>
    /// Универсальный DTO для выпадающих списков
    /// </summary>
    public class ComboItemDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }

        public string Tooltip { get; set; }
    }
}