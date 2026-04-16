namespace AutomechanicsProject.ViewModels
{
    /// <summary>
    /// для отображения отгрузки
    /// </summary>
    public class ShipmentViewModel
    {
        public string Article { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Profit { get; set; }
        public decimal Total { get; set; }
        public string RecipientName { get; set; }

        public string TotalText => $"{Total:C}";
        public string ProfitText => $"{Profit:C}";
    }
}