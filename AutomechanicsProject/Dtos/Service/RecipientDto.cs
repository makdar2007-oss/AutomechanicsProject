using System;

namespace AutomechanicsProject.Dtos.Service
{
    /// <summary>
    /// DTO для получателя
    /// </summary>
    public class RecipientDto
    {
        /// <summary>
        /// Идентификатор получателя
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название компании 
        /// </summary>
        public string CompanyName { get; set; }
    }
}