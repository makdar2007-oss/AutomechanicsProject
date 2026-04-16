using System;

namespace AutomechanicsProject.Classes
{
    /// <summary>
    /// Вспомогательный класс для отображения товара в комбобоксе
    /// </summary>
    public class ProductDisplayItem
    {
        public Guid Id { get; set; }
        public string Article { get; set; }
        public string Name { get; set; }
        public int Balance { get; set; }
        public bool IsActive { get; set; }

        public DateTime? ProductExpiryDate { get; set; } 
        public bool HasExpiryDate => ProductExpiryDate.HasValue; 

        public string DisplayName => IsActive
            ? $"{Name} (в наличии: {Balance})"
            : $"{Name} (ожидается поставка)";

        public string SearchString => $"{Article} {Name}".ToLower();
    }
}