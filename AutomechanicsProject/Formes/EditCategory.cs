using AutomechanicsProject.Classes;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Properties;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;

namespace AutomechanicsProject.Formes
{
    /// <summary>
    /// Форма для редактирования категории товаров
    /// </summary>
    public partial class EditCategory : Form
    {
        private readonly DateBase db;
        private Category selectedCategory;

        /// <summary>
        /// Инициализирует новый экземпляр формы редактирования категории
        /// </summary>
        public EditCategory()
        {
            InitializeComponent();
            db = new DateBase();
            Load += EditCategory_Load;
        }

        /// <summary>
        /// Обработчик загрузки формы
        /// </summary>
        private void EditCategory_Load(object sender, EventArgs e)
        {
            LoadCategories();
            SetupWatermarks();
        }

        /// <summary>
        /// Настраивает водяные знаки для полей ввода
        /// </summary>
        private void SetupWatermarks()
        {
            TextBoxHelper.SetupWatermarkTextBox(textBoxNewName, Resources.EditCategoryWatermark);
        }

        /// <summary>
        /// Загружает список категорий для выбора
        /// </summary>
        private void LoadCategories()
        {
            try
            {
                var categories = db.Categories
                    .OrderBy(c => c.Name)
                    .Select(c => new
                    {
                        c.Id,
                        DisplayName = $"{c.Name} (товаров: {db.Products.Count(p => p.CategoryId == c.Id)})"
                    })
                    .ToList();

                comboBoxCategory.DisplayMember = "DisplayName";
                comboBoxCategory.ValueMember = "Id";
                comboBoxCategory.DataSource = categories;

                var hasCategories = comboBoxCategory.Items.Count > 0;
                comboBoxCategory.SelectedIndex = -1;
                buttonEdit.Enabled = false;
                textBoxNewName.Enabled = false;
                textBoxNewName.Text = Resources.EditCategoryWatermark;
                textBoxNewName.ForeColor = System.Drawing.Color.Gray;
                selectedCategory = null;
            }
            catch (Exception ex)
            {
                Program.LogError("Ошибка при загрузке категорий в EditCategory", ex);
                MessageBox.Show("Не удалось загрузить категории",
                    Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Обработчик изменения выбранной категории
        /// </summary>
        private void ComboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCategory.SelectedItem != null)
            {
                var selectedItem = (dynamic)comboBoxCategory.SelectedItem;
                var categoryId = (Guid)selectedItem.Id;

                selectedCategory = db.Categories
                    .FirstOrDefault(c => c.Id == categoryId);

                if (selectedCategory != null)
                {
                    textBoxNewName.Text = selectedCategory.Name;
                    textBoxNewName.ForeColor = System.Drawing.Color.Black;
                    textBoxNewName.Enabled = true;
                    buttonEdit.Enabled = true;
                }
            }
            else
            {
                textBoxNewName.Text = Resources.EditCategoryWatermark;
                textBoxNewName.ForeColor = System.Drawing.Color.Gray;
                textBoxNewName.Enabled = false;
                buttonEdit.Enabled = false;
                selectedCategory = null;
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Редактировать"
        /// </summary>
        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            if (selectedCategory == null)
            {
                MessageBox.Show("Выберите категорию для редактирования",
                    Resources.TitleWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newName = textBoxNewName.Text.Trim();

            if (string.IsNullOrWhiteSpace(newName) || newName == Resources.EditCategoryWatermark)
            {
                MessageBox.Show(Resources.ErrorFillCategory,
                    Resources.TitleWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newName == selectedCategory.Name)
            {
                MessageBox.Show("Название категории не изменилось",
                    Resources.TitleInformation, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var categoryExists = db.Categories
                    .Any(c => c.Name == newName && c.Id != selectedCategory.Id);

                if (categoryExists)
                {
                    MessageBox.Show(Resources.ErrorCategoryExists,
                        Resources.TitleWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var oldName = selectedCategory.Name;
                selectedCategory.Name = newName;
                db.SaveChanges();

                Program.LogInfo($"Категория '{oldName}' переименована в '{newName}'");
                MessageBox.Show($"Категория \"{oldName}\" успешно переименована в \"{newName}\"!",
                    Resources.TitleSuccess, MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCategories();
            }
            catch (Exception ex)
            {
                Program.LogError($"Ошибка при редактировании категории ID {selectedCategory?.Id}", ex);
                MessageBox.Show("Не удалось отредактировать категорию. Попробуйте позже.",
                    Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Обработчик нажатия кнопки Отмена
        /// </summary>
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            var hasChanges = selectedCategory != null &&
                             textBoxNewName.Text != selectedCategory.Name &&
                             textBoxNewName.Text != Resources.EditCategoryWatermark;

            if (hasChanges)
            {
                var result = MessageBox.Show(
                    "У вас есть несохраненные изменения.\nВы действительно хотите отменить редактирование?",
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