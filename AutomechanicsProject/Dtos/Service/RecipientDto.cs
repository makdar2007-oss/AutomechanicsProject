using System;

namespace AutomechanicsProject.Dtos.Service
{
    /// <summary>
    /// DTO для получателя
    /// </summary>
    public class RecipientDto
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
    }
}