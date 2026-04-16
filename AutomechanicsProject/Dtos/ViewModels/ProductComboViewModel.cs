using System;

namespace AutomechanicsProject.ViewModels
{
    /// <summary>
    /// Для выпадающего списка 
    /// </summary>
    public class ProductComboViewModel
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