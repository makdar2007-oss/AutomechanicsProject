using AutomechanicsProject.Classes;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Properties;
using System;
using System.Linq;
using System.Windows.Forms;
using NLog;

namespace AutomechanicsProject.Formes
{
    /// <summary>
    /// Форма для добавления категории
    /// </summary>
    public partial class AddCategory : Form
    {
        private readonly DateBase _db;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public AddCategory(DateBase database)
        {
            InitializeComponent();
            _db = database ?? throw new ArgumentNullException(nameof(database));
            SetupWatermark();
        }

        /// <summary>
        /// Установка водяного знака для текстового поля
        /// </summary>
        private void SetupWatermark()
        {
            TextBoxHelper.SetupWatermarkTextBox(textBoxAddCategory, Resources.CategoryAddWatermark);
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Добавить"
        /// </summary>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var categoryName = GetValidatedCategoryName();
            if (categoryName == null)
            {
                return;
            }

            if (CategoryExists(categoryName))
            {
                MessageBox.Show(Resources.ErrorCategoryExists, Resources.TitleWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AddNewCategory(categoryName);
        }

        /// <summary>
        /// Проверяет название категории из текстового поля
        /// </summary>
        private string GetValidatedCategoryName()
        {
            if (Validation.IsWatermark(textBoxAddCategory.Text, Resources.CategoryAddWatermark))
            {
                MessageBox.Show(Resources.ErrorFillCategory, Resources.TitleWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            return textBoxAddCategory.Text.Trim();
        }

        /// <summary>
        /// Проверка на наличие категории с таким же названием
        /// </summary>
        private bool CategoryExists(string categoryName)
        {
            return _db.Categories.Any(c => c.Name.ToLower() == categoryName.ToLower());
        }

        /// <summary>
        /// Создает и сохраняет новую категорию
        /// </summary>
        private void AddNewCategory(string categoryName)
        {
            try
            {
                var newCategory = new Category
                {
                    Id = Guid.NewGuid(),
                    Name = categoryName
                };

                _db.Categories.Add(newCategory);
                _db.SaveChanges();

                logger.Info($"Категория '{categoryName}' успешно добавлена");
                MessageBox.Show(string.Format(Resources.SuccessCategoryAdded, categoryName),
                    Resources.TitleSuccess, MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                logger.Error($"Ошибка при добавлении категори '{categoryName}'", ex);
                MessageBox.Show(Resources.ErrorAddCategory, Resources.TitleError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Отмена"
        /// </summary>
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}