using AutomechanicsProject.Classes;
using AutomechanicsProject.Properties;
using Microsoft.EntityFrameworkCore;
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
        private readonly DateBase db;
        /// <summary>
        /// Инициализирует новый экземпляр формы удаления категории
        /// </summary>
        public DeleteCategory()
        {
            InitializeComponent();
            db = new DateBase();
            Load += DeleteCategory_Load;
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
                var categories = db.Categories
                    .OrderBy(c => c.Name)
                    .Select(c => new
                    {
                        c.Id,
                        DisplayName = $"{c.Name} (товаров: {db.Products.Count(p => p.CategoryId == c.Id)})"
                    })
                    .ToList();

                comboBoxCategory.DisplayMember = "DisplayName";
                comboBoxCategory.ValueMember = "Id";
                comboBoxCategory.DataSource = categories;

                var hasCategories = comboBoxCategory.Items.Count > 0;
                comboBoxCategory.SelectedIndex = hasCategories ? -1 : -1;

                label1.Text = hasCategories ? "Выберите категорию для удаления" : "Нет доступных категорий для удаления";
                buttonDelete.Enabled = hasCategories;
            }
            catch (Exception ex)
            {
                Program.LogError("Ошибка при загрузке категорий в DeleteCategory", ex);
                MessageBox.Show("Не удалось загрузить категории",
                    Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                var selectedItem = (dynamic)comboBoxCategory.SelectedItem;
                var categoryId = (Guid)selectedItem.Id;
                var category = db.Categories
                    .FirstOrDefault(c => c.Id == categoryId);

                if (category == null)
                {
                    MessageBox.Show("Категория не найдена в базе данных!", Resources.TitleError,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var products = db.Products.Where(p => p.CategoryId == categoryId).ToList();
                if (products.Any())
                {
                    var productIds = products.Select(p => p.Id).ToList();
                    var hasShipments = db.ShipmentItems.Any(si => productIds.Contains(si.ProductId));

                    if (hasShipments)
                    {
                        MessageBox.Show(
                            "Невозможно удалить категорию, так как некоторые товары используются в отгрузках.\n" +
                            "Сначала удалите связанные отгрузки.",
                            Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                var confirmMessage = products.Any()
                    ? $"В категории \"{category.Name}\" находится {products.Count} товаров.\n\n" +
                      "Вы уверены, что хотите удалить категорию и все товары в ней?\n" +
                      "Это действие нельзя отменить"
                    : $"Вы действительно хотите удалить категорию \"{category.Name}\"?";

                var result = MessageBox.Show(confirmMessage,
                    products.Any() ? "Подтверждение удаления" : Resources.TitleConfirmation,
                    MessageBoxButtons.YesNo,
                    products.Any() ? MessageBoxIcon.Warning : MessageBoxIcon.Question);

                if (result != DialogResult.Yes) return;
                if (products.Any())
                {
                    db.Products.RemoveRange(products);
                    Program.LogInfo($"Удалено {products.Count} товаров из категории '{category.Name}'");
                }
                db.Categories.Remove(category);
                db.SaveChanges();
                Program.LogInfo($"Категория '{category.Name}' успешно удалена");

                var successMessage = products.Any()
                    ? $"Категория \"{category.Name}\" и все ({products.Count}) товары в ней успешно удалены!"
                    : string.Format(Resources.SuccessCategoryDeleted, category.Name);

                MessageBox.Show(successMessage, Resources.TitleSuccess,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                Program.LogError("Ошибка при удалении категории", ex);
                MessageBox.Show("Не удалось удалить категорию. Попробуйте позже.",
                    Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
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