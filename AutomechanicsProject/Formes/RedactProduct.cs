using AutomechanicsProject.Classes;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Properties;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
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
        /// <summary>
        /// Инициализирует новый экземпляр формы редактирования товара
        /// </summary>
        public RedactProduct(Guid id)
        {
            InitializeComponent();
            db = new DateBase();
            productId = id;
            TextBoxHelper.SetupWatermarkTextBox(textBoxArt, Resources.EditArticleWatermark);
            TextBoxHelper.SetupWatermarkTextBox(textBoxName, Resources.EditNameWatermark);
            TextBoxHelper.SetupWatermarkTextBox(textBoxCategory, Resources.EditCategoryWatermark);
            TextBoxHelper.SetupWatermarkTextBox(textBoxUnit, Resources.EditUnitWatermark);
            TextBoxHelper.SetupWatermarkTextBox(textBoxPrice, Resources.EditPriceWatermark);

            textBoxArt.TextChanged += (s, e) => hasChanges = true;
            textBoxName.TextChanged += (s, e) => hasChanges = true;
            textBoxCategory.TextChanged += (s, e) => hasChanges = true;
            textBoxUnit.TextChanged += (s, e) => hasChanges = true;
            textBoxPrice.TextChanged += (s, e) => hasChanges = true;

            LoadProductData();
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
                    .FirstOrDefault(p => p.Id == productId);

                if (currentProduct == null)
                {
                    MessageBox.Show("Товар не найден!", Resources.TitleError,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult = DialogResult.Cancel;
                    Close();
                    return;
                }
                textBoxArt.Text = currentProduct.Article;
                textBoxArt.ForeColor = Color.Black;

                textBoxName.Text = currentProduct.Name;
                textBoxName.ForeColor = Color.Black;

                textBoxCategory.Text = currentProduct.Category?.Name ?? "Без категории";
                textBoxCategory.ForeColor = Color.Black;

                textBoxUnit.Text = currentProduct.Unit;
                textBoxUnit.ForeColor = Color.Black;

                textBoxPrice.Text = currentProduct.Price.ToString("F2");
                textBoxPrice.ForeColor = Color.Black;

                hasChanges = false;
            }
            catch (Exception ex)
            {
                Program.LogError($"Ошибка при загрузке данных товара ID {productId}", ex);
                MessageBox.Show("Не удалось загрузить данные товара",
                    Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (!decimal.TryParse(textBoxPrice.Text, out var price) || price < 0)
            {
                MessageBox.Show(Resources.ErrorEnterCorrectPrice, Resources.TitleWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                currentProduct.Article = textBoxArt.Text;
                currentProduct.Name = textBoxName.Text;
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
                currentProduct.Unit = string.IsNullOrWhiteSpace(textBoxUnit.Text) || textBoxUnit.Text == Resources.EditUnitWatermark
                    ? "шт"
                    : textBoxUnit.Text;
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
                MessageBox.Show("Не удалось обновить товар. Попробуйте позже.",
                    Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    "У вас есть несохраненные изменения.\n\nВы действительно хотите отменить редактирование?",
                    Resources.TitleConfirmation,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes) return;
            }
            DialogResult = DialogResult.Cancel;
            Close();
        }
        /// <summary>
        /// Освобождает ресурсы при закрытии формы
        /// </summary>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            db?.Dispose();
            base.OnFormClosed(e);
        }
    }
}