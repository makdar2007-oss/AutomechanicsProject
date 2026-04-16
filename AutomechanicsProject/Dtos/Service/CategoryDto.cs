using System;

namespace AutomechanicsProject.Dtos.Service
{
    /// <summary>
    /// DTO для категории
    /// </summary>
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int ProductsCount { get; set; }
    }
}