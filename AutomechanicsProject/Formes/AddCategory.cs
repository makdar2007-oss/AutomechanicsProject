using AutomechanicsProject.Classes;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Properties;
using System;
using System.Linq;
using System.Windows.Forms;

namespace AutomechanicsProject.Formes
{
    /// <summary>
    /// Форма для добавления новой категории товаров
    /// </summary>
    public partial class AddCategory : Form
    {
        private readonly DateBase db;

        /// <summary>
        /// Инициализирует новый экземпляр формы добавления категории
        /// </summary>
        public AddCategory()
        {
            InitializeComponent();
            db = new DateBase();
            TextBoxHelper.SetupWatermarkTextBox(textBoxAddCategory, Resources.CategoryAddWatermark);
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Добавить"
        /// Сохранение новой категории в базу данных
        /// </summary>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxAddCategory.Text) ||
                textBoxAddCategory.Text == Resources.CategoryAddWatermark)
            {
                MessageBox.Show(Resources.ErrorFillCategory, Resources.TitleWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var categoryName = textBoxAddCategory.Text.Trim();
                var categoryExists = db.Categories.Any(c => c.Name.ToLower() == categoryName.ToLower());

                if (categoryExists)
                {
                    MessageBox.Show(Resources.ErrorCategoryExists, Resources.TitleWarning,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var newCategory = new Category
                {
                    Id = Guid.NewGuid(),
                    Name = categoryName
                };
                db.Categories.Add(newCategory);
                db.SaveChanges();
                Program.LogInfo($"Категория '{categoryName}' успешно добавлена");
                MessageBox.Show(string.Format(Resources.SuccessCategoryAdded, categoryName), Resources.TitleSuccess,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                Program.LogError($"Ошибка при добавлении категории '{textBoxAddCategory.Text}'", ex);
                MessageBox.Show("Не удалось добавить категорию. Попробуйте позже.",
                    Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Закрывает форму без сохранения
        /// </summary>
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}