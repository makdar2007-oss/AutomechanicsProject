using AutomechanicsProject.Classes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AutomechanicsProject.Formes
{
    public partial class DeleteProduct : Form
    {
        private DateBase db;
        private Guid? productId;
        public DeleteProduct()
        {
            db = new DateBase();
            InitializeComponent();
        }
        public DeleteProduct(Guid id) : this()
        {
            productId = id;
            LoadProductById();
        }
        private void LoadProductById()
        {
            try
            {
                if (productId.HasValue)
                {
                    var product = db.Products
                        .Include(p => p.Category)
                        .FirstOrDefault(p => p.Id == productId.Value);

                    if (product != null)
                    {
                        textBoxArt.Text = product.Article;
                        textBoxArt.ForeColor = Color.Black;
                        textBoxArt.ReadOnly = true;
                        labelDeleteProduct.Text = $"Удаление товара:\n{product.Article} - {product.Name}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке товара: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private Product FindProductByArticle(string article)
        {
            if (string.IsNullOrWhiteSpace(article))
                return null; 

            return db.Products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Article.ToLower() == article.ToLower().Trim());
        }
        private void textBoxArt_Leave(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "Введите артикул товара";
                tb.ForeColor = Color.Gray;
            }
        }
        private void textBoxArt_Enter(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb.Text == "Введите артикул товара")
            {
                tb.Text = string.Empty;
                tb.ForeColor = Color.Black;
            }
        }
        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBoxArt.Text) ||
                    textBoxArt.Text == "Введите артикул товара")
                {
                    MessageBox.Show("Введите артикул товара!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Product productToDelete = null;
                if (productId.HasValue)
                {
                    productToDelete = db.Products
                        .Include(p => p.Category)
                        .FirstOrDefault(p => p.Id == productId.Value);
                }
                else
                {
                    productToDelete = FindProductByArticle(textBoxArt.Text);
                }
                if (productToDelete == null)
                {
                    MessageBox.Show($"Товар с артикулом \"{textBoxArt.Text}\" не найден!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                bool hasShipments = db.ShipmentItems.Any(si => si.ProductId == productToDelete.Id);
                if (hasShipments)
                {
                    int shipmentsCount = db.ShipmentItems
                        .Count(si => si.ProductId == productToDelete.Id);

                    MessageBox.Show(
                        $"Невозможно удалить товар \"{productToDelete.Article} - {productToDelete.Name}\",\n\n" +
                        $"так как он используется в {shipmentsCount} отгрузках.\n\n" +
                        $"Сначала удалите связанные отгрузки.",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
                string categoryName = productToDelete.Category?.Name ?? "Без категории";
                var confirmResult = MessageBox.Show(
                    $"Вы действительно хотите удалить товар?\n\n" +
                    $"Артикул: {productToDelete.Article}\n" +
                    $"Название: {productToDelete.Name}\n" +
                    $"Категория: {categoryName}\n" +
                    $"Цена: {productToDelete.Price:C}\n" +
                    $"Остаток: {productToDelete.Balance} {productToDelete.Unit}\n\n" +
                    $"Это действие нельзя отменить!",
                    "Подтверждение удаления товара",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                if (confirmResult == DialogResult.Yes)
                {
                    db.Products.Remove(productToDelete);
                    db.SaveChanges();
                    MessageBox.Show(
                        $"Товар успешно удален!\n\n" +
                        $"Артикул: {productToDelete.Article}\n" +
                        $"Название: {productToDelete.Name}",
                        "Успех",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении товара: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxArt.Text) &&
                textBoxArt.Text != "Введите артикул товара" &&
                !productId.HasValue)
            {
                var result = MessageBox.Show(
                    "Вы уверены, что хотите отменить удаление?",
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

    }
}