using AutomechanicsProject.Classes;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
namespace AutomechanicsProject.Formes
{
    public partial class AddCategory : Form
    {
        private DateBase db;
        public AddCategory()
        {
            InitializeComponent();
            db = new DateBase();
        }

        private void textBoxAddCategory_Enter(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb.Text == "Введите название")
            {
                tb.Text = string.Empty;
                tb.ForeColor = Color.Gray;
            }
        }
        private void textBoxAddCategory_Leave(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "Введите название";
                tb.ForeColor = Color.Gray;
            }
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxAddCategory.Text) ||
                textBoxAddCategory.Text == "Введите название")
            {
                MessageBox.Show("Введите название категории!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string categoryName = textBoxAddCategory.Text.Trim();

                bool categoryExists = db.Categories.Any(c => c.Name.ToLower() == categoryName.ToLower());

                if (categoryExists)
                {
                    MessageBox.Show("Категория с таким названием уже существует!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var newCategory = new Category
                {
                    Id = Guid.NewGuid(),
                    Name = categoryName
                };

                // Добавляем в базу данных
                db.Categories.Add(newCategory);
                db.SaveChanges();

                MessageBox.Show($"Категория \"{categoryName}\" успешно добавлена!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Закрываем форму с результатом OK
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении категории: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
