using AutomechanicsProject.Classes;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AutomechanicsProject.Formes
{
    public partial class AdminForm : Form
    {

        private DateBase db;
        private Timer searchTimer;
        public AdminForm()
        {
            InitializeComponent();
            db = new DateBase();
            LoadProducts();

            searchTimer = new Timer();
            searchTimer.Interval = 500; 
            searchTimer.Tick += SearchTimer_Tick;

            товарToolStripMenuItem.Click += (s, e) => OpenAddProductForm();
            категориюToolStripMenuItem.Click += (s, e) => OpenAddCategoryForm();

            товарToolStripMenuItem1.Click += (s, e) => OpenEditProductForm();
            категориюToolStripMenuItem1.Click += (s, e) => OpenEditCategoryForm();

            товарToolStripMenuItem2.Click += (s, e) => OpenDeleteProductForm();
            категориюToolStripMenuItem2.Click += (s, e) => OpenDeleteCategoryForm();

        }
        public void RefreshProductList()
        {
            LoadProducts(textBoxSearch.Text);
        }
        public dynamic GetSelectedProduct()
        {
            if (dataGridViewMainForm.SelectedRows.Count > 0)
            {
                return dataGridViewMainForm.SelectedRows[0].DataBoundItem;
            }
            return null;
        }
        private void AdminForm_Load(object sender, EventArgs e)
        {
            if (Program.CurrentUser != null)
            {
                toolStripTextBoxAdmin.Text = $"Администратор: {Program.CurrentUser.FullName}";
            }
            else
            {
                toolStripTextBoxAdmin.Text = "Администратор: Неизвестный";
            }
        }
        private void ButtonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ButtonHistory_Click(object sender, EventArgs e)
        {
            var historyForm = new ShipmentHistoryForm();
            historyForm.ShowDialog();
        }

        private void OpenAddProductForm()
        {
            var addProductForm = new AddProduct();
            if (addProductForm.ShowDialog() == DialogResult.OK)
            {
                RefreshProductList();
            }
        }
        private void OpenEditCategoryForm()
        {
            MessageBox.Show("Функция редактирования категорий пока недоступна.",
                "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void OpenAddCategoryForm()
        {
            var addCategoryForm = new AddCategory();
            addCategoryForm.ShowDialog();
            LoadProducts(); 
        }
        private void OpenEditProductForm()
        {
            if (dataGridViewMainForm.SelectedRows.Count > 0)
            {
                var selectedItem = dataGridViewMainForm.SelectedRows[0].DataBoundItem;
                var productId = (Guid)selectedItem.GetType().GetProperty("Id").GetValue(selectedItem);

                var editForm = new RedactProduct(productId);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadProducts();
                }
            }
            else
            {
                MessageBox.Show("Выберите товар для редактирования", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void OpenDeleteProductForm()
        {
            if (dataGridViewMainForm.SelectedRows.Count > 0)
            {
                var selectedItem = dataGridViewMainForm.SelectedRows[0].DataBoundItem;
                var productId = (Guid)selectedItem.GetType().GetProperty("Id").GetValue(selectedItem);

                var result = MessageBox.Show("Вы действительно хотите удалить этот товар?",
                    "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var product = db.Products.Find(productId);
                        if (product != null)
                        {
                            db.Products.Remove(product);
                            db.SaveChanges();
                            LoadProducts();
                            MessageBox.Show("Товар успешно удален!", "Успех",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении товара: {ex.Message}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите товар для удаления", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void OpenDeleteCategoryForm()
        {
            var deleteCategoryForm = new DeleteCategory();
            deleteCategoryForm.ShowDialog();
            LoadProducts(); 
        }

        private void LoadProducts(string searchText = "")
        {
            try
            {
                var query = from p in db.Products
                            join c in db.Categories on p.CategoryId equals c.Id
                            select new
                            {
                                p.Id,
                                p.Article,
                                p.Name,
                                CategoryName = c.Name,
                                p.Unit,
                                p.Price,
                                p.Balance
                            };

                if (!string.IsNullOrWhiteSpace(searchText) && searchText != "Поиск:")
                {
                    searchText = searchText.ToLower();
                    query = query.Where(p =>
                        p.Article.ToLower().Contains(searchText) ||
                        p.Name.ToLower().Contains(searchText) ||
                        p.CategoryName.ToLower().Contains(searchText));
                }

                dataGridViewMainForm.DataSource = query.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке товаров: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void textBoxSearch_Enter(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb.Text == "Поиск:")
            {
                tb.Text = string.Empty;
                tb.ForeColor = Color.Black;
            }
        }
        private void textBoxSearch_Leave(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "Поиск:";
                tb.ForeColor = Color.Gray;
            }
        }

        private void TextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            searchTimer.Stop();
            searchTimer.Start();
        }
        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            searchTimer.Stop();
            LoadProducts(textBoxSearch.Text);
        }
    }
}
