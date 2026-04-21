using AutomechanicsProject.Classes;
using AutomechanicsProject.Dtos.UI;
using AutomechanicsProject.Properties;
using NLog;
using System;
using System.Linq;
using System.Windows.Forms;

namespace AutomechanicsProject.Formes
{
    /// <summary>
    /// Форма для удаления категории товаров
    /// </summary>
    public partial class DeleteCategory : Form
    {
        private readonly DateBase _db;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public DeleteCategory(DateBase database)
        {
            InitializeComponent();
            _db = database ?? throw new ArgumentNullException(nameof(database));
        }

        /// <summary>
        /// Обработчик загрузки формы
        /// </summary>
        private void DeleteCategory_Load(object sender, EventArgs e)
        {
            LoadCategories();
        }

        /// <summary>
        /// Загружает список категорий для удаления
        /// </summary>
        private void LoadCategories()
        {
            try
            {
                comboBoxCategory.DropDownStyle = ComboBoxStyle.DropDownList;

                var categories = _db.Categories
                    .OrderBy(c => c.Name)
                    .Select(c => new ComboItemDto
                    {
                        Id = c.Id,
                        Text = $"{c.Name} (товаров: {_db.Products.Count(p => p.CategoryId == c.Id)})"
                    })
                    .ToList();
                comboBoxCategory.DisplayMember = "Text";
                comboBoxCategory.ValueMember = "Id";
                comboBoxCategory.DataSource = categories;
                comboBoxCategory.SelectedIndex = -1;
                var hasCategories = comboBoxCategory.Items.Count > 0;
                comboBoxCategory.SelectedIndex = hasCategories ? -1 : -1;

                label1.Text = hasCategories ? Resources.SelectCategoryForDelete : Resources.NoCategoriesForDelete;
                buttonDelete.Enabled = hasCategories;
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка при загрузке категорий в форму 'Удаление категории'", ex);
                MessageBox.Show(Resources.ErrorLoadCategories, Resources.TitleError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки Удалить
        /// </summary>
        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (comboBoxCategory.SelectedItem == null)
            {
                MessageBox.Show(Resources.ErrorSelectCategoryForDelete, Resources.TitleWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                var selectedItem = (ComboItemDto)comboBoxCategory.SelectedItem;
                var categoryId = selectedItem.Id;
                var category = _db.Categories
                    .FirstOrDefault(c => c.Id == categoryId);

                if (category == null)
                {
                    MessageBox.Show(Resources.ErrorCategoryNotFound, Resources.TitleError,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var products = _db.Products.Where(p => p.CategoryId == categoryId).ToList();
                if (products.Any())
                {
                    var productIds = products.Select(p => p.Id).ToList();
                    var hasShipments = _db.ShipmentItems.Any(si => productIds.Contains(si.ProductId));

                    if (hasShipments)
                    {
                        MessageBox.Show(Resources.ErrorCannotDeleteCategoryWithShipments,
                            Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                var confirmMessage = products.Any()
                    ? string.Format(Resources.ConfirmDeleteCategoryWithProducts, category.Name, products.Count)
                    : string.Format(Resources.ConfirmDeleteCategory, category.Name);

                var result = MessageBox.Show(confirmMessage,
                    products.Any() ? Resources.TitleDeleteConfirmation : Resources.TitleConfirmation,
                    MessageBoxButtons.YesNo,
                    products.Any() ? MessageBoxIcon.Warning : MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                {
                    return;
                }

                if (products.Any())
                {
                    _db.Products.RemoveRange(products);
                    logger.Info($"Удалено {products.Count} товаров из категории '{category.Name}'");
                }
                _db.Categories.Remove(category);
                _db.SaveChanges();
                logger.Info($"Категория '{category.Name}' успешно удалена");

                var successMessage = products.Any()
                    ? string.Format(Resources.SuccessCategoryAndProductsDeleted, category.Name, products.Count)
                    : string.Format(Resources.SuccessCategoryDeleted, category.Name);

                MessageBox.Show(successMessage, Resources.TitleSuccess,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка при удалении категории", ex);
                MessageBox.Show(Resources.ErrorDeleteCategory, Resources.TitleError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки Отмена
        /// </summary>
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}