using AutomechanicsProject.Classes;
using AutomechanicsProject.Dtos.UI;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Properties;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Linq;
using System.Windows.Forms;

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
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Инициализирует новый экземпляр формы редактирования товара
        /// </summary>
        public RedactProduct(DateBase database, Guid id)
        {
            InitializeComponent();
            db = database ?? throw new ArgumentNullException(nameof(database));
            productId = id;

            TextBoxHelper.SetupWatermarkTextBox(textBoxArt, Resources.EditArticleWatermark);
            TextBoxHelper.SetupWatermarkTextBox(textBoxName, Resources.EditNameWatermark);
            TextBoxHelper.SetupWatermarkTextBox(textBoxCategory, Resources.EditCategoryWatermark);
            TextBoxHelper.SetupWatermarkTextBox(textBoxPrice, Resources.EditPriceWatermark);
            TextBoxHelper.SetupWatermarkComboBox(comboBoxUnit, Resources.UnitSelectWatermark);

            textBoxArt.TextChanged += (s, e) => hasChanges = true;
            textBoxName.TextChanged += (s, e) => hasChanges = true;
            textBoxCategory.TextChanged += (s, e) => hasChanges = true;
            comboBoxUnit.SelectedIndexChanged += (s, e) => hasChanges = true;
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
                logger.Error("Ошибка при загрузке единиц измерения", ex);
                MessageBox.Show(Resources.ErrorLoadUnits, Resources.TitleError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        var item = (ComboItemDto)comboBoxUnit.Items[i];
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
                logger.Error($"Ошибка при загрузке данных товара ID {productId}", ex);
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

                var selectedUnit = (ComboItemDto)comboBoxUnit.SelectedItem;
                currentProduct.UnitId = selectedUnit.Id;

                currentProduct.Price = price;

                db.SaveChanges();

                logger.Info($"Товар '{currentProduct.Article} - {currentProduct.Name}' обновлен");
                MessageBox.Show(Resources.SuccessProductUpdated, Resources.TitleSuccess,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                logger.Error($"Ошибка при обновлении товара ID {productId}", ex);
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

                if (result != DialogResult.Yes)
                {
                    return;
                }
            }
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}