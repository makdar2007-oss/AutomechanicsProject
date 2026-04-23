namespace AutomechanicsProject.Classes
{
    /// <summary>
    /// Представляет элемент товара для импорта из внешнего источника данных
    /// Используется при массовом добавлении товаров в базу данных
    /// </summary>
    public class ImportProductItem
    {
        /// <summary>
        /// Артикул товара
        /// </summary>
        public string Article { get; set; }

        /// <summary>
        /// Наименование товара
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Категория товара
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Ед измерения
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// Поставщик
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        /// Количество товара
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Цена товара
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Срок годности 
        /// </summary>
        public string ExpiryDate { get; set; }
    }
}