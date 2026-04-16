using AutomechanicsProject.Formes;
using System;

namespace AutomechanicsProject.Dtos
{
    /// <summary>
    /// DTO для отображения товара в таблице
    /// </summary>
    public class ProductDisplayDto
    {
        public Guid Id { get; set; }
        public string Article { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string UnitName { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public decimal Balance { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SellingPrice { get; set; }
        public bool RequiresDiscount { get; set; }
        public bool IsExpired { get; set; }

        public string Артикул => Article;
        public string Название => Name;
        public string Категория => Category;
        public string ЕдИзмерения => UnitName;
        public DateTime? СрокГодности => ExpiryDate;
        public decimal Остаток => Balance;
        public decimal ЦенаЗакупки => PurchasePrice;
        public decimal Цена => ChoosingCurrency.ConvertPrice(SellingPrice);
        public bool ТребуетСкидки => RequiresDiscount;
        public bool Просрочен => IsExpired;
    }
}