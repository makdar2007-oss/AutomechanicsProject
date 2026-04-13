using System;

namespace AutomechanicsProject.Classes.Dtos  
{
    public class ProductComboBoxDto
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string ShortName { get; set; }
        public string Article { get; set; }
        public string Name { get; set; }
        public decimal PurchaseCost { get; set; }
        public decimal SellingPrice { get; set; }
        public int Balance { get; set; }
        public string UnitName { get; set; }
        public Guid UnitId { get; set; }
    }
}