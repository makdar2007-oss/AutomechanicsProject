// Helpers/DataGridViewHelper.cs
using AutomechanicsProject.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutomechanicsProject.Helpers
{
    /// <summary>
    /// Вспомогательный класс для настройки и форматирования DataGridView
    /// </summary>
    public static class DataGridViewHelper
    {
        #region Базовые настройки

        /// <summary>
        /// Настраивает общие свойства DataGridView
        /// </summary>
        public static void ConfigureBasicSettings(DataGridView grid)
        {
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.AllowUserToResizeColumns = false;
            grid.AllowUserToResizeRows = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.RowHeadersVisible = false;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.BackgroundColor = SystemColors.Window;
            grid.BorderStyle = BorderStyle.None;
        }

        #endregion

        #region Настройка колонок для разных форм

        /// <summary>
        /// Настраивает колонки для главной формы администратора
        /// </summary>
        public static void ConfigureProductColumns(DataGridView grid, string currencyCode)
        {
            string[] columnOrder = {
                "Артикул", "Название", "Категория", "ЕдИзмерения",
                "СрокГодности", "Цена", "ЦенаЗакупки", "Остаток"
            };

            HideColumns(grid, "Id", "RequiresDiscount", "IsExpired", "Article", "Name",
                        "Category", "UnitName", "ExpiryDate", "Balance", "PurchasePrice", "SellingPrice");
            SetColumnOrder(grid, columnOrder);
            ConfigurePriceColumn(grid, "Цена", currencyCode);
            ConfigureDecimalColumn(grid, "ЦенаЗакупки", "Цена закупки (₽)");
            ConfigureDateColumn(grid, "СрокГодности", "Срок годности");
            ConfigureTextColumn(grid, "ЕдИзмерения", "Ед. измерения");
            ConfigureNumericColumn(grid, "Остаток");
        }

        /// <summary>
        /// Настраивает колонки для формы кладовщика
        /// </summary>
        public static void ConfigureStorekeeperColumns(DataGridView grid, string currencyCode)
        {
            string[] columnOrder = {
                "Артикул", "Название", "Категория", "ЕдИзмерения",
                "СрокГодности", "Цена", "ЦенаЗакупки", "Остаток"
            };

            HideColumns(grid, "Id", "RequiresDiscount", "IsExpired", "Article", "Name",
                        "Category", "UnitName", "ExpiryDate", "Balance", "PurchasePrice", "SellingPrice");
            SetColumnOrder(grid, columnOrder);
            ConfigurePriceColumn(grid, "Цена", currencyCode);
            ConfigureDecimalColumn(grid, "ЦенаЗакупки", "Цена закупки");
            ConfigureDateColumn(grid, "СрокГодности", "Срок годности");
            ConfigureTextColumn(grid, "ЕдИзмерения", "Ед. изм.");
            ConfigureNumericColumn(grid, "Остаток");
        }

        /// <summary>
        /// Настраивает колонки для формы отгрузки
        /// </summary>
        public static void ConfigureShipmentColumns(DataGridView grid)
        {
            string[] columnOrder = {
                "Артикул", "Название", "Количество", "Цена", "Прибыль", "Сумма", "Кому"
            };

            SetColumnOrder(grid, columnOrder);
            ConfigureDecimalColumn(grid, "Цена", "Цена");
            ConfigureDecimalColumn(grid, "Прибыль", "Прибыль");
            ConfigureDecimalColumn(grid, "Сумма", "Сумма");
            ConfigureNumericColumn(grid, "Количество");

            if (grid.Columns["Кому"] != null)
                grid.Columns["Кому"].HeaderText = "Получатель";
        }

        /// <summary>
        /// Настраивает колонки для формы истории отгрузок
        /// </summary>
        public static void ConfigureHistoryColumns(DataGridView grid)
        {
            string[] columnOrder = {
                "Номер", "Артикул", "Название", "Количество", "Цена", "Прибыль", "Сумма", "Получатель", "Кладовщик", "Дата"
            };

            SetColumnOrder(grid, columnOrder);
            ConfigureDecimalColumn(grid, "Цена", "Цена");
            ConfigureDecimalColumn(grid, "Прибыль", "Прибыль");
            ConfigureDecimalColumn(grid, "Сумма", "Сумма");
            ConfigureNumericColumn(grid, "Количество");
            ConfigureDateColumn(grid, "Дата", "Дата", "dd.MM.yyyy HH:mm");
        }

        /// <summary>
        /// Настраивает колонки для формы отчета
        /// </summary>
        public static void ConfigureReportColumns(DataGridView grid)
        {
            string[] columnOrder = {
                "Номер", "Артикул", "Название", "Количество", "Цена", "Прибыль", "Сумма", "Получатель", "Кладовщик", "Дата"
            };

            SetColumnOrder(grid, columnOrder);
            ConfigureDecimalColumn(grid, "Цена", "Цена");
            ConfigureDecimalColumn(grid, "Прибыль", "Прибыль");
            ConfigureDecimalColumn(grid, "Сумма", "Сумма");
            ConfigureNumericColumn(grid, "Количество");
            ConfigureDateColumn(grid, "Дата", "Дата", "dd.MM.yyyy HH:mm");
        }

        #endregion

        #region Подсветка строк

        /// <summary>
        /// Подсвечивает строки с истекающим/просроченным сроком годности
        /// </summary>
        public static void HighlightExpiryRows(DataGridView grid)
        {
            var today = DateTime.Today;

            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.DataBoundItem == null) continue;

                try
                {
                    var expiryDateProp = row.DataBoundItem.GetType().GetProperty("СрокГодности");
                    if (expiryDateProp == null) continue;

                    var expiryDate = expiryDateProp.GetValue(row.DataBoundItem) as DateTime?;

                    if (expiryDate.HasValue)
                    {
                        if (expiryDate.Value < today)
                        {
                            row.DefaultCellStyle.BackColor = Color.DarkRed;
                            row.DefaultCellStyle.ForeColor = Color.White;
                        }
                        else if ((expiryDate.Value - today).Days <= 30)
                        {
                            row.DefaultCellStyle.BackColor = Color.LightCoral;
                        }
                        else
                        {
                            ResetRowColor(row);
                        }
                    }
                    else
                    {
                        ResetRowColor(row);
                    }
                }
                catch (Exception ex)
                {
                    Program.LogError("Ошибка при подсветке строки в DataGridView", ex);
                }
            }
        }

        /// <summary>
        /// Сбрасывает цвет строки к стандартному
        /// </summary>
        public static void ResetRowColor(DataGridViewRow row)
        {
            row.DefaultCellStyle.BackColor = Color.White;
            row.DefaultCellStyle.ForeColor = Color.Black;
        }

        #endregion

        #region Асинхронные операции

        /// <summary>
        /// Асинхронная загрузка данных в DataGridView
        /// </summary>
        public static async Task LoadDataAsync<T>(
            DataGridView grid,
            Func<Task<List<T>>> loadDataAsync,
            Action<DataGridView> configureColumns = null,
            Action<Exception> onError = null)
        {
            var originalCursor = grid.Cursor;
            var originalEnabled = grid.Enabled;

            grid.Cursor = Cursors.WaitCursor;
            grid.Enabled = false;

            try
            {
                var data = await loadDataAsync();
                grid.DataSource = data;
                configureColumns?.Invoke(grid);
            }
            catch (Exception ex)
            {
                Program.LogError("Ошибка при загрузке данных в DataGridView", ex);
                onError?.Invoke(ex);
                grid.DataSource = null;
            }
            finally
            {
                grid.Cursor = originalCursor;
                grid.Enabled = originalEnabled;
            }
        }

        /// <summary>
        /// Асинхронный экспорт данных в CSV файл
        /// </summary>
        public static async Task<bool> ExportToCsvAsync(DataGridView grid, string defaultFileName, IWin32Window owner = null)
        {
            if (grid.Rows.Count == 0)
            {
                MessageBox.Show(owner ?? grid.FindForm(),
                    Resources.ErrorNoDataToExport,
                    Resources.TitleWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return await Task.Run(() =>
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "CSV файлы (*.csv)|*.csv|Все файлы (*.*)|*.*";
                    saveFileDialog.DefaultExt = "csv";
                    saveFileDialog.FileName = $"{defaultFileName}_{DateTime.Now:yyyyMMdd_HHmmss}";
                    saveFileDialog.Title = Resources.SaveCsvFileTitle;

                    if (saveFileDialog.ShowDialog(owner) == DialogResult.OK)
                    {
                        try
                        {
                            ExportToCsvFile(grid, saveFileDialog.FileName);

                            MessageBox.Show(owner ?? grid.FindForm(),
                                string.Format(Resources.SuccessExportFormat, saveFileDialog.FileName),
                                Resources.TitleSuccess,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(owner ?? grid.FindForm(),
                                string.Format(Resources.ErrorExportToCsvWithMessage, ex.Message),
                                Resources.TitleError,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
                return false;
            });
        }

        #endregion

        #region Приватные методы настройки колонок

        /// <summary>
        /// Скрывает указанные колонки
        /// </summary>
        private static void HideColumns(DataGridView grid, params string[] columnNames)
        {
            foreach (var columnName in columnNames)
            {
                if (grid.Columns[columnName] != null)
                    grid.Columns[columnName].Visible = false;
            }
        }

        /// <summary>
        /// Устанавливает порядок колонок
        /// </summary>
        private static void SetColumnOrder(DataGridView grid, string[] columnOrder)
        {
            for (int i = 0; i < columnOrder.Length; i++)
            {
                if (grid.Columns[columnOrder[i]] != null)
                    grid.Columns[columnOrder[i]].DisplayIndex = i;
            }
        }

        /// <summary>
        /// Настраивает колонку с ценой
        /// </summary>
        private static void ConfigurePriceColumn(DataGridView grid, string columnName, string currencyCode)
        {
            if (grid.Columns[columnName] != null)
            {
                grid.Columns[columnName].DefaultCellStyle.Format = "F2";
                grid.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                grid.Columns[columnName].HeaderText = $"Цена ({currencyCode})";
            }
        }

        /// <summary>
        /// Настраивает десятичную колонку
        /// </summary>
        private static void ConfigureDecimalColumn(DataGridView grid, string columnName, string headerText = null)
        {
            if (grid.Columns[columnName] != null)
            {
                grid.Columns[columnName].DefaultCellStyle.Format = "F2";
                grid.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                if (!string.IsNullOrEmpty(headerText))
                    grid.Columns[columnName].HeaderText = headerText;
            }
        }

        /// <summary>
        /// Настраивает числовую колонку
        /// </summary>
        private static void ConfigureNumericColumn(DataGridView grid, string columnName, string headerText = null)
        {
            if (grid.Columns[columnName] != null)
            {
                grid.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                if (!string.IsNullOrEmpty(headerText))
                    grid.Columns[columnName].HeaderText = headerText;
            }
        }

        /// <summary>
        /// Настраивает текстовую колонку
        /// </summary>
        private static void ConfigureTextColumn(DataGridView grid, string columnName, string headerText = null)
        {
            if (grid.Columns[columnName] != null && !string.IsNullOrEmpty(headerText))
                grid.Columns[columnName].HeaderText = headerText;
        }

        /// <summary>
        /// Настраивает колонку с датой
        /// </summary>
        private static void ConfigureDateColumn(DataGridView grid, string columnName, string headerText = null, string format = "dd.MM.yyyy")
        {
            if (grid.Columns[columnName] != null)
            {
                grid.Columns[columnName].DefaultCellStyle.Format = format;
                grid.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (!string.IsNullOrEmpty(headerText))
                    grid.Columns[columnName].HeaderText = headerText;
            }
        }

        #endregion

        #region Приватные методы экспорта

        /// <summary>
        /// Экспортирует данные из DataGridView в CSV файл
        /// </summary>
        private static void ExportToCsvFile(DataGridView grid, string filePath)
        {
            var sb = new StringBuilder();

            sb.Append('\uFEFF');

            var visibleColumns = grid.Columns.Cast<DataGridViewColumn>()
                .Where(c => c.Visible)
                .OrderBy(c => c.DisplayIndex)
                .ToList();

            var headers = visibleColumns.Select(c => EscapeCsvValue(c.HeaderText)).ToArray();
            sb.AppendLine(string.Join(";", headers));

            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.IsNewRow) continue;

                var rowData = new string[visibleColumns.Count];
                for (int i = 0; i < visibleColumns.Count; i++)
                {
                    var cell = row.Cells[visibleColumns[i].Index];
                    var value = cell.Value?.ToString() ?? "";
                    rowData[i] = EscapeCsvValue(value);
                }
                sb.AppendLine(string.Join(";", rowData));
            }

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        /// <summary>
        /// Экранирует значение для CSV (обрабатывает кавычки, точки с запятой, переводы строк)
        /// </summary>
        private static string EscapeCsvValue(string value)
        {
            if (string.IsNullOrEmpty(value))
                return "";

            bool needsQuoting = value.Contains(";") || value.Contains("\"") || value.Contains("\n") || value.Contains("\r");

            if (needsQuoting)
            {
                value = value.Replace("\"", "\"\"");
                return $"\"{value}\"";
            }

            return value;
        }

        #endregion

        #region Синхронные методы (для обратной совместимости)

        /// <summary>
        /// Синхронная загрузка данных в DataGridView
        /// </summary>
        public static void LoadData<T>(
            DataGridView grid,
            Func<List<T>> loadData,
            Action<DataGridView> configureColumns = null)
        {
            grid.Cursor = Cursors.WaitCursor;
            grid.Enabled = false;

            try
            {
                var data = loadData();
                grid.DataSource = data;
                configureColumns?.Invoke(grid);
            }
            catch (Exception ex)
            {
                Program.LogError("Ошибка при загрузке данных в DataGridView", ex);
                grid.DataSource = null;
                throw;
            }
            finally
            {
                grid.Cursor = Cursors.Default;
                grid.Enabled = true;
            }
        }

        /// <summary>
        /// Синхронный экспорт данных в CSV
        /// </summary>
        public static bool ExportToCsv(DataGridView grid, string defaultFileName)
        {
            if (grid.Rows.Count == 0)
            {
                MessageBox.Show(Resources.ErrorNoDataToExport, Resources.TitleWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "CSV файлы (*.csv)|*.csv|Все файлы (*.*)|*.*";
                saveFileDialog.DefaultExt = "csv";
                saveFileDialog.FileName = $"{defaultFileName}_{DateTime.Now:yyyyMMdd_HHmmss}";
                saveFileDialog.Title = Resources.SaveCsvFileTitle;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        ExportToCsvFile(grid, saveFileDialog.FileName);

                        MessageBox.Show(string.Format(Resources.SuccessExportFormat, saveFileDialog.FileName),
                            Resources.TitleSuccess, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(string.Format(Resources.ErrorExportToCsvWithMessage, ex.Message),
                            Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return false;
        }

        #endregion
    }
}