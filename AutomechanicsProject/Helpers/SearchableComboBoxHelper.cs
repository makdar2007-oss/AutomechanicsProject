using AutomechanicsProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AutomechanicsProject.Helpers
{
    /// <summary>
    /// Вспомогательный класс для настройки ComboBox с поиском
    /// </summary>
    public static class SearchableComboBoxHelper
    {
        /// <summary>
        /// Состояние ComboBox для отслеживания операций очистки/обновления
        /// </summary>
        public class ComboBoxState
        {
            public bool IsClearingText { get; set; } = false;
            public bool IsUpdatingText { get; set; } = false;
            public List<ProductComboViewModel> AllProducts { get; set; }
        }

        /// <summary>
        /// Настраивает ComboBox для поиска товаров
        /// </summary>
        public static void SetupProductSearchComboBox(
            ComboBox comboBox,
            ComboBoxState state,
            List<ProductComboViewModel> allProducts,
            Action<ProductComboViewModel> onProductSelected = null)
        {
            if (comboBox == null)
            {
                return;
            }
            if (state == null)
            {
                return;
            }

            state.AllProducts = allProducts;

            comboBox.DropDownHeight = 200;
            comboBox.IntegralHeight = false;

            comboBox.TextUpdate -= ComboBox_TextUpdate;
            comboBox.DropDown -= ComboBox_DropDown;
            comboBox.SelectionChangeCommitted -= ComboBox_SelectionChangeCommitted;

            comboBox.Tag = new { State = state, OnSelected = onProductSelected };
            comboBox.TextUpdate += ComboBox_TextUpdate;
            comboBox.DropDown += ComboBox_DropDown;
            comboBox.SelectionChangeCommitted += ComboBox_SelectionChangeCommitted;

            LoadProducts(comboBox, allProducts);
        }


        /// <summary>
        /// Фильтрация списка товаров при вводе текста пользователем
        /// </summary>
        private static void ComboBox_TextUpdate(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;
            var tag = comboBox?.Tag;
            if (tag == null)
            {
                return;
            }

            var state = (ComboBoxState)tag.GetType().GetProperty("State")?.GetValue(tag);
            if (state == null || state.IsClearingText || state.IsUpdatingText)
            {
                return;
            }

            var searchText = comboBox.Text;
            if (string.IsNullOrWhiteSpace(searchText))
            {
                ClearAndReloadProducts(comboBox, state);
                return;
            }

            var filtered = state.AllProducts
                .Where(p => p.Name.ToLower().Contains(searchText.ToLower()) ||
                           p.Article.ToLower().Contains(searchText.ToLower()))
                .ToList();

            var currentText = comboBox.Text;
            comboBox.DataSource = null;
            comboBox.DisplayMember = "Text";
            comboBox.ValueMember = "Id";
            comboBox.DataSource = filtered;
            comboBox.Text = currentText;
            comboBox.SelectionStart = comboBox.Text.Length;
            comboBox.DroppedDown = true;
        }


        /// <summary>
        /// Фильтрация списка при открытии выпадающего списка
        /// </summary>
        private static void ComboBox_DropDown(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;
            var tag = comboBox?.Tag;
            if (tag == null)
            {
                return;
            }

            var state = (ComboBoxState)tag.GetType().GetProperty("State")?.GetValue(tag);
            var searchText = comboBox.Text;

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                var filtered = state.AllProducts
                    .Where(p => p.Name.ToLower().Contains(searchText.ToLower()) ||
                               p.Article.ToLower().Contains(searchText.ToLower()))
                    .ToList();

                var currentText = comboBox.Text;
                comboBox.DataSource = null;
                comboBox.DisplayMember = "Text";
                comboBox.ValueMember = "Id";
                comboBox.DataSource = filtered;
                comboBox.Text = currentText;
            }
        }


        /// <summary>
        /// Фильтрация списка при отркытии выпадающего списка
        /// </summary>
        private static void ComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;
            var tag = comboBox?.Tag;
            if (tag == null)
            {
                return;
            }

            var state = (ComboBoxState)tag.GetType().GetProperty("State")?.GetValue(tag);
            var onSelected = (Action<ProductComboViewModel>)tag.GetType().GetProperty("OnSelected")?.GetValue(tag);

            if (comboBox.SelectedItem != null)
            {
                state.IsUpdatingText = true;
                var selected = (ProductComboViewModel)comboBox.SelectedItem;
                comboBox.Text = $"{selected.Article} - {selected.Name}";
                comboBox.SelectionStart = 0;
                comboBox.SelectionLength = 0;
                state.IsUpdatingText = false;
                comboBox.ForeColor = System.Drawing.SystemColors.WindowText;
                comboBox.BackColor = System.Drawing.SystemColors.Window;

                onSelected?.Invoke(selected);
            }
        }

        /// <summary>
        /// Загружает товары в список
        /// </summary>
        public static void LoadProducts(ComboBox comboBox, List<ProductComboViewModel   > products)
        {
            comboBox.DataSource = null;
            comboBox.DisplayMember = "Text";
            comboBox.ValueMember = "Id";
            comboBox.DataSource = products;
        }

        /// <summary>
        /// Очищает ComboBox и загружает все товары
        /// </summary>
        public static void ClearAndReloadProducts(ComboBox comboBox, ComboBoxState state)
        {
            if (comboBox == null)
            {
                return;
            }
            if (state != null)
            {
                state.IsClearingText = true;
            }

            LoadProducts(comboBox, state.AllProducts);
            comboBox.Text = "";
            comboBox.SelectedIndex = -1;

            comboBox.ForeColor = System.Drawing.Color.Gray;
            comboBox.BackColor = System.Drawing.SystemColors.Window;

            if (state != null)
            {
                state.IsClearingText = false;
            }
        }
    }
}