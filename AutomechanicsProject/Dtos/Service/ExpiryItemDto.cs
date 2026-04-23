using System;

namespace AutomechanicsProject.Dtos
{
    /// <summary>
    /// DTO для отображения срока годности товара
    /// </summary>
    public class ExpiryItemDto
    {
        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Текст для отображения в комбобоксе
        /// </summary>
        public string DisplayText { get; set; }

        /// <summary>
        /// Срок годности
        /// </summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// Остаток товара
        /// </summary>
        public int Balance { get; set; }
    }
}