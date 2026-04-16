using AutomechanicsProject.Classes;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Properties;
using System;
using System.Linq;
using System.Windows.Forms;
using AutomechanicsProject.Dtos;
using AutomechanicsProject.Dtos.Service;
using AutomechanicsProject.Mappers;

namespace AutomechanicsProject.Formes
{
    /// <summary>
    /// Форма для добавления категории
    /// </summary>
    public partial class AddCategory : Form
    {
        private readonly DateBase db;

        public AddCategory()
        {
            InitializeComponent();
            db = DbContextManager.GetContext();
            DbContextManager.AddReference();
            TextBoxHelper.SetupWatermarkTextBox(textBoxAddCategory, Resources.CategoryAddWatermark);
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Добавить"
        /// </summary>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (Validation.IsWatermark(textBoxAddCategory.Text, Resources.CategoryAddWatermark))
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