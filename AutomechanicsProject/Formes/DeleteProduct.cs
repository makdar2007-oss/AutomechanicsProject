using AutomechanicsProject.Classes;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Properties;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AutomechanicsProject.Formes
{
    /// <summary>
    /// Форма для удаления товара
    /// </summary>
    public partial class DeleteProduct : Form
    {
        private readonly DateBase db;
        private readonly Guid? productId;
        /// <summary>
        /// Инициализирует форму удаления товара (поиск по артикулу)
        /// </summary>
        public DeleteProduct()
        {
            InitializeComponent();
            db = new DateBase();
            TextBoxHelper.SetupWatermarkTextBox(textBoxArt, Resources.DeleteArticleWatermark);
        }
        /// <summary>
        /// Инициализирует форму удаления товара по ID
        /// </summary>
        public DeleteProduct(Guid id) : this()
        {
            productId = id;
            LoadProductById();
        }
        /// <summary>
        /// Загружает товар по ID и отображает информацию
        /// </summary>
        private void LoadProductById()
        {
            try
            {
                if (!productId.HasValue) return;

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
            catch (Exception ex)
            {
                Program.LogError($"Ошибка при загрузке товара с ID {productId}", ex);
                MessageBox.Show("Не удалось загрузить товар",
                    Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Находит товар по артикулу
        /// </summary>
        private Product FindProductByArticle(string article)
        {
            if (string.IsNullOrWhiteSpace(article)) return null;

            return db.Products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Article == article.Trim());
        }
        /// <summary>
        /// Обработчик нажатия кнопки "Удалить"
        /// </summary>
        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsArticleValid())
                {
                    MessageBox.Show(Resources.ErrorEnterArticle, Resources.TitleWarning,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var product = GetProduct();

                if (product == null)
                {
                    MessageBox.Show(string.Format(Resources.ErrorProductNotFound, textBoxArt.Text),
                        Resources.TitleWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (HasShipments(product, out int shipmentsCount))
                {
                    MessageBox.Show(
                        $"Невозможно удалить товар \"{product.Article} - {product.Name}\",\n" +
                        $"так как он используется в {shipmentsCount} отгрузках.",
                        Resources.TitleWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ConfirmDelete(product)) return;

                db.Products.Remove(product);
                db.SaveChanges();

                Program.LogInfo($"Товар '{product.Article} - {product.Name}' удален");

                MessageBox.Show(Resources.SuccessProductDeleted, Resources.TitleSuccess,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                Program.LogError("Ошибка при удалении товара", ex);
                MessageBox.Show("Не удалось удалить товар. Попробуйте позже.",
                    Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool ConfirmDelete(Product product)
        {
            var categoryName = product.Category?.Name ?? "Без категории";

            var result = MessageBox.Show(
                $"Вы действительно хотите удалить товар?\n\n" +
                $"Артикул: {product.Article}\n" +
                $"Название: {product.Name}\n" +
                $"Категория: {categoryName}\n" +
                $"Цена: {product.Price:F2}\n" +
                $"Остаток: {product.Balance} {product.Unit}\n\n" +
                $"Это действие нельзя отменить!",
                Resources.TitleConfirmation,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            return result == DialogResult.Yes;
        }
        /// <summary>
        /// Проверка использования в отгрузках
        /// </summary>
        private bool HasShipments(Product product, out int count)
        {
            count = db.ShipmentItems.Count(si => si.ProductId == product.Id);
            return count > 0;
        }
        /// <summary>
        ///Проверка валидности артикула
        /// </summary>
        private bool IsArticleValid()
        {
            return !string.IsNullOrWhiteSpace(textBoxArt.Text) &&
                   textBoxArt.Text != Resources.DeleteArticleWatermark;
        }
        /// <summary>
        /// Обработчик нажатия кнопки Отмена
        /// </summary>
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            var hasInput = !string.IsNullOrWhiteSpace(textBoxArt.Text) &&
                           textBoxArt.Text != Resources.DeleteArticleWatermark &&
                           !productId.HasValue;
            if (hasInput)
            {
                var result = MessageBox.Show(
                    "Вы уверены, что хотите отменить удаление?",
                    Resources.TitleConfirmation,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes) return;
            }

            DialogResult = DialogResult.Cancel;
            Close();
        }
        /// <summary>
        /// Метод получения товара
        /// </summary>
        private Product GetProduct()
        {
            if (productId.HasValue)
            {
                return db.Products
                    .Include(p => p.Category)
                    .FirstOrDefault(p => p.Id == productId.Value);
            }

            return FindProductByArticle(textBoxArt.Text);
        }
        /// <summary>
        /// Освобождает ресурсы при закрытии формы
        /// </summary>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            db?.Dispose();
            base.OnFormClosed(e);
        }
    }
}