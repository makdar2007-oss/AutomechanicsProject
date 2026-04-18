using System;

namespace AutomechanicsProject.Dtos.Service
{
    /// <summary>
    /// DTO для категории
    /// </summary>
    public class CategoryDto
    {
        /// <summary>
        /// Уникальный идентификатор категории
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование категории
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Количнство товаров в категории
        /// </summary>
        public int ProductsCount { get; set; }
    }
}