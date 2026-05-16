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
    /// Форма тепловой карты склада
    /// </summary>
    public partial class WarehouseHeatmapForm : Form
    {
        private const int ColumnsCount = 6;
        private const int LowStockLimit = 10;

        private readonly IWarehouseHeatmapService _warehouseHeatmapService;

        private List<WarehouseCellViewModel> _allCells = new List<WarehouseCellViewModel>();
        private string _searchText = "";

        /// <summary>
        /// Создает форму тепловой карты склада
        /// </summary>
        public WarehouseHeatmapForm(IWarehouseHeatmapService warehouseHeatmapService)
        {
            InitializeComponent();

            _warehouseHeatmapService = warehouseHeatmapService ?? throw new ArgumentNullException(nameof(warehouseHeatmapService));

            ConfigureGrid();
            ConfigureSearch();

            Load += WarehouseHeatmapForm_Load;
        }

        private void WarehouseHeatmapForm_Load(object sender, EventArgs e)
        {
            RefreshWarehouse();
        }

        private void ConfigureGrid()
        {
            dataGridViewWarehouse.AllowUserToAddRows = false;
            dataGridViewWarehouse.AllowUserToDeleteRows = false;
            dataGridViewWarehouse.AllowUserToResizeRows = false;
            dataGridViewWarehouse.ReadOnly = true;
            dataGridViewWarehouse.RowHeadersVisible = true;
            dataGridViewWarehouse.ColumnHeadersVisible = true;
            dataGridViewWarehouse.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridViewWarehouse.MultiSelect = false;
            dataGridViewWarehouse.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewWarehouse.BackgroundColor = Color.White;
            dataGridViewWarehouse.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewWarehouse.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridViewWarehouse.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridViewWarehouse.CellClick -= DataGridViewWarehouse_CellClick;
            dataGridViewWarehouse.CellClick += DataGridViewWarehouse_CellClick;

            dataGridViewWarehouse.CellMouseEnter -= DataGridViewWarehouse_CellMouseEnter;
            dataGridViewWarehouse.CellMouseEnter += DataGridViewWarehouse_CellMouseEnter;
        }

        private void ConfigureSearch()
        {
            txtSearch.TextChanged -= TxtSearch_TextChanged;
            txtSearch.TextChanged += TxtSearch_TextChanged;

            txtSearch.KeyDown -= TxtSearch_KeyDown;
            txtSearch.KeyDown += TxtSearch_KeyDown;
        }

        private void RefreshWarehouse()
        {
            _warehouseHeatmapService.EnsureCellsForProducts();
            _allCells = _warehouseHeatmapService.GetWarehouseCells();

            RenderGrid();
            UpdateSummary();
            ClearProductCard();
        }

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

            var maxRow = _allCells.Count == 0 ? 0 : _allCells.Max(c => c.Row);

            for (var row = 0; row <= maxRow; row++)
            {
                var rowIndex = dataGridViewWarehouse.Rows.Add();

                dataGridViewWarehouse.Rows[rowIndex].HeaderCell.Value = (row + 1).ToString();
                dataGridViewWarehouse.Rows[rowIndex].Height = 80;

                for (var column = 0; column < ColumnsCount; column++)
                {
                    var cell = _allCells.FirstOrDefault(c => c.Row == row && c.Column == column);

                    FillGridCell(rowIndex, column, cell);
                }
            }
        }

        private void FillGridCell(int rowIndex, int columnIndex, WarehouseCellViewModel cell)
        {
            var gridCell = dataGridViewWarehouse.Rows[rowIndex].Cells[columnIndex];

            if (cell == null || cell.IsEmpty)
            {
                gridCell.Value = "Пусто";
                gridCell.Tag = null;
                gridCell.Style.BackColor = Color.LightGray;
                gridCell.Style.ForeColor = Color.DimGray;
                gridCell.ToolTipText = "Свободная ячейка";
                return;
            }

            var isMatch = IsSearchMatch(cell);
            var baseColor = GetCellColor(cell);

            gridCell.Value = cell.ProductName + Environment.NewLine + cell.Balance + " шт.";
            gridCell.Tag = cell;
            gridCell.Style.BackColor = GetSearchColor(baseColor, isMatch);
            gridCell.Style.ForeColor = GetSearchTextColor(isMatch);
            gridCell.ToolTipText =
                "Ячейка: " + cell.CellCode + Environment.NewLine +
                "Товар: " + cell.ProductName + Environment.NewLine +
                "Артикул: " + cell.Article + Environment.NewLine +
                "Остаток: " + cell.Balance + " шт." + Environment.NewLine +
                "Категория: " + GetTextOrDash(cell.CategoryName) + Environment.NewLine +
                "Срок годности: " + GetExpiryText(cell);
        }

        private void DataGridViewWarehouse_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            var cell = dataGridViewWarehouse.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag as WarehouseCellViewModel;

            if (cell == null || cell.IsEmpty)
            {
                ClearProductCard();

                MessageBox.Show("Ячейка пуста",
                    "Склад",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            ShowProductCard(cell);
        }

        private void DataGridViewWarehouse_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            dataGridViewWarehouse.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
        }

        private bool IsSearchMatch(WarehouseCellViewModel cell)
        {
            if (string.IsNullOrWhiteSpace(_searchText) || _searchText.Length < 3)
            {
                return false;
            }

            var cellCode = cell.CellCode == null ? "" : cell.CellCode.ToLower();
            var name = cell.ProductName == null ? "" : cell.ProductName.ToLower();
            var article = cell.Article == null ? "" : cell.Article.ToLower();
            var category = cell.CategoryName == null ? "" : cell.CategoryName.ToLower();

            return cellCode.Contains(_searchText) ||
                   name.Contains(_searchText) ||
                   article.Contains(_searchText) ||
                   category.Contains(_searchText);
        }

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

            if (cell.HasExpiryDate && cell.ExpiryDate.HasValue)
            {
                var days = (cell.ExpiryDate.Value.Date - DateTime.Now.Date).Days;

                if (days <= 7)
                {
                    return Color.Orange;
                }

                if (days <= 30)
                {
                    return Color.Khaki;
                }

                return Color.LightGreen;
            }

            if (cell.Balance < LowStockLimit)
            {
                return Color.LightCoral;
            }

            return Color.LightSkyBlue;
        }

        private Color GetSearchColor(Color baseColor, bool isMatch)
        {
            if (string.IsNullOrWhiteSpace(_searchText) || _searchText.Length < 3)
            {
                return baseColor;
            }

            if (isMatch)
            {
                return Color.Gold;
            }

            return ControlPaint.Light(baseColor, 0.7f);
        }

        private Color GetSearchTextColor(bool isMatch)
        {
            if (string.IsNullOrWhiteSpace(_searchText) || _searchText.Length < 3)
            {
                return Color.Black;
            }

            return isMatch ? Color.Black : Color.Gray;
        }

        private void ShowProductCard(WarehouseCellViewModel cell)
        {
            lblCardName.Text = GetTextOrDash(cell.ProductName);
            lblCardCategory.Text = GetTextOrDash(cell.CategoryName);
            lblCardStock.Text = cell.Balance.ToString();
            lblCardExpiry.Text = GetExpiryText(cell);
            lblCardCell.Text = GetTextOrDash(cell.CellCode);
        }

        private void ClearProductCard()
        {
            lblCardName.Text = "—";
            lblCardCategory.Text = "—";
            lblCardStock.Text = "—";
            lblCardExpiry.Text = "—";
            lblCardCell.Text = "—";
        }

        private string GetExpiryText(WarehouseCellViewModel cell)
        {
            if (!cell.HasExpiryDate || !cell.ExpiryDate.HasValue)
            {
                return "Не требуется";
            }

            return cell.ExpiryDate.Value.ToString("dd.MM.yyyy");
        }

        private string GetTextOrDash(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return "—";
            }

            return text;
        }

        private void UpdateSummary()
        {
            var filledCells = _allCells.Where(c => !c.IsEmpty).ToList();

            var total = filledCells.Count;
            var lowStock = filledCells.Count(c => c.Balance > 0 && c.Balance < LowStockLimit);

            var expSoon = 0;
            var expNormal = 0;

            foreach (var cell in filledCells)
            {
                if (!cell.HasExpiryDate || !cell.ExpiryDate.HasValue)
                {
                    continue;
                }

                var days = (cell.ExpiryDate.Value.Date - DateTime.Now.Date).Days;

                if (days <= 7)
                {
                    expSoon++;
                }
                else if (days <= 30)
                {
                    expNormal++;
                }
            }

            lblTotalCountcount.Text = total.ToString();
            lblExpSooncount.Text = expSoon.ToString();
            lblExpNormalcount.Text = expNormal.ToString();
            lblLowStockcount.Text = lowStock.ToString();
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            _searchText = txtSearch.Text.Trim().ToLower();

            RenderGrid();
        }

        private void ShowSearchMessage()
        {
            if (string.IsNullOrWhiteSpace(_searchText) || _searchText.Length < 3)
            {
                MessageBox.Show("Введите минимум 3 символа для поиска",
                    "Поиск",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            var count = _allCells.Count(c => !c.IsEmpty && IsSearchMatch(c));

            if (count == 0)
            {
                MessageBox.Show("Ничего не найдено",
                    "Результат поиска",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            MessageBox.Show("Найдено: " + count + " ячеек",
                "Результат поиска",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            e.SuppressKeyPress = true;

            _searchText = txtSearch.Text.Trim().ToLower();

            RenderGrid();
            ShowSearchMessage();
        }
        /// <summary>
        /// Освобождает ячейку товара и обновляет карту
        /// </summary>
        public void FreeCellByProduct(Guid productId)
        {
            _warehouseHeatmapService.FreeCellByProduct(productId);
            RefreshWarehouse();
        }

        /// <summary>
        /// Закрепляет товар за ячейкой и обновляет карту
        /// </summary>
        public void EnsureProductHasCell(Guid productId)
        {
            _warehouseHeatmapService.EnsureProductHasCell(productId);
            RefreshWarehouse();
        }
    }
}