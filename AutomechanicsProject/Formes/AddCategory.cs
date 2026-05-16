using AutomechanicsProject.Helpers;
using AutomechanicsProject.Properties;
using AutomechanicsProject.Services.Interfaces;
using NLog;
using System;
using System.Windows.Forms;

namespace AutomechanicsProject.Formes
{
    /// <summary>
    /// Форма для добавления категории
    /// </summary>
    public partial class AddCategory : Form
    {
        /// <summary>
        /// Сервис категорий
        /// </summary>
        private readonly ICategoryService _categoryService;

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Конструктор формы
        /// </summary>
        public AddCategory(ICategoryService categoryService)
        {
            InitializeComponent();

            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));

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

            if (_categoryService.CategoryExists(categoryName))
            {
                MessageBox.Show(Resources.ErrorCategoryExists, Resources.TitleWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AddNewCategory(categoryName);
        }

        /// <summary>
        /// Проверяет название категории
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
        /// Добавляет новую категорию
        /// </summary>
        private void AddNewCategory(string categoryName)
        {
            try
            {
                _categoryService.AddCategory(categoryName);

                logger.Info($"Категория '{categoryName}' успешно добавлена");

                MessageBox.Show(string.Format(Resources.SuccessCategoryAdded, categoryName),
                    Resources.TitleSuccess, MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                logger.Error($"Ошибка при добавлении категории '{categoryName}'", ex);

                MessageBox.Show(Resources.ErrorAddCategory, Resources.TitleError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик кнопки отмены
        /// </summary>
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}