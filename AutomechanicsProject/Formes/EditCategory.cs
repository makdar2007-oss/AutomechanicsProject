using AutomechanicsProject.Classes;
using AutomechanicsProject.Dtos.UI;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Properties;
using NLog;
using System;
using System.Linq;
using System.Windows.Forms;
using AutomechanicsProject.Services.Interfaces;

namespace AutomechanicsProject.Formes


{
    /// <summary>
    /// Форма для редактирования категории товаров
    /// </summary>
    public partial class EditCategory : Form
    {
        private readonly ICategoryService _categoryService;
        private Guid? selectedCategoryId;
        private string selectedCategoryName;
        private bool selectedCategoryIsScrapMetal;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Применяет текст из ресурсов к элементам формы
        /// </summary>
        private void ApplyLocalization()
        {
            Text = Resources.EditCategory_Form_Title;

            labelTitle.Text = Resources.EditCategory_LabelTitle_Text;
            buttonEdit.Text = Resources.EditCategory_ButtonEdit_Text;
            buttonCancel.Text = Resources.EditCategory_ButtonCancel_Text;

            groupBoxScrapMetal.Text = Resources.Category_GroupBoxScrapMetal_Text;
            radioButtonScrapYes.Text = Resources.Category_RadioButtonScrapYes_Text;
            radioButtonScrapNo.Text = Resources.Category_RadioButtonScrapNo_Text;
        }

        /// <summary>
        /// Инициализирует новый экземпляр формы редактирования категории
        /// </summary>
        public EditCategory(ICategoryService categoryService)
        {
            InitializeComponent();
            ApplyLocalization();


            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));

            TextBoxHelper.SetupWatermarkTextBox(textBoxNewName, Resources.EditCategoryWatermark);
            groupBoxScrapMetal.Enabled = false;
            radioButtonScrapNo.Checked = true;
        }

        /// <summary>
        /// Обработчик загрузки формы
        /// </summary>
        private void EditCategory_Load(object sender, EventArgs e)
        {
            LoadCategories();
        }

        /// <summary>
        /// Загружает список категорий для выбора
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
                textBoxNewName.Text = Resources.EditCategoryWatermark;
                textBoxNewName.ForeColor = System.Drawing.Color.Gray;
                textBoxNewName.Enabled = false;
                buttonEdit.Enabled = false;

                selectedCategoryId = null;
                selectedCategoryName = null;
                selectedCategoryIsScrapMetal = false;

                groupBoxScrapMetal.Enabled = false;
                radioButtonScrapNo.Checked = true;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Ошибка при загрузке категорий в форму редактирования категории");

                MessageBox.Show(Resources.ErrorLoadCategories,
                    Resources.TitleError,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Обработчик изменения выбранной категории
        /// </summary>
        private void ComboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCategory.SelectedItem != null)
            {
                var selectedItem = (ComboItemDto)comboBoxCategory.SelectedItem;

                selectedCategoryId = selectedItem.Id;
                selectedCategoryName = _categoryService.GetCategoryNameById(selectedItem.Id);
                selectedCategoryIsScrapMetal = _categoryService.GetCategoryIsScrapMetalById(selectedItem.Id);

                textBoxNewName.Text = selectedCategoryName;
                textBoxNewName.ForeColor = System.Drawing.Color.Black;
                textBoxNewName.Enabled = true;

                groupBoxScrapMetal.Enabled = true;
                radioButtonScrapYes.Checked = selectedCategoryIsScrapMetal;
                radioButtonScrapNo.Checked = !selectedCategoryIsScrapMetal;

                buttonEdit.Enabled = true;
            }
            else
            {
                textBoxNewName.Text = Resources.EditCategoryWatermark;
                textBoxNewName.ForeColor = System.Drawing.Color.Gray;
                textBoxNewName.Enabled = false;
                buttonEdit.Enabled = false;

                selectedCategoryId = null;
                selectedCategoryName = null;
                selectedCategoryIsScrapMetal = false;

                groupBoxScrapMetal.Enabled = false;
                radioButtonScrapNo.Checked = true;
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Редактировать"
        /// </summary>
        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            if (!selectedCategoryId.HasValue)
            {
                MessageBox.Show(Resources.SelectCategoryForEdit, Resources.TitleWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newName = textBoxNewName.Text.Trim();

            if (string.IsNullOrWhiteSpace(newName) || newName == Resources.EditCategoryWatermark)
            {
                MessageBox.Show(Resources.ErrorFillCategory, Resources.TitleWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var isScrapMetal = radioButtonScrapYes.Checked;

            if (newName == selectedCategoryName && isScrapMetal == selectedCategoryIsScrapMetal)
            {
                MessageBox.Show(Resources.InfoCategoryNameNotChanged, Resources.TitleInformation,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var oldName = selectedCategoryName;

                _categoryService.EditCategory(selectedCategoryId.Value, newName, isScrapMetal);

                logger.Info($"Категория '{oldName}' переименована в '{newName}'");

                MessageBox.Show(string.Format(Resources.SuccessCategoryRenamed, oldName, newName),
                    Resources.TitleSuccess,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadCategories();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Ошибка при редактировании категории");
                MessageBox.Show(Resources.ErrorEditCategory, Resources.TitleError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Обработчик нажатия кнопки Отмена
        /// </summary>
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            var hasChanges = selectedCategoryId.HasValue &&
                 textBoxNewName.Text != Resources.EditCategoryWatermark &&
                 (textBoxNewName.Text != selectedCategoryName ||
                  radioButtonScrapYes.Checked != selectedCategoryIsScrapMetal);

            if (hasChanges)
            {
                var result = MessageBox.Show(
                    Resources.ConfirmCancelEdit,
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