using AutomechanicsProject.Classes;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Properties;
using NLog;
using System;
using System.Drawing;
using System.Windows.Forms;
using AutomechanicsProject.Services.Interfaces;

namespace AutomechanicsProject.Formes
{
    /// <summary>
    /// Форма для удаления товара
    /// </summary>
    public partial class DeleteProduct : Form
    {
       
        private readonly IProductService _productService;
        private readonly Guid? _productId;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Инициализирует форму удаления товара по артикулу
        /// </summary>
        public DeleteProduct(IProductService productService)
        {
            InitializeComponent();

            _productService = productService ??
                throw new ArgumentNullException(nameof(productService));

            TextBoxHelper.SetupWatermarkTextBox(
                textBoxArt,
                Resources.DeleteArticleWatermark);
        }

        /// <summary>
        /// Инициализирует форму удаления товара по ID
        /// </summary>
        public DeleteProduct(IProductService productService, Guid id) : this(productService)
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

                var product = _productService.GetProductById(_productId.Value);

                if (product != null)
                {
                    textBoxArt.Text = product.Article;
                    textBoxArt.ForeColor = Color.Black;
                    textBoxArt.ReadOnly = true;
                    labelDeleteProduct.Text = string.Format(Resources.DeleteProductWithDetails, product.Article, product.Name);
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

            return _productService.GetProductByArticle(article);
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


                if (!ConfirmDelete(product))
                {
                    return;
                }

                _productService.DeleteProduct(product.Id);

                logger.Info($"Товар '{product.Article} - {product.Name}' удален");

                MessageBox.Show(Resources.SuccessProductDeleted, Resources.TitleSuccess,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Ошибка при удалении товара");

                MessageBox.Show(Resources.ErrorDeleteProduct,
                    Resources.TitleError,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Подтверждает удаление товара
        /// </summary>
        private bool ConfirmDelete(Product product)
        {
            var categoryName = product.Category?.Name ?? Resources.CategoryNone;
            var unitName = product.Unit?.Name ?? "шт";

            var result = MessageBox.Show(
                string.Format(Resources.ConfirmDeleteProduct,
                    product.Article, product.Name, categoryName, product.Price, product.Balance, unitName),
                Resources.TitleConfirmation,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            return result == DialogResult.Yes;
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
                return _productService.GetProductById(_productId.Value);
            }

            return FindProductByArticle(textBoxArt.Text);
        }
    }
}