namespace AutomechanicsProject.Classes
{
    public class ImportProductItem
    {
        public string Article { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string UnitName { get; set; }
        public string SupplierName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ExpiryDate { get; set; }
    }
}