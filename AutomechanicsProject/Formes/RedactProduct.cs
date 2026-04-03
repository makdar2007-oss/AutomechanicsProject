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

            // Настройка водяных знаков для текстовых полей
            TextBoxHelper.SetupWatermarkTextBox(textBoxArt, Resources.EditArticleWatermark);
            TextBoxHelper.SetupWatermarkTextBox(textBoxName, Resources.EditNameWatermark);
            TextBoxHelper.SetupWatermarkTextBox(textBoxCategory, Resources.EditCategoryWatermark);
            TextBoxHelper.SetupWatermarkTextBox(textBoxPrice, Resources.EditPriceWatermark);

            // Настройка водяного знака для выпадающего списка единиц измерения
            TextBoxHelper.SetupWatermarkComboBox(comboBoxUnit, Resources.UnitSelectWatermark);

            // Подписка на события изменения текста
            textBoxArt.TextChanged += (s, e) => hasChanges = true;
            textBoxName.TextChanged += (s, e) => hasChanges = true;
            textBoxCategory.TextChanged += (s, e) => hasChanges = true;
            comboBoxUnit.SelectedIndexChanged += (s, e) => hasChanges = true;  // Изменено
            textBoxPrice.TextChanged += (s, e) => hasChanges = true;

            // Загрузка данных
            LoadUnits();  // Сначала загружаем единицы измерения
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
                    .Select(u => new
                    {
                        u.Id,
                        DisplayName = $"{u.Name} ({u.ShortName})",
                        u.ShortName
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
                MessageBox.Show("Не удалось загрузить список единиц измерения",
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
                    .Include(p => p.Unit)  // Добавляем загрузку единицы измерения
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

                // Устанавливаем выбранную единицу измерения в комбобоксе
                if (currentProduct.Unit != null && comboBoxUnit.Items.Count > 0)
                {
                    // Ищем и выбираем нужную единицу измерения по Id
                    for (int i = 0; i < comboBoxUnit.Items.Count; i++)
                    {
                        var item = comboBoxUnit.Items[i];
                        var itemId = (Guid)((dynamic)item).Id;
                        if (itemId == currentProduct.UnitId)
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
            // Проверка заполнения обязательных полей
            if (string.IsNullOrWhiteSpace(textBoxArt.Text) || textBoxArt.Text == Resources.EditArticleWatermark ||
                string.IsNullOrWhiteSpace(textBoxName.Text) || textBoxName.Text == Resources.EditNameWatermark ||
                string.IsNullOrWhiteSpace(textBoxCategory.Text) || textBoxCategory.Text == Resources.EditCategoryWatermark ||
                string.IsNullOrWhiteSpace(textBoxPrice.Text) || textBoxPrice.Text == Resources.EditPriceWatermark)
            {
                MessageBox.Show(Resources.ErrorFillFields, Resources.TitleWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверка выбора единицы измерения
            if (comboBoxUnit.SelectedItem == null)
            {
                MessageBox.Show("Выберите единицу измерения!", Resources.TitleWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверка корректности цены
            if (!decimal.TryParse(textBoxPrice.Text, out var price) || price < 0)
            {
                MessageBox.Show(Resources.ErrorEnterCorrectPrice, Resources.TitleWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Обновляем основные поля
                currentProduct.Article = textBoxArt.Text.Trim();
                currentProduct.Name = textBoxName.Text.Trim();

                // Обработка категории
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

                // Обновляем единицу измерения (теперь это Guid)
                var unitId = (Guid)((dynamic)comboBoxUnit.SelectedItem).Id;
                currentProduct.UnitId = unitId;

                // Обновляем цену
                currentProduct.Price = price;

                // Сохраняем изменения
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
    }
}