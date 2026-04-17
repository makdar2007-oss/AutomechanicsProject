using System.ComponentModel;

namespace AutomechanicsProject.ViewModels
{
    /// <summary>
    /// для отображения отгрузки
    /// </summary>
    public class ShipmentViewModel
    {
        [DisplayName("Артикул")]
        public string Article { get; set; }

        [DisplayName("Наименование")]
        public string Name { get; set; }

        [DisplayName("Количество")]
        public int Quantity { get; set; }

        [DisplayName("Цена")]
        public decimal Price { get; set; }

        [DisplayName("Прибыль")]
        public decimal Profit { get; set; }

        [DisplayName("Сумма")]
        public decimal Total { get; set; }

        [DisplayName("Получатель")]
        public string RecipientName { get; set; }

        [Browsable(false)]
        public string TotalText => $"{Total:C}";

        [Browsable(false)]
        public string ProfitText => $"{Profit:C}";
    }
}