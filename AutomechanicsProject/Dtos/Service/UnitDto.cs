using System;

namespace AutomechanicsProject.Dtos.Service
{
    /// <summary>
    /// DTO для единицы измерения
    /// </summary>
    public class UnitDto
    {
        /// <summary>
        /// Уникальный идентификатор ед измерения
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название ед измерения
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Короткое название 
        /// </summary>
        public string ShortName { get; set; }
    }
}