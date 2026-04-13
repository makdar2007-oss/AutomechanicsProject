using AutomechanicsProject.Classes;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Properties;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AutomechanicsProject.Classes.Dtos;

namespace AutomechanicsProject.Formes
{
    /// <summary>
    /// Форма для редактирования товара
    /// </summary>
    public partial class RedactProduct : Form
    {
        private readonly DateBase db;
        private readonly Guid productId;
        private Product currentProduct;
        private bool hasChanges;

        /// <summary>
        /// Инициализирует новый экземпляр формы редактирования товара
        /// </summary>
        public RedactProduct(Guid id)
        {
            InitializeComponent();
            db = DbContextManager.GetContext();
            DbContextManager.AddReference();
            productId = id;

            TextBoxHelper.SetupWatermarkTextBox(textBoxArt, Resources.EditArticleWatermark);
            TextBoxHelper.SetupWatermarkTextBox(textBoxName, Resources.EditNameWatermark);
            TextBoxHelper.SetupWatermarkTextBox(textBoxCategory, Resources.EditCategoryWatermark);
            TextBoxHelper.SetupWatermarkTextBox(textBoxPrice, Resources.EditPriceWatermark);

            TextBoxHelper.SetupWatermarkComboBox(comboBoxUnit, Resources.UnitSelectWatermark);

            textBoxArt.TextChanged += (s, e) => hasChanges = true;
            textBoxName.TextChanged += (s, e) => hasChanges = true;
            textBoxCategory.TextChanged += (s, e) => hasChanges = true;
            comboBoxUnit.SelectedIndexChanged += (s, e) => hasChanges = true;  // Изменено
            textBoxPrice.TextChanged += (s, e) => hasChanges = true;

            LoadUnits();  
            LoadProductData();
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
                    .Select(u => new UnitComboBoxDto 
                    {
                        Id = u.Id,
                        DisplayName = $"{u.Name} ({u.ShortName})",
                        ShortName = u.ShortName,
                        Name = u.Name
                    })
                    .ToList();

                comboBoxUnit.DataSource = units;
                comboBoxUnit.DisplayMember = "DisplayName";
                comboBoxUnit.ValueMember = "Id";

                if (comboBoxUnit.Items.Count > 0)
                {
                    comboBoxUnit.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                Program.LogError("Ошибка при загрузке единиц измерения в RedactProduct", ex);
                MessageBox.Show(Resources.ErrorLoadUnits,
    Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Загружает данные товара из базы данных и отображает их в полях формы
        /// </summary>
        private void LoadProductData()
        {
            try
            {
                currentProduct = db.Products
                    .Include(p => p.Category)
                    .Include(p => p.Unit)  
                    .FirstOrDefault(p => p.Id == productId);

                if (currentProduct == null)
                {
                    MessageBox.Show(Resources.ErrorProductNotFoundGeneric, Resources.TitleError,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult = DialogResult.Cancel;
                    Close();
                    return;
                }

                textBoxArt.Text = currentProduct.Article;
                textBoxName.Text = currentProduct.Name;
                textBoxCategory.Text = currentProduct.Category?.Name ?? Resources.CategoryNone;
                textBoxPrice.Text = currentProduct.Price.ToString("F2");

                if (currentProduct.Unit != null && comboBoxUnit.Items.Count > 0)
                {
                    for (int i = 0; i < comboBoxUnit.Items.Count; i++)
                    {
                        var item = (UnitComboBoxDto)comboBoxUnit.Items[i];
                        if (item.Id == currentProduct.UnitId)
                        {
                            comboBoxUnit.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.LogError($"Ошибка при загрузке данных товара ID {productId}", ex);
                MessageBox.Show(Resources.ErrorLoadProductData, Resources.TitleError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки Редактировать
        /// </summary>
        private void ButtonRedact_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxArt.Text) || textBoxArt.Text == Resources.EditArticleWatermark ||
                string.IsNullOrWhiteSpace(textBoxName.Text) || textBoxName.Text == Resources.EditNameWatermark ||
                string.IsNullOrWhiteSpace(textBoxCategory.Text) || textBoxCategory.Text == Resources.EditCategoryWatermark ||
                string.IsNullOrWhiteSpace(textBoxPrice.Text) || textBoxPrice.Text == Resources.EditPriceWatermark)
            {
                MessageBox.Show(Resources.ErrorFillFields, Resources.TitleWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBoxUnit.SelectedItem == null)
            {
                MessageBox.Show(Resources.ErrorSelectUnit, Resources.TitleWarning,
                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(textBoxPrice.Text, out var price) || price < 0)
            {
                MessageBox.Show(Resources.ErrorEnterCorrectPrice, Resources.TitleWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                currentProduct.Article = textBoxArt.Text.Trim();
                currentProduct.Name = textBoxName.Text.Trim();

                var categoryName = textBoxCategory.Text.Trim();
                var category = db.Categories.FirstOrDefault(c => c.Name == categoryName);
                if (category == null)
                {
                    category = new Category
                    {
                        Id = Guid.NewGuid(),
                        Name = categoryName
                    };
                    db.Categories.Add(category);
                }
                currentProduct.CategoryId = category.Id;

                var selectedUnit = (UnitComboBoxDto)comboBoxUnit.SelectedItem; 
                currentProduct.UnitId = selectedUnit.Id;

                currentProduct.Price = price;

                db.SaveChanges();

                Program.LogInfo($"Товар '{currentProduct.Article} - {currentProduct.Name}' обновлен");
                MessageBox.Show(Resources.SuccessProductUpdated, Resources.TitleSuccess,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                Program.LogError($"Ошибка при обновлении товара ID {productId}", ex);
                MessageBox.Show(Resources.ErrorUpdateProduct, Resources.TitleError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки Отмена
        /// Запрашивает подтверждение при наличии несохраненных изменений
        /// </summary>
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            if (hasChanges)
            {
                var result = MessageBox.Show(
                    Resources.ConfirmCancelEditWithDetails,
                    Resources.TitleConfirmation,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes) return;
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