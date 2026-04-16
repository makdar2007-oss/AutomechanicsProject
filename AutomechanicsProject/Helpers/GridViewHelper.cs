using System;
using System.Drawing;
using System.Windows.Forms;
using AutomechanicsProject.Properties;

namespace AutomechanicsProject.Helpers
{
    /// <summary>
    /// Вспомогательный класс для настройки DataGridView
    /// </summary>
    public static class GridViewHelper
    {
        /// <summary>
        /// Цвета для подсветки строк
        /// </summary>
        public static class HighlightColors
        {
            public static Color ExpiredBack => Color.DarkRed;
            public static Color ExpiredFore => Color.White;
            public static Color DiscountBack => Color.LightCoral;
            public static Color DiscountFore => Color.Black;
            public static Color NormalBack => Color.White;
            public static Color NormalFore => Color.Black;
        }

        /// <summary>
        /// Базовые настройки DataGridView
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
        }

        /// <summary>
        /// Подсвечивает строки на основе свойств "Просрочен" и "ТребуетСкидки"
        /// </summary>
        public static void HighlightRowsByFlags(DataGridView grid, string expiredPropertyName = "Просрочен", string discountPropertyName = "ТребуетСкидки")
        {
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.DataBoundItem == null) continue;

                try
                {
                    var isExpired = GetBoolProperty(row.DataBoundItem, expiredPropertyName);
                    var requiresDiscount = GetBoolProperty(row.DataBoundItem, discountPropertyName);

                    if (isExpired)
                    {
                        row.DefaultCellStyle.BackColor = HighlightColors.ExpiredBack;
                        row.DefaultCellStyle.ForeColor = HighlightColors.ExpiredFore;
                    }
                    else if (requiresDiscount)
                    {
                        row.DefaultCellStyle.BackColor = HighlightColors.DiscountBack;
                        row.DefaultCellStyle.ForeColor = HighlightColors.DiscountFore;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = HighlightColors.NormalBack;
                        row.DefaultCellStyle.ForeColor = HighlightColors.NormalFore;
                    }
                }
                catch (Exception ex)
                {
                    Program.LogError("Ошибка при подсветке строки в DataGridView", ex);
                    MessageBox.Show(Resources.ErrorHighlightRow, Resources.TitleError,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// Подсвечивает строки на основе срока годности
        /// </summary>
        public static void HighlightRowsByExpiryDate(DataGridView grid, string expiryDatePropertyName = "СрокГодности")
        {
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.DataBoundItem == null) continue;

                try
                {
                    var expiryDate = GetDateTimeProperty(row.DataBoundItem, expiryDatePropertyName);

                    if (DateTimeHelper.IsExpired(expiryDate))
                    {
                        row.DefaultCellStyle.BackColor = HighlightColors.ExpiredBack;
                        row.DefaultCellStyle.ForeColor = HighlightColors.ExpiredFore;
                    }
                    else if (DateTimeHelper.IsExpiringSoon(expiryDate, 30))
                    {
                        row.DefaultCellStyle.BackColor = HighlightColors.DiscountBack;
                        row.DefaultCellStyle.ForeColor = HighlightColors.DiscountFore;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = HighlightColors.NormalBack;
                        row.DefaultCellStyle.ForeColor = HighlightColors.NormalFore;
                    }
                }
                catch (Exception ex)
                {
                    Program.LogError("Ошибка при подсветке строки в DataGridView", ex);
                    MessageBox.Show(Resources.ErrorHighlightRow, Resources.TitleError,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// Настраивает колонки для главной формы администратора
        /// </summary>
        public static void ConfigureProductColumns(DataGridView grid, string currencyCode)
        {
            string[] columnOrder = {
                "Артикул", "Название", "Категория", "ЕдИзмерения",
                "СрокГодности", "Цена", "ЦенаЗакупки", "Остаток"
            };

            SetColumnOrder(grid, columnOrder);

            HideColumns(grid, "ТребуетСкидки", "Просрочен");

            ConfigurePriceColumn(grid, "Цена", currencyCode);

            ConfigureDecimalColumn(grid, "ЦенаЗакупки", "Цена закупки (₽)");

            ConfigureDateColumn(grid, "СрокГодности", "Срок годности");

            ConfigureTextColumn(grid, "ЕдИзмерения", "Ед. измерения");

            ConfigureNumericColumn(grid, "Остаток");
        }

        private static void SetColumnOrder(DataGridView grid, string[] columnOrder)
        {
            for (int i = 0; i < columnOrder.Length; i++)
            {
                if (grid.Columns[columnOrder[i]] != null)
                    grid.Columns[columnOrder[i]].DisplayIndex = i;
            }
        }

        private static void HideColumns(DataGridView grid, params string[] columnNames)
        {
            foreach (var columnName in columnNames)
            {
                if (grid.Columns[columnName] != null)
                    grid.Columns[columnName].Visible = false;
            }
        }

        private static void ConfigurePriceColumn(DataGridView grid, string columnName, string currencyCode)
        {
            if (grid.Columns[columnName] != null)
            {
                grid.Columns[columnName].DefaultCellStyle.Format = "F2";
                grid.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                grid.Columns[columnName].HeaderText = $"Цена ({currencyCode})";
            }
        }

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

        private static void ConfigureTextColumn(DataGridView grid, string columnName, string headerText = null)
        {
            if (grid.Columns[columnName] != null && !string.IsNullOrEmpty(headerText))
                grid.Columns[columnName].HeaderText = headerText;
        }

        private static void ConfigureNumericColumn(DataGridView grid, string columnName)
        {
            if (grid.Columns[columnName] != null)
                grid.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private static bool GetBoolProperty(object obj, string propertyName)
        {
            var prop = obj.GetType().GetProperty(propertyName);
            return prop != null && (bool)prop.GetValue(obj);
        }

        private static DateTime? GetDateTimeProperty(object obj, string propertyName)
        {
            var prop = obj.GetType().GetProperty(propertyName);
            return prop?.GetValue(obj) as DateTime?;
        }
    }
}