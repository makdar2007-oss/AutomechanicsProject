using AutomechanicsProject.Classes;
using AutomechanicsProject.Dtos.UI;
using AutomechanicsProject.Properties;
using NLog;
using System;
using System.Linq;
using System.Windows.Forms;
using AutomechanicsProject.Services.Interfaces;

namespace AutomechanicsProject.Formes
{
    /// <summary>
    /// Форма для удаления категории товаров
    /// </summary>
    public partial class DeleteCategory : Form
    {
        private readonly ICategoryService _categoryService;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        //// <summary>
        /// Инициализирует новый экземпляр формы удаления категории
        /// </summary>
        public DeleteCategory(ICategoryService categoryService)
        {
            InitializeComponent();

            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        /// <summary>
        /// Обработчик загрузки формы
        /// </summary>
        private void DeleteCategory_Load(object sender, EventArgs e)
        {
            LoadCategories();
        }

        /// <summary>
        /// Загружает список категорий для удаления
        /// </summary>
       
        private void LoadCategories()
        {
            try
            {
                var categories = _categoryService.GetCategoriesWithProductCountForCombo();

                comboBoxCategory.DataSource = categories;
                comboBoxCategory.DisplayMember = "Text";
                comboBoxCategory.ValueMember = "Id";
                comboBoxCategory.SelectedIndex = -1;

                buttonDelete.Enabled = categories.Count > 0;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Ошибка при загрузке категорий в форму удаления категории");

                MessageBox.Show(Resources.ErrorLoadCategories,
                    Resources.TitleError,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        
        }

        /// <summary>
        /// Обработчик нажатия кнопки удаления категории
        /// </summary>
        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (comboBoxCategory.SelectedItem == null)
            {
                MessageBox.Show(Resources.ErrorSelectCategoryForDelete,
                    Resources.TitleWarning,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var selectedItem = (ComboItemDto)comboBoxCategory.SelectedItem;
                var categoryId = selectedItem.Id;
                var categoryName = _categoryService.GetCategoryNameById(categoryId);
                var productsCount = _categoryService.GetProductsCountByCategory(categoryId);

                var confirmMessage = productsCount > 0
                    ? string.Format(Resources.ConfirmDeleteCategoryWithProducts, categoryName, productsCount)
                    : string.Format(Resources.ConfirmDeleteCategory, categoryName);

                var result = MessageBox.Show(confirmMessage,
                    productsCount > 0 ? Resources.TitleDeleteConfirmation : Resources.TitleConfirmation,
                    MessageBoxButtons.YesNo,
                    productsCount > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                {
                    return;
                }

                _categoryService.DeleteCategory(categoryId);

                logger.Info($"Категория '{categoryName}' успешно удалена");

                var successMessage = productsCount > 0
                    ? string.Format(Resources.SuccessCategoryAndProductsDeleted, categoryName, productsCount)
                    : string.Format(Resources.SuccessCategoryDeleted, categoryName);

                MessageBox.Show(successMessage,
                    Resources.TitleSuccess,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Ошибка при удалении категории");

                MessageBox.Show(Resources.ErrorDeleteCategory,
                    Resources.TitleError,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Обработчик нажатия кнопки Отмена
        /// </summary>
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}