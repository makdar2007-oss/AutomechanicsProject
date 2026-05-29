using AutomechanicsProject.ViewModels;
using System;
using System.Collections.Generic;

namespace AutomechanicsProject.Services.Interfaces
{
    /// <summary>
    /// Сервис тепловой карты склада
    /// </summary>
    public interface IWarehouseHeatmapService
    {
        /// <summary>
        /// Получает ячейки склада для тепловой карты
        /// </summary>
        List<WarehouseCellViewModel> GetWarehouseCells();

        /// <summary>
        /// Проверяет и создает ячейки для неудаленных товаров с остатком
        /// </summary>
        void EnsureCellsForProducts();

        /// <summary>
        /// Освобождает ячейку товара
        /// </summary>
        void FreeCellByProduct(Guid productId);

        /// <summary>
        /// Закрепляет товар за свободной ячейкой
        /// </summary>
        void EnsureProductHasCell(Guid productId);

        /// <summary>
        /// Освобождает ячейки удаленных товаров
        /// </summary>
        void FreeDeletedProductCells();
    }
}