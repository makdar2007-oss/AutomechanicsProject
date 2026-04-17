using AutomechanicsProject.Classes;
using AutomechanicsProject.Dtos;
using AutomechanicsProject.Dtos.UI;
using AutomechanicsProject.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AutomechanicsProject.Helpers
{
    /// <summary>
    /// Вспомогательный класс для работы с выпадающим списком
    /// </summary>
    public static class ComboBoxHelper
    {
        /// <summary>
        /// Привязывает данные 
        /// </summary>
        public static void Bind<T>(ComboBox comboBox, List<T> data, string displayMember = "Text", string valueMember = "Id")
        {
            comboBox.DataSource = null;
            comboBox.DataSource = data;
            comboBox.DisplayMember = displayMember;
            comboBox.ValueMember = valueMember;
            comboBox.SelectedIndex = -1;
        }

        /// <summary>
        /// Загружает категории 
        /// </summary>
        public static void LoadCategories(ComboBox comboBox, DateBase db, bool showProductCount = false)
        {
            var categories = db.Categories
                .OrderBy(c => c.Name)
                .Select(c => new ComboItemDto
                {
                    Id = c.Id,
                    Text = showProductCount
                        ? FormatHelper.FormatCategoryWithCount(c.Name, db.Products.Count(p => p.CategoryId == c.Id))
                        : c.Name
                })
                .ToList();

            Bind(comboBox, categories);
        }

        /// <summary>
        /// Загружает единицы измерения 
        /// </summary>
        public static void LoadUnits(ComboBox comboBox, DateBase db)
        {
            var units = db.Units
                .OrderBy(u => u.Name)
                .Select(u => new ComboItemDto
                {
                    Id = u.Id,
                    Text = FormatHelper.FormatUnitDisplay(u.Name, u.ShortName)
                })
                .ToList();

            Bind(comboBox, units);
        }

        /// <summary>
        /// Загружает поставщиков 
        /// </summary>
        public static void LoadSuppliers(ComboBox comboBox, DateBase db)
        {
            var suppliers = db.Suppliers
                .OrderBy(s => s.Name)
                .Select(s => new ComboItemDto
                {
                    Id = s.Id,
                    Text = s.Name
                })
                .ToList();

            Bind(comboBox, suppliers);
        }

        /// <summary>
        /// Загружает получателей 
        /// </summary>
        public static void LoadRecipients(ComboBox comboBox, DateBase db)
        {
            var recipients = db.Addresses
                .Where(a => a.CompanyName != null && a.CompanyName.Trim() != "" && a.CompanyName.Trim() != "-")
                .OrderBy(a => a.CompanyName)
                .Select(a => new ComboItemDto
                {
                    Id = a.Id,
                    Text = a.CompanyName
                })
                .ToList();

            Bind(comboBox, recipients);
        }

        /// <summary>
        /// Получает выбранный элемент 
        /// </summary>
        public static ComboItemDto GetSelectedItem(ComboBox comboBox)
        {
            return comboBox.SelectedItem as ComboItemDto;
        }

        /// <summary>
        /// Получает ID выбранного элемента
        /// </summary>
        public static Guid? GetSelectedId(ComboBox comboBox)
        {
            return GetSelectedItem(comboBox)?.Id;
        }

        /// <summary>
        /// Устанавливает выбранный элемент по ID
        /// </summary>
        public static void SetSelectedById(ComboBox comboBox, Guid id)
        {
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                if (comboBox.Items[i] is ComboItemDto item && item.Id == id)
                {
                    comboBox.SelectedIndex = i;
                    break;
                }
            }
        }

        /// <summary>
        /// Проверяет, выбран ли элемент 
        /// </summary>
        public static bool IsSelected(ComboBox comboBox)
        {
            return comboBox.SelectedItem != null;
        }

        /// <summary>
        /// Очищает выбранный элемент
        /// </summary>
        public static void ClearSelection(ComboBox comboBox)
        {
            comboBox.SelectedIndex = -1;
        }
    }
}