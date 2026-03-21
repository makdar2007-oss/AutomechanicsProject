using AutomechanicsProject.Classes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;

namespace AutomechanicsProject.Formes
{
    public partial class DeleteCategory : Form
    {
        private DateBase db;
        public DeleteCategory()
        {
            db = new DateBase();
            InitializeComponent();

            this.Load += DeleteCategory_Load;
        }
        private void DeleteCategory_Load(object sender, EventArgs e)
        {
            LoadCategories();
        }
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

                if (comboBoxCategory.Items.Count > 0)
                {
                    comboBoxCategory.SelectedIndex = -1; 
                    buttonDelete.Enabled = false;
                }
                else
                {
                    label1.Text = "Нет доступных категорий для удаления";
                    buttonDelete.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке категорий: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxCategory.SelectedItem == null)
                {
                    MessageBox.Show("Выберите категорию для удаления!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var selectedItem = (dynamic)comboBoxCategory.SelectedItem;
                Guid categoryId = selectedItem.Id;
                var category = db.Categories
                    .Include(c => c.Products)
                    .FirstOrDefault(c => c.Id == categoryId);

                if (category == null)
                {
                    MessageBox.Show("Категория не найдена в базе данных!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int productsCount = db.Products.Count(p => p.CategoryId == categoryId);

                if (productsCount > 0)
                {
                    var result = MessageBox.Show(
                        $"В категории \"{category.Name}\" находится {productsCount} товаров.\n\n" +
                        "Выберите действие:\n" +
                        "Да - удалить категорию и все товары в ней\n" +
                        "Нет - отменить удаление",
                        "Внимание! Есть товары в категории",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        var products = db.Products.Where(p => p.CategoryId == categoryId).ToList();
                        bool hasShipments = db.ShipmentItems.Any(si => products.Select(p => p.Id).Contains(si.ProductId));

                        if (hasShipments)
                        {
                            MessageBox.Show(
                                "Невозможно удалить категорию, так как некоторые товары используются в отгрузках.\n" +
                                "Сначала удалите связанные отгрузки.",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        db.Products.RemoveRange(products);
                        db.Categories.Remove(category);
                        db.SaveChanges();

                        MessageBox.Show(
                            $"Категория \"{category.Name}\" и все ({productsCount}) товары в ней успешно удалены!",
                            "Успех",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    var result = MessageBox.Show(
                        $"Вы действительно хотите удалить категорию \"{category.Name}\"?",
                        "Подтверждение удаления",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        db.Categories.Remove(category);
                        db.SaveChanges();

                        MessageBox.Show(
                            $"Категория \"{category.Name}\" успешно удалена!",
                            "Успех",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        return;
                    }
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении категории: {ex.Message}", "Ошибка",
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
