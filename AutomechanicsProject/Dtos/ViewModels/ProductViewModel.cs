using System;

namespace AutomechanicsProject.ViewModels
{
    /// <summary>
    /// ViewModel для отображения товара в таблице
    /// </summary>
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Article { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string UnitName { get; set; }
        public string ExpiryDateText { get; set; }
        public int Balance { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal Price { get; set; }
        public string BatchNumber { get; set; }

        public string DisplayName => $"{Article} - {Name}";
        public string BalanceWithUnit => $"{Balance} {UnitName}";
    }
}