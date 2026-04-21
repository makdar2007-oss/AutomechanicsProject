using AutomechanicsProject.Classes;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Properties;
using Microsoft.EntityFrameworkCore;
using NLog;
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
        private readonly DateBase _db;
        private readonly Guid? _productId;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Инициализирует форму удаления товара (поиск по артикулу)
        /// </summary>
        public DeleteProduct(DateBase database)
        {
            InitializeComponent();
            _db = database ?? throw new ArgumentNullException(nameof(database));
            TextBoxHelper.SetupWatermarkTextBox(textBoxArt, Resources.DeleteArticleWatermark);
        }

        /// <summary>
        /// Инициализирует форму удаления товара по ID
        /// </summary>
        public DeleteProduct(DateBase database, Guid id) : this(database)
        {
            _productId = id;
            LoadProductById();
        }

        /// <summary>
        /// Загружает товар по ID и отображает информацию
        /// </summary>
        private void LoadProductById()
        {
            try
            {
                if (!_productId.HasValue)
                {
                    return;
                }

                var product = _db.Products
                    .Include(p => p.Category)
                    .FirstOrDefault(p => p.Id == _productId.Value);

                if (product != null)
                {
                    textBoxArt.Text = product.Article;
                    textBoxArt.ForeColor = Color.Black;
                    textBoxArt.ReadOnly = true;
                    labelDeleteProduct.Text = $"Удаление товара: \n{product.Article} - {product.Name}";
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Ошибка при загрузке товара с ID {_productId}", ex);
                MessageBox.Show(Resources.ErrorLoadProduct, Resources.TitleError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Находит товар по артикулу
        /// </summary>
        private Product FindProductByArticle(string article)
        {
            if (string.IsNullOrWhiteSpace(article))
            {
                return null;
            }

            return _db.Products
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
                    MessageBox.Show(string.Format(Resources.ErrorProductInShipments, product.Article, shipmentsCount),
                        Resources.TitleWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ConfirmDelete(product))
                {
                    return;
                }

                _db.Products.Remove(product);
                _db.SaveChanges();

                logger.Info($"Товар '{product.Article} - {product.Name}' удален");

                MessageBox.Show(Resources.SuccessProductDeleted, Resources.TitleSuccess,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                logger.Error("ошибка при удалении товара", ex);
                MessageBox.Show(Resources.ErrorDeleteProduct, Resources.TitleError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Подтверждает удаление товара
        /// </summary>
        private bool ConfirmDelete(Product product)
        {
            var categoryName = product.Category?.Name ?? Resources.CategoryNone;

            var result = MessageBox.Show(
                string.Format(Resources.ConfirmDeleteProduct,
                    product.Article, product.Name, categoryName, product.Price, product.Balance, product.Unit),
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
            count = _db.ShipmentItems.Count(si => si.ProductId == product.Id);
            return count > 0;
        }

        /// <summary>
        /// Проверка валидности артикула
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
                           !_productId.HasValue;
            if (hasInput)
            {
                var result = MessageBox.Show(
                    Resources.ConfirmCancelDelete,
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

        /// <summary>
        /// Метод получения товара
        /// </summary>
        private Product GetProduct()
        {
            if (_productId.HasValue)
            {
                return _db.Products
                    .Include(p => p.Category)
                    .FirstOrDefault(p => p.Id == _productId.Value);
            }

            return FindProductByArticle(textBoxArt.Text);
        }
    }
}