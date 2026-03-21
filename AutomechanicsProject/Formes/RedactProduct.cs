using AutomechanicsProject.Classes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AutomechanicsProject.Formes
{
    public partial class RedactProduct : Form
    {
        private DateBase db;
        private Guid productId;
        private Product currentProduct;
        private bool hasChanges = false;
        public RedactProduct(Guid id)
        {
            InitializeComponent();
            db = new DateBase();
            productId = id;
            textBoxArt.TextChanged += (s, e) => hasChanges = true;
            textBoxName.TextChanged += (s, e) => hasChanges = true;
            textBoxCategory.TextChanged += (s, e) => hasChanges = true;
            textBoxUnit.TextChanged += (s, e) => hasChanges = true;
            textBoxPrice.TextChanged += (s, e) => hasChanges = true;
            LoadProductData();
        }
        private void LoadProductData()
        {
            try
            {
                currentProduct = db.Products
                    .Include(p => p.Category)
                    .FirstOrDefault(p => p.Id == productId);

                if (currentProduct == null)
                {
                    MessageBox.Show("Товар не найден!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    return;
                }
                textBoxArt.Text = currentProduct.Article;
                textBoxArt.ForeColor = Color.Black;

                textBoxName.Text = currentProduct.Name;
                textBoxName.ForeColor = Color.Black;

                textBoxCategory.Text = currentProduct.Category?.Name ?? "Без категории";
                textBoxCategory.ForeColor = Color.Black;

                textBoxUnit.Text = currentProduct.Unit;
                textBoxUnit.ForeColor = Color.Black;

                textBoxPrice.Text = currentProduct.Price.ToString("F2");
                textBoxPrice.ForeColor = Color.Black;
                hasChanges = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonRedact_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxArt.Text) || textBoxArt.Text == "Артикул" ||
                string.IsNullOrWhiteSpace(textBoxName.Text) || textBoxName.Text == "Название" ||
                string.IsNullOrWhiteSpace(textBoxCategory.Text) || textBoxCategory.Text == "Категория" ||
                string.IsNullOrWhiteSpace(textBoxPrice.Text) || textBoxPrice.Text == "Цена")
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
                currentProduct.Article = textBoxArt.Text;
                currentProduct.Name = textBoxName.Text;

                string categoryName = textBoxCategory.Text;
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
                currentProduct.Unit = string.IsNullOrWhiteSpace(textBoxUnit.Text) || textBoxUnit.Text == "Единица измерения"
                    ? "шт"
                    : textBoxUnit.Text;
                currentProduct.Price = price;

                db.SaveChanges();

                MessageBox.Show("Товар успешно обновлен!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении товара: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            if (hasChanges)
            {
                var result = MessageBox.Show(
                    "У вас есть несохраненные изменения.\n\n" +
                    "Вы действительно хотите отменить редактирование?",
                    "Подтверждение отмены",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
        private void textBoxArt_Leave(object sender, EventArgs e)
        {

            var tb = sender as TextBox;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "Артикул";
                tb.ForeColor = Color.Gray;
            }
        }

        private void textBoxName_Leave(object sender, EventArgs e)
        {

            var tb = sender as TextBox;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "Название";
                tb.ForeColor = Color.Gray;
            }
        }
        private void textBoxName_Enter(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb.Text == "Название")
            {
                tb.Text = string.Empty;
                tb.ForeColor = Color.Black;
            }
        }
        private void textBoxArt_Enter(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb.Text == "Артикул")
            {
                tb.Text = string.Empty;
                tb.ForeColor = Color.Black;
            }
        }

        private void textBoxCategory_Enter(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb.Text == "Категория")
            {
                tb.Text = string.Empty;
                tb.ForeColor = Color.Black;
            }
        }
       
        private void textBoxCategory_Leave(object sender, EventArgs e)
        {

            var tb = sender as TextBox;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "Категория";
                tb.ForeColor = Color.Gray;
            }
        }

        private void textBoxUnit_Enter(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb.Text == "Единица измерения")
            {
                tb.Text = string.Empty;
                tb.ForeColor = Color.Black;
            }
        }

        private void textBoxUnit_Leave(object sender, EventArgs e)
        {

            var tb = sender as TextBox;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "Единица измерения";
                tb.ForeColor = Color.Gray;
            }
        }
        private void textBoxPrice_Leave(object sender, EventArgs e)
        {

            var tb = sender as TextBox;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "Цена";
                tb.ForeColor = Color.Gray;
            }
        }
        private void textBoxPrice_Enter(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb.Text == "Цена")
            {
                tb.Text = string.Empty;
                tb.ForeColor = Color.Black;
            }
        }
    }
}