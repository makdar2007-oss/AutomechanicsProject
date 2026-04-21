using AutomechanicsProject.Classes;
using AutomechanicsProject.Dtos.Service;
using AutomechanicsProject.Mappers;
using AutomechanicsProject.Dtos.UI;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Properties;
using System;
using System.Linq;
using System.Windows.Forms;
using NLog;

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
            SetupWatermarks();
            SetupReadOnlyFields();
            LoadCategories();
            LoadUnits();
            GenerateAndSetArticle();
        }

        /// <summary>
        /// Устанавливает название полей
        /// </summary>
        private void SetupWatermarks()
        {
            TextBoxHelper.SetupWatermarkTextBox(textBoxName, Resources.ProductNameWatermark);
            TextBoxHelper.SetupWatermarkTextBox(textBoxPrice, Resources.ProductPriceWatermark);
            TextBoxHelper.SetupWatermarkComboBox(comboBoxCategory, Resources.CategorySelectWatermark);
            TextBoxHelper.SetupWatermarkComboBox(comboBoxUnit, Resources.UnitSelectWatermark);
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
                comboBoxCategory.DisplayMember = "Text";
                comboBoxCategory.ValueMember = "Id";
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
                comboBoxUnit.DisplayMember = "Text";
                comboBoxUnit.ValueMember = "Id";
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
        /// Проверка выбора категории
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
        /// Проверка выбора ед измерения
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
                textBoxArt.Text = "Ошибка";
            }
        }
    }
}