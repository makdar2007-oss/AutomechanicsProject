using System;

namespace AutomechanicsProject.Dtos.UI
{
    /// <summary>
    /// DTO для выпадающего списка товаров (с доп. информацией)
    /// </summary>
    public class ProductComboDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }  
        public string Article { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Balance { get; set; }
        public string UnitName { get; set; }
        public Guid UnitId { get; set; }
    }
}