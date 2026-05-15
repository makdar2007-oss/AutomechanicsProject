using AutomechanicsProject.Classes;
using AutomechanicsProject.Services.Interfaces;
using AutomechanicsProject.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomechanicsProject.Services
{
    /// <summary>
    /// Сервис тепловой карты склада
    /// </summary>
    public class WarehouseHeatmapService : IWarehouseHeatmapService
    {
        private const int ColumnsCount = 6;

        private readonly DateBase _db;

        /// <summary>
        /// Создает сервис тепловой карты склада
        /// </summary>
        public WarehouseHeatmapService(DateBase db)
        {
            _db = db;
        }

        /// <summary>
        /// Получает ячейки склада для тепловой карты
        /// </summary>
        public List<WarehouseCellViewModel> GetWarehouseCells()
        {
            var cells = _db.WarehouseCells
                .Include(c => c.Product)
                .ThenInclude(p => p.Category)
                .OrderBy(c => c.Row)
                .ThenBy(c => c.Column)
                .ToList();

            return cells
                .Select(c => new WarehouseCellViewModel
                {
                    Id = c.Id,
                    Row = c.Row,
                    Column = c.Column,
                    CellCode = GetCellCode(c.Row, c.Column),
                    ProductId = c.Product != null && !c.Product.IsDeleted
                        ? c.ProductId
                        : null,
                    ProductName = c.Product != null && !c.Product.IsDeleted
                        ? c.Product.Name
                        : null,
                    Article = c.Product != null && !c.Product.IsDeleted
                        ? c.Product.Article
                        : null,
                    CategoryName = c.Product != null && !c.Product.IsDeleted && c.Product.Category != null
                        ? c.Product.Category.Name
                        : null,
                    Balance = c.Product != null && !c.Product.IsDeleted
                        ? c.Product.Balance
                        : 0,
                    ExpiryDate = c.Product != null && !c.Product.IsDeleted
                        ? c.Product.ExpiryDate
                        : null,
                    HasExpiryDate = c.Product != null &&
                                    !c.Product.IsDeleted &&
                                    c.Product.ExpiryDate.HasValue
                })
                .ToList();
        }

        /// <summary>
        /// Проверяет и создает ячейки для неудаленных товаров с остатком
        /// </summary>
        public void EnsureCellsForProducts()
        {
            FreeDeletedProductCells();

            var products = _db.Products
                .Where(p => !p.IsDeleted && p.Balance > 0)
                .OrderBy(p => p.Name)
                .ToList();

            var cells = _db.WarehouseCells
                .OrderBy(c => c.Row)
                .ThenBy(c => c.Column)
                .ToList();

            var changed = false;

            foreach (var product in products)
            {
                var alreadyHasCell = cells.Any(c => c.ProductId == product.Id);

                if (alreadyHasCell)
                {
                    continue;
                }

                var freeCell = cells.FirstOrDefault(c => c.ProductId == null);

                if (freeCell == null)
                {
                    var nextIndex = cells.Count;
                    var row = nextIndex / ColumnsCount;
                    var column = nextIndex % ColumnsCount;

                    freeCell = new WarehouseCell
                    {
                        Id = Guid.NewGuid(),
                        Row = row,
                        Column = column,
                        ProductId = null
                    };

                    _db.WarehouseCells.Add(freeCell);
                    cells.Add(freeCell);
                }

                freeCell.ProductId = product.Id;
                changed = true;
            }

            if (changed)
            {
                _db.SaveChanges();
            }
        }

        /// <summary>
        /// Освобождает ячейку товара
        /// </summary>
        public void FreeCellByProduct(Guid productId)
        {
            var cell = _db.WarehouseCells.FirstOrDefault(c => c.ProductId == productId);

            if (cell == null)
            {
                return;
            }

            cell.ProductId = null;
            _db.SaveChanges();
        }

        /// <summary>
        /// Закрепляет товар за свободной ячейкой
        /// </summary>
        public void EnsureProductHasCell(Guid productId)
        {
            var product = _db.Products
                .FirstOrDefault(p => p.Id == productId && !p.IsDeleted && p.Balance > 0);

            if (product == null)
            {
                return;
            }

            var existingCell = _db.WarehouseCells.FirstOrDefault(c => c.ProductId == productId);

            if (existingCell != null)
            {
                return;
            }

            var freeCell = _db.WarehouseCells
                .OrderBy(c => c.Row)
                .ThenBy(c => c.Column)
                .FirstOrDefault(c => c.ProductId == null);

            if (freeCell == null)
            {
                var nextIndex = _db.WarehouseCells.Count();
                var row = nextIndex / ColumnsCount;
                var column = nextIndex % ColumnsCount;

                freeCell = new WarehouseCell
                {
                    Id = Guid.NewGuid(),
                    Row = row,
                    Column = column,
                    ProductId = null
                };

                _db.WarehouseCells.Add(freeCell);
            }

            freeCell.ProductId = productId;
            _db.SaveChanges();
        }

        /// <summary>
        /// Освобождает ячейки удаленных товаров
        /// </summary>
        public void FreeDeletedProductCells()
        {
            var cells = _db.WarehouseCells
                .Include(c => c.Product)
                .Where(c => c.ProductId != null &&
                            (c.Product == null ||
                             c.Product.IsDeleted))
                .ToList();

            if (!cells.Any())
            {
                return;
            }

            foreach (var cell in cells)
            {
                cell.ProductId = null;
            }

            _db.SaveChanges();
        }

        private string GetCellCode(int row, int column)
        {
            var columnLetter = (char)('A' + column);
            return columnLetter + (row + 1).ToString();
        }
    }
}