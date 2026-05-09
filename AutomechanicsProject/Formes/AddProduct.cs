using AutomechanicsProject.Classes;
using AutomechanicsProject.Dtos.Service;
using AutomechanicsProject.Dtos.UI;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Mappers;
using AutomechanicsProject.Properties;
using AutomechanicsProject.Services.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AutomechanicsProject.Formes
{
    /// <summary>
    /// Форма для добавления нового товара
    /// </summary>
    public partial class AddProduct : Form
    {
        private readonly DateBase _db;

        /// <summary>
        /// Сервис товаров
        /// </summary>
        private readonly IProductService _productService;

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Конструктор формы
        /// </summary>
        public AddProduct(DateBase database, IProductService productService)
        {
            InitializeComponent();

            _db = database ?? throw new ArgumentNullException(nameof(database));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));

           
            LoadCategories();
            LoadUnits();
            GenerateAndSetArticle();

            TextBoxHelper.SetupWatermarkTextBox(textBoxName, Resources.ProductNameWatermark);
            TextBoxHelper.SetupWatermarkTextBox(textBoxPrice, Resources.ProductPriceWatermark);
            TextBoxHelper.SetupWatermarkComboBox(comboBoxCategory, Resources.CategorySelectWatermark);
            TextBoxHelper.SetupWatermarkComboBox(comboBoxUnit, Resources.UnitSelectWatermark);

            comboBoxCategory.SelectedIndexChanged += ComboBoxCategory_SelectedIndexChanged;
        }

        /// <summary>
        /// При изменении категории генерируем новый артикул
        /// </summary>
        private void ComboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCategory.SelectedItem != null &&
                comboBoxCategory.Text != Resources.CategorySelectWatermark)
            {
                GenerateAndSetArticle();
            }
        }


        /// <summary>
        /// Загружает категории
        /// </summary>
        private void LoadCategories()
        {
            try
            {
                var categories = _db.Categories
                    .OrderBy(c => c.Name)
                    .Select(c => new ComboItemDto
                    {
                        Id = c.Id,
                        Text = c.Name
                    })
                    .ToList();

                comboBoxCategory.DataSource = categories;
                comboBoxCategory.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                Logger.Error("Ошибка при загрузке категорий", ex);
                MessageBox.Show(Resources.ErrorLoadCategories, Resources.TitleError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Загружает единицы измерения
        /// </summary>
        private void LoadUnits()
        {
            try
            {
                var units = _productService.GetUnitsForCombo();

                comboBoxUnit.DataSource = units;
                comboBoxUnit.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                Logger.Error("Ошибка при загрузке единиц измерения", ex);
                MessageBox.Show(Resources.ErrorLoadUnits, Resources.TitleError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Нажатие кнопки добавить
        /// </summary>
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateFields())
            {
                return;
            }

            if (!ValidatePrice(out var price))
            {
                return;
            }

            if (!ValidateCategory())
            {
                return;
            }

            if (!ValidateUnit())
            {
                return;
            }

            AddNewProduct(price);
        }

        /// <summary>
        /// Проверка цены
        /// </summary>
        private bool ValidatePrice(out decimal price)
        {
            if (!Validation.ValidatePrice(textBoxPrice.Text, out price))
            {
                MessageBox.Show(Resources.ErrorEnterCorrectPrice, Resources.TitleWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Проверка категории
        /// </summary>
        private bool ValidateCategory()
        {
            if (comboBoxCategory.SelectedItem == null)
            {
                MessageBox.Show(Resources.ErrorSelectCategory, Resources.TitleWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Проверка единицы измерения
        /// </summary>
        private bool ValidateUnit()
        {
            if (comboBoxUnit.SelectedItem == null)
            {
                MessageBox.Show(Resources.UnitSelectWatermark, Resources.TitleWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Добавляет товар
        /// </summary>
        private void AddNewProduct(decimal price)
        {
            try
            {
                var selectedCategory = (ComboItemDto)comboBoxCategory.SelectedItem;
                var selectedUnit = (ComboItemDto)comboBoxUnit.SelectedItem;

                var createDto = new CreateProductDto
                {
                    Article = GenerateArticle(),
                    Name = textBoxName.Text.Trim(),
                    CategoryId = selectedCategory.Id,
                    UnitId = selectedUnit.Id,
                    Price = price,
                    HasExpiryDate = radioButtonHasExpiry.Checked
                };

                _productService.AddProduct(createDto);

                Logger.Info($"Товар '{createDto.Article} - {createDto.Name}' добавлен");

                MessageBox.Show(Resources.SuccessProductAdded, Resources.TitleSuccess,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                FormHelper.HandleException(Resources.ErrorAddProduct, ex);
            }
        }

        /// <summary>
        /// Проверка полей
        /// </summary>
        private bool ValidateFields()
        {
            if (Validation.IsWatermark(textBoxName.Text, Resources.ProductNameWatermark) ||
                Validation.IsWatermark(textBoxPrice.Text, Resources.ProductPriceWatermark))
            {
                MessageBox.Show(Resources.ErrorFillFields, Resources.TitleWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Отмена
        /// </summary>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Генерация артикула
        /// </summary>
        private string GenerateArticle()
        {
            if (comboBoxCategory.SelectedItem == null)
            {
                return _productService.GenerateDefaultArticle();
            }

            var selectedCategory = (ComboItemDto)comboBoxCategory.SelectedItem;
            return _productService.GenerateArticle(selectedCategory.Text);
        }

        /// <summary>
        /// Устанавливает артикул
        /// </summary>
        private void GenerateAndSetArticle()
        {
            try
            {
                textBoxArt.Text = GenerateArticle();
            }
            catch (Exception ex)
            {
                Logger.Error("Ошибка генерации артикула", ex);
                textBoxArt.Text = Resources.StatusError;
            }
        }
    }
}