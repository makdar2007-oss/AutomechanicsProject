using AutomechanicsProject.Classes;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
namespace AutomechanicsProject.Formes
{
    public partial class AddProduct : Form
    {
        private DateBase db;
        public AddProduct()
        {
            InitializeComponent();
            db = new DateBase();
            LoadCategories();
        }
        private void LoadCategories()
        {
            try
            {
                var categories = db.Categories
                    .OrderBy(c => c.Name)
                    .Select(c => new { c.Id, c.Name })
                    .ToList();

                comboBoxCategory.DataSource = categories;
                comboBoxCategory.DisplayMember = "Name";
                comboBoxCategory.ValueMember = "Id";
                if (comboBoxCategory.Items.Count > 0)
                {
                    comboBoxCategory.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке категорий: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (comboBoxCategory.SelectedItem == null)
            {
                MessageBox.Show("Выберите категорию!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (textBoxArt.Text == "Введите артикул" || string.IsNullOrWhiteSpace(textBoxArt.Text) ||
                textBoxName.Text == "Введите название" || string.IsNullOrWhiteSpace(textBoxName.Text) ||
                textBoxPrice.Text == "Введите цену" || string.IsNullOrWhiteSpace(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните все обязательные поля!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(textBoxPrice.Text, out decimal price) || price < 0)
            {
                MessageBox.Show("Введите корректную цену!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                var selectedItem = comboBoxCategory.SelectedItem;
                var categoryIdProperty = selectedItem.GetType().GetProperty("Id");
                if (categoryIdProperty == null)
                {
                    MessageBox.Show("Ошибка получения данных категории!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Guid categoryId = (Guid)categoryIdProperty.GetValue(selectedItem);
                var categoryNameProperty = selectedItem.GetType().GetProperty("Name");
                string categoryName = categoryNameProperty?.GetValue(selectedItem)?.ToString() ?? "Неизвестно";
                string unit = textBoxUnit.Text.Trim();
                if (string.IsNullOrWhiteSpace(unit) || unit == "Введите единицы измерения")
                {
                    unit = "шт";
                }

                var product = new Product
                {
                    Id = Guid.NewGuid(),
                    Article = textBoxArt.Text.Trim(),
                    Name = textBoxName.Text.Trim(),
                    CategoryId = categoryId,
                    Unit = unit,
                    Price = price,
                    Balance = 0
                };
                db.Products.Add(product);
                db.SaveChanges();
                MessageBox.Show("Товар успешно добавлен!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении товара: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void textBoxArt_Leave(object sender, System.EventArgs e)
        {
            var tb = sender as TextBox;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "Введите артикул";
                tb.ForeColor = Color.Gray;
            }
        }
        private void textBoxArt_Enter(object sender, System.EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb.Text == "Введите артикул")
            {
                tb.Text = string.Empty;
                tb.ForeColor = Color.Black;
            }
        }
        private void textBoxName_Leave(object sender, System.EventArgs e)
        {
            var tb = sender as TextBox;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "Введите название";
                tb.ForeColor = Color.Gray;
            }
        }
        private void textBoxName_Enter(object sender, System.EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb.Text == "Введите название")
            {
                tb.Text = string.Empty;
                tb.ForeColor = Color.Black;
            }
        }
        private void comboBoxCategory_Leave(object sender, System.EventArgs e)
        {
            var tb = sender as TextBox;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "Выберите категорию";
                tb.ForeColor = Color.Gray;
            }
        }
        private void comboBoxCategory_Enter(object sender, System.EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb.Text == "Выберите категорию")
            {
                tb.Text = string.Empty;
                tb.ForeColor = Color.Black;
            }
        }
        private void textBoxUnit_Leave(object sender, System.EventArgs e)
        {
            var tb = sender as TextBox;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "Введите  единицы измерения";
                tb.ForeColor = Color.Gray;
            }
        }
        private void textBoxUnit_Enter(object sender, System.EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb.Text == "Введите единицы измерения")
            {
                tb.Text = string.Empty;
                tb.ForeColor = Color.Black;
            }
        }
        private void textBoxPrice_Leave(object sender, System.EventArgs e)
        {
            var tb = sender as TextBox;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "Введите цену";
                tb.ForeColor = Color.Gray;
            }
        }
        private void textBoxPrice_Enter(object sender, System.EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb.Text == "Введите цену")
            {
                tb.Text = string.Empty;
                tb.ForeColor = Color.Black;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (!((textBoxArt.Text == "Введите артикул" || string.IsNullOrWhiteSpace(textBoxArt.Text)) &&
                   (textBoxName.Text == "Введите название" || string.IsNullOrWhiteSpace(textBoxName.Text)) &&
                   (comboBoxCategory.Text == "Введите категорию" || string.IsNullOrWhiteSpace(comboBoxCategory.Text)) &&
                   (textBoxUnit.Text == "Введите единицы измерения" || string.IsNullOrWhiteSpace(textBoxUnit.Text)) &&
                   (textBoxPrice.Text == "Введите цену" || string.IsNullOrWhiteSpace(textBoxPrice.Text))))
            {
                var result = MessageBox.Show("Вы уверены, что хотите отменить добавление товара?\nВсе введенные данные будут потеряны.",
                    "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;

            }
        }
    }
}