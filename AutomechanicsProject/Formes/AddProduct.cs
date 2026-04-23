using AutomechanicsProject.Classes;
using AutomechanicsProject.Dtos.Service;
using AutomechanicsProject.Dtos.UI;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Mappers;
using AutomechanicsProject.Properties;
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
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Инициализирует новый экземпляр формы добавления товара
        /// </summary>
        public AddProduct(DateBase database)
        {
            InitializeComponent();
            _db = database ?? throw new ArgumentNullException(nameof(database));
            SetupReadOnlyFields();
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
        /// Устанавливает поле только для чтения
        /// </summary>
        private void SetupReadOnlyFields()
        {
            textBoxArt.ReadOnly = true;
        }

        /// <summary>
        /// Загружает список категорий из базы данных в выпадающий список
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
                Logger.Error("Ошибка при загрузке категорий в форму 'Редактирование категорий'", ex);
                MessageBox.Show(Resources.ErrorLoadCategories, Resources.TitleError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Загружает список единиц измерения из базы данных в выпадающий список
        /// </summary>
        private void LoadUnits()
        {
            try
            {
                var units = _db.Units
                    .OrderBy(u => u.Name)
                    .Select(u => new ComboItemDto
                    {
                        Id = u.Id,
                        Text = $"{u.Name} ({u.ShortName})"
                    })
                    .ToList();
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
        /// Обработчик нажатия кнопки "Добавить"
        /// Сохранение нового товара в базу данных
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
        /// Проверка формата цены
        /// </summary>
        internal bool ValidatePrice(out decimal price)
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
        /// Проверка выбора категории
        /// </summary>
        internal bool ValidateCategory()
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
        /// Проверка выбора ед измерения
        /// </summary>
        internal bool ValidateUnit()
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
        /// Добавление товара 
        /// </summary>
        private void AddNewProduct(decimal price)
        {
            try
            {
                var selectedCategory = (ComboItemDto)comboBoxCategory.SelectedItem;
                var selectedUnit = (ComboItemDto)comboBoxUnit.SelectedItem;
                var article = GenerateArticle();

                var createDto = new CreateProductDto
                {
                    Article = article,
                    Name = textBoxName.Text.Trim(),
                    CategoryId = selectedCategory.Id,
                    UnitId = selectedUnit.Id,
                    Price = price,
                    HasExpiryDate = radioButtonHasExpiry.Checked
                };

                var product = ProductMapper.ToEntity(createDto);

                _db.Products.Add(product);
                _db.SaveChanges();

                Logger.Info($"Товар '{product.Article} - {product.Name}' успешно добавлен");
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
        /// Выполняет валидацию обязательных полей формы
        /// </summary>
        internal bool ValidateFields()
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
        /// Обработчик нажатия кнопки Отмена
        /// Запрашивает подтверждение и закрывает форму без сохранения
        /// </summary>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            var hasInput = !((textBoxName.Text == Resources.ProductNameWatermark || string.IsNullOrWhiteSpace(textBoxName.Text)) &&
                   (comboBoxCategory.Text == Resources.CategorySelectWatermark || string.IsNullOrWhiteSpace(comboBoxCategory.Text)) &&
                   (comboBoxUnit.Text == Resources.UnitSelectWatermark || string.IsNullOrWhiteSpace(comboBoxUnit.Text)) &&
                   (textBoxPrice.Text == Resources.ProductPriceWatermark || string.IsNullOrWhiteSpace(textBoxPrice.Text)));

            if (hasInput && !FormHelper.ShowCancelConfirmation(Resources.ConfirmCancelAddProduct))
            {
                return;
            }
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Генерация артикула товара
        /// </summary>
        private string GenerateArticle()
        {
            var selectedCategory = (ComboItemDto)comboBoxCategory.SelectedItem;

            if (selectedCategory == null)
            {
                return GenerateDefaultArticle();
            }

            string categoryPrefix = GetCategoryPrefix(selectedCategory.Text);

            var lastProduct = _db.Products
                .Where(p => p.Article.StartsWith(categoryPrefix))
                .OrderByDescending(p => p.Article)
                .FirstOrDefault();

            if (lastProduct != null && lastProduct.Article.StartsWith(categoryPrefix))
            {
                string numberPart = lastProduct.Article.Substring(categoryPrefix.Length);
                if (int.TryParse(numberPart, out int num))
                {
                    return $"{categoryPrefix}{(num + 1):D4}";
                }
            }

            return $"{categoryPrefix}0001";
        }

        /// <summary>
        /// Генерация обычного артикула (без категории)
        /// </summary>
        private string GenerateDefaultArticle()
        {
            var lastProduct = _db.Products
                .Where(p => p.Article.StartsWith("ART-"))
                .OrderByDescending(p => p.Article)
                .FirstOrDefault();

            if (lastProduct != null && lastProduct.Article.StartsWith("ART-"))
            {
                string lastNumber = lastProduct.Article.Substring(4);
                if (int.TryParse(lastNumber, out int num))
                {
                    return $"ART-{(num + 1):D4}";
                }
            }

            return "ART-0001";
        }

        /// <summary>
        /// Устанавливает сгенерированный артикул
        /// </summary>
        private void GenerateAndSetArticle()
        {
            try
            {
                string newArticle = GenerateArticle();
                textBoxArt.Text = newArticle;
                textBoxArt.ForeColor = System.Drawing.SystemColors.WindowText;
            }
            catch (Exception ex)
            {
                Logger.Error("Ошибка при генерации артикула", ex);
                textBoxArt.Text = Resources.StatusError;
            }
        }

        /// <summary>
        /// Получение префикса для категории
        /// </summary>
        private string GetCategoryPrefix(string categoryName)
        {
            string shortPrefix = categoryName.Length >= 3 ? categoryName.Substring(0, 3).ToUpper() : categoryName.ToUpper();
            return $"{shortPrefix}-";
        }
    }
}