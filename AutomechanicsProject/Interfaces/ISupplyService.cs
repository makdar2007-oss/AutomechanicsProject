using AutomechanicsProject.Classes;
using System;
using System.Collections.Generic;
using AutomechanicsProject.Dtos.UI;
using AutomechanicsProject.ViewModels;

namespace AutomechanicsProject.Services.Interfaces
{
    /// <summary>
    /// Сервис поставок
    /// </summary>
    public interface ISupplyService
    {

        /// <summary>
        /// Получает товары для формы поставки
        /// </summary>
        List<ProductDisplayItem> GetProductsForSupply();

        /// <summary>
        /// Получает поставщиков для выпадающего списка
        /// </summary>
        List<ComboItemDto> GetSuppliersForCombo(); 
        
        /// <summary>
        /// Создаёт поставку
        /// </summary>

        Supply CreateSupply(
            List<SupplyPosition> positions,
            Guid userId,
            string currencyCode,
            decimal exchangeRate);
    }
}