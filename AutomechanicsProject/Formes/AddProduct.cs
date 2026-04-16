using AutomechanicsProject.Classes;
using AutomechanicsProject.Dtos.Service;
using AutomechanicsProject.Dtos.UI;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Properties;
using System;
using System.Linq;
using System.Windows.Forms;
namespace AutomechanicsProject.Formes
{
    /// <summary>
    /// Форма для добавления нового товара
    /// </summary>
    public partial class AddProduct : Form
    {
        private readonly DateBase db;

        /// <summary>
        /// Инициализирует новый экземпляр формы добавления товара
        /// </summary>
        public AddProduct()
        {
            InitializeComponent();
            db = DbContextManager.GetContext();
            DbContextManager.AddReference();
            TextBoxHelper.SetupWatermarkTextBox(textBoxArt, Resources.ProductArticleWatermark);
            TextBoxHelper.SetupWatermarkTextBox(textBoxName, Resources.ProductNameWatermark);
            TextBoxHelper.SetupWatermarkTextBox(textBoxPrice, Resources.ProductPriceWatermark);
            TextBoxHelper.SetupWatermarkComboBox(comboBoxCategory, Resources.CategorySelectWatermark);
            TextBoxHelper.SetupWatermarkComboBox(comboBoxUnit, Resources.UnitSelectWatermark);
            LoadCategories();
            LoadUnits();
        }

        /// <summary>
        /// Загружает список категорий из базы данных в выпадающий список
        /// </summary>
        private void LoadCategories()
        {
            try
            {
                var categories = db.Categories
                    .OrderBy(c => c.Name)
                    .Select(c => new ComboItemDto
                    {
                        Id = c.Id,
                        Text = c.Name,
                    })
                    .ToList();

                comboBoxCategory.DataSource = categories;
                comboBoxCategory.DisplayMember = "Text";
                comboBoxCategory.ValueMember = "Id";

                if (comboBoxCategory.Items.Count > 0)
                {
                    comboBoxCategory.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                Program.LogError("Ошибка при загрузке категорий в AddProduct", ex);
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
                var units = db.Units
                    .OrderBy(u => u.Name)
                    .Select(u => new ComboItemDto
                    {
                        Id = u.Id,
                        Text = $"{u.Name} ({u.ShortName})"
                    })
                    .ToList();

                comboBoxUnit.DataSource = units;
                comboBoxUnit.DisplayMember = "Text";
                comboBoxUnit.ValueMember = "Id";

                if (comboBoxUnit.Items.Count > 0)
                {
                    comboBoxUnit.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                Program.LogError("Ошибка при загрузке единиц измерения", ex);
                MessageBox.Show(Resources.ErrorLoadUnits,
                    Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Добавить"
        /// Сохранение нового товара в базу данных
        /// </summary>
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateFields()) return;

            if (!Validation.ValidatePrice(textBoxPrice.Text, out var price))
            {
                MessageBox.Show(Resources.ErrorEnterCorrectPrice, Resources.TitleWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBoxCategory.SelectedItem == null)
            {
                MessageBox.Show(Resources.ErrorSelectCategory, Resources.TitleWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBoxUnit.SelectedItem == null)
            {
                MessageBox.Show(Resources.UnitSelectWatermark, Resources.TitleWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var selectedCategory = (ComboItemDto)comboBoxCategory.SelectedItem;
                var selectedUnit = (ComboItemDto)comboBoxUnit.SelectedItem;

                // Используем новый DTO
                var createDto = new CreateProductDto
                {
                    Article = textBoxArt.Text.Trim(),
                    Name = textBoxName.Text.Trim(),
                    CategoryId = selectedCategory.Id,
                    UnitId = selectedUnit.Id,
                    Price = price,
                    HasExpiryDate = radioButtonHasExpiry.Checked
                };

                var product = ProductMapper.ToEntity(createDto);

                db.Products.Add(product);
                db.SaveChanges();

                Program.LogInfo($"Товар '{product.Article} - {product.Name}' успешно добавлен");
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
        /// Обработчик изменения выбора срока годности
        /// </summary>
        private void RadioButtonExpiry_CheckedChanged(object sender, EventArgs e)
        {
        }


        /// <summary>
        /// Выполняет валидацию обязательных полей формы
        /// </summary>
        private bool ValidateFields()
        {
            if (Validation.IsWatermark(textBoxArt.Text, Resources.ProductArticleWatermark) ||
                Validation.IsWatermark(textBoxName.Text, Resources.ProductNameWatermark) ||
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
            var hasInput = !((textBoxArt.Text == Resources.ProductArticleWatermark || string.IsNullOrWhiteSpace(textBoxArt.Text)) &&
                   (textBoxName.Text == Resources.ProductNameWatermark || string.IsNullOrWhiteSpace(textBoxName.Text)) &&
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
        /// Освобождает ресурсы контекста базы данных при закрытии формы
        /// </summary>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            DbContextManager.ReleaseReference();
        }
    }
}