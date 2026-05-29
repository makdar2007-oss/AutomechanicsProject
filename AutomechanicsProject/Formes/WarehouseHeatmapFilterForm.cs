using AutomechanicsProject.Classes;
using AutomechanicsProject.Properties;
using AutomechanicsProject.Services.Interfaces;
using AutomechanicsProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AutomechanicsProject.Formes
{
    /// <summary>
    /// Форма фильтрации тепловой карты склада
    /// </summary>
    public partial class WarehouseHeatmapFilterForm : Form
    {
        private const int ColumnsCount = 6;
        private const int LowStockLimit = 10;

        private readonly IWarehouseHeatmapService _warehouseHeatmapService;

        private List<WarehouseCellViewModel> _allCells = new List<WarehouseCellViewModel>();
        private List<WarehouseCellViewModel> _visibleCells = new List<WarehouseCellViewModel>();
        private bool _isSortWatermark = true;

        /// <summary>
        /// Создает форму фильтрации тепловой карты склада
        /// </summary>
        public WarehouseHeatmapFilterForm(IWarehouseHeatmapService warehouseHeatmapService)
        {
            InitializeComponent();

            _warehouseHeatmapService = warehouseHeatmapService ?? throw new ArgumentNullException(nameof(warehouseHeatmapService));

            ApplyLocalization();
            ConfigureSort();

            Load += WarehouseHeatmapFilterForm_Load;
        }

        /// <summary>
        /// Применяет текст из ресурсов к элементам формы
        /// </summary>
        private void ApplyLocalization()
        {
            Text = Resources.WarehouseFilter_Title;

            labelmain.Text = Resources.WarehouseFilter_Title;
            labelSortBy.Text = Resources.WarehouseFilter_SortBy;
            buttonApply.Text = Resources.WarehouseFilter_Apply;
            buttonReset.Text = Resources.WarehouseFilter_Reset;

            labelSortBy.Visible = false;

            comboBoxSortBy.Items.Clear();
            comboBoxSortBy.Items.Add(Resources.WarehouseFilter_SortExpiry);
            comboBoxSortBy.Items.Add(Resources.WarehouseFilter_SortQuantity);

            SetSortWatermark();

            labellegend.Text = Resources.Warehouse_LegendTitle;
            labelgreent.Text = Resources.Warehouse_LegendGreen;
            labelyellowt.Text = Resources.Warehouse_LegendYellow;
            labeloranget.Text = Resources.Warehouse_LegendOrange;
            labelredt.Text = Resources.Warehouse_LegendRed;
            labelbluet.Text = Resources.Warehouse_LegendBlue;
            laberule.Text = Resources.Warehouse_LegendRule;
        }

        /// <summary>
        /// Устанавливает подсказку в список сортировки
        /// </summary>
        private void SetSortWatermark()
        {
            _isSortWatermark = true;
            comboBoxSortBy.SelectedIndex = -1;
            comboBoxSortBy.Text = Resources.WarehouseFilter_SortBy;
            comboBoxSortBy.ForeColor = Color.Gray;
        }

        /// <summary>
        /// Убирает подсказку из списка сортировки
        /// </summary>
        private void RemoveSortWatermark()
        {
            if (!_isSortWatermark)
            {
                return;
            }

            _isSortWatermark = false;
            comboBoxSortBy.Text = "";
            comboBoxSortBy.ForeColor = Color.Black;
        }

        /// <summary>
        /// Обрабатывает вход в список сортировки
        /// </summary>
        private void comboBoxSortBy_Enter(object sender, EventArgs e)
        {
            RemoveSortWatermark();
        }

        /// <summary>
        /// Обрабатывает выход из списка сортировки
        /// </summary>
        private void comboBoxSortBy_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBoxSortBy.Text))
            {
                SetSortWatermark();
            }
        }

        /// <summary>
        /// Запрещает ручной ввод в список сортировки
        /// </summary>
        private void comboBoxSortBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        /// <summary>
        /// Настраивает выбранный вариант сортировки
        /// </summary>
        private void ConfigureSort()
        {
            if (comboBoxSortBy.SelectedIndex < 0)
            {
                comboBoxSortBy.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Загружает данные при открытии формы
        /// </summary>
        private void WarehouseHeatmapFilterForm_Load(object sender, EventArgs e)
        {
            RefreshWarehouse();
        }

        /// <summary>
        /// Загружает ячейки склада
        /// </summary>
        private void RefreshWarehouse()
        {
            _warehouseHeatmapService.EnsureCellsForProducts();

            _allCells = _warehouseHeatmapService.GetWarehouseCells();

            ResetSorting();
        }


        /// <summary>
        /// Сортирует ячейки по сроку годности
        /// </summary>
        private void SortByExpiryDate()
        {
            _visibleCells = _allCells
                .OrderBy(c => IsEmptyForExpirySort(c) ? 1 : 0)
                .ThenBy(c => IsEmptyForExpirySort(c) ? DateTime.MaxValue : GetExpirySortDate(c))
                .ThenBy(c => IsEmptyForExpirySort(c) ? string.Empty : c.ProductName)
                .ThenBy(c => c.Row)
                .ThenBy(c => c.Column)
                .ToList();

            RenderGrid();
        }
        /// <summary>
        /// Проверяет, нужно ли считать ячейку пустой при сортировке по сроку
        /// </summary>
        private bool IsEmptyForExpirySort(WarehouseCellViewModel cell)
        {
            return cell == null || cell.IsEmpty || cell.Balance <= 0;
        }
        /// <summary>
        /// Возвращает дату для сортировки по сроку годности
        /// </summary>
        private DateTime GetExpirySortDate(WarehouseCellViewModel cell)
        {
            if (!cell.HasExpiryDate || !cell.ExpiryDate.HasValue)
            {
                return DateTime.MaxValue;
            }

            return cell.ExpiryDate.Value.Date;
        }
        /// <summary>
        /// Сортирует ячейки по количеству
        /// </summary>
        private void SortByQuantity()
        {
            _visibleCells = _allCells
                .OrderBy(c => c.IsEmpty ? 1 : 0)
                .ThenBy(c => c.Balance)
                .ThenBy(c => c.ProductName)
                .ThenBy(c => c.Row)
                .ThenBy(c => c.Column)
                .ToList();

            RenderGrid();
        }

        /// <summary>
        /// Возвращает стандартный порядок ячеек
        /// </summary>
        private void ResetSorting()
        {
            _visibleCells = _allCells
                .OrderBy(c => c.Row)
                .ThenBy(c => c.Column)
                .ToList();

            RenderGrid();
        }

        /// <summary>
        /// Отрисовывает таблицу тепловой карты
        /// </summary>
        private void RenderGrid()
        {
            dataGridViewWarehouse.Columns.Clear();
            dataGridViewWarehouse.Rows.Clear();

            for (var column = 0; column < ColumnsCount; column++)
            {
                var columnName = ((char)('A' + column)).ToString();

                dataGridViewWarehouse.Columns.Add(columnName, columnName);
                dataGridViewWarehouse.Columns[column].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            var rowsCount = _visibleCells.Count == 0
                ? 1
                : (int)Math.Ceiling(_visibleCells.Count / (decimal)ColumnsCount);

            for (var row = 0; row < rowsCount; row++)
            {
                var rowIndex = dataGridViewWarehouse.Rows.Add();

                
                dataGridViewWarehouse.Rows[rowIndex].Height = 80;

                for (var column = 0; column < ColumnsCount; column++)
                {
                    var index = row * ColumnsCount + column;
                    var cell = index < _visibleCells.Count ? _visibleCells[index] : null;

                    FillGridCell(rowIndex, column, cell);
                }
            }
        }

        /// <summary>
        /// Заполняет ячейку таблицы
        /// </summary>
        private void FillGridCell(int rowIndex, int columnIndex, WarehouseCellViewModel cell)
        {
            var gridCell = dataGridViewWarehouse.Rows[rowIndex].Cells[columnIndex];

            if (cell == null || cell.IsEmpty)
            {
                gridCell.Value = Resources.Warehouse_EmptyCell;
                gridCell.Tag = null;
                gridCell.Style.BackColor = Color.LightGray;
                gridCell.Style.ForeColor = Color.DimGray;
                gridCell.ToolTipText = Resources.Warehouse_FreeCell;

                return;
            }

            gridCell.Value = cell.ProductName + Environment.NewLine + cell.Balance + " " + Resources.Warehouse_UnitPieceShort;
            gridCell.Tag = cell;
            gridCell.Style.BackColor = GetCellColor(cell);
            gridCell.Style.ForeColor = Color.Black;
            gridCell.ToolTipText =
                string.Format(Resources.Warehouse_CellCodeLabel, cell.CellCode) + Environment.NewLine +
                string.Format(Resources.Warehouse_ProductNameLabel, cell.ProductName) + Environment.NewLine +
                string.Format(Resources.Warehouse_ArticleLabel, cell.Article) + Environment.NewLine +
                string.Format(Resources.Warehouse_BalanceLabel, cell.Balance) + Environment.NewLine +
                string.Format(Resources.Warehouse_CategoryLabel, GetTextOrDash(cell.CategoryName)) + Environment.NewLine +
                string.Format(Resources.Warehouse_ExpiryLabel, GetExpiryText(cell));
        }

        /// <summary>
        /// Возвращает цвет ячейки
        /// </summary>
        private Color GetCellColor(WarehouseCellViewModel cell)
        {
            if (cell.IsEmpty)
            {
                return Color.LightGray;
            }

            if (cell.Balance <= 0)
            {
                return Color.LightGray;
            }

            if (cell.Balance < LowStockLimit)
            {
                return Color.FromArgb(255, 128, 128);
            }

            if (cell.HasExpiryDate && cell.ExpiryDate.HasValue)
            {
                var days = (cell.ExpiryDate.Value.Date - MoscowTime.Today).Days;

                if (days <= 7)
                {
                    return Color.FromArgb(255, 192, 128);
                }

                if (days <= 30)
                {
                    return Color.FromArgb(255, 255, 128);
                }

                return Color.FromArgb(128, 255, 128);
            }

            return Color.LightSkyBlue;
        }
        /// <summary>
        /// Возвращает текст срока годности
        /// </summary>
        private string GetExpiryText(WarehouseCellViewModel cell)
        {
            if (!cell.HasExpiryDate || !cell.ExpiryDate.HasValue)
            {
                return Resources.Warehouse_NoExpiryRequired;
            }

            return cell.ExpiryDate.Value.ToString("dd.MM.yyyy");
        }

        /// <summary>
        /// Возвращает текст или прочерк
        /// </summary>
        private string GetTextOrDash(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return Resources.Warehouse_Dash;
            }

            return text;
        }

        /// <summary>
        /// Применяет выбранную сортировку
        /// </summary>
        private void buttonApply_Click_1(object sender, EventArgs e)
        {
            if (_isSortWatermark)
            {
                return;
            }

            if (comboBoxSortBy.SelectedIndex == 0)
            {
                SortByExpiryDate();
                return;
            }

            if (comboBoxSortBy.SelectedIndex == 1)
            {
                SortByQuantity();
            }
        }

        /// <summary>
        /// Сбрасывает сортировку
        /// </summary>
        private void buttonReset_Click_1(object sender, EventArgs e)
        {
            ResetSorting();

            SetSortWatermark();
        }
    }
}