using System;

namespace AutomechanicsProject.Dtos.Service
{
    /// <summary>
    /// DTO для единицы измерения
    /// </summary>
    public class UnitDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}