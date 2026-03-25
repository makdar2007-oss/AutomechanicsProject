    using AutomechanicsProject.Classes;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Properties;
using System;
using System.Linq;
using System.Windows.Forms;

namespace AutomechanicsProject.Formes
{
    /// <summary>
    /// Главная форма администратора системы
    /// Предоставляет полный доступ к управлению товарами, категориями и просмотру истории отгрузок
    /// </summary>
    public partial class AdminForm : Form
    {
        private readonly DateBase db;

        /// <summary>
        /// Инициализирует новый экземпляр формы администратора
        /// </summary>
        public AdminForm()
        {
            InitializeComponent();
            db = new DateBase();

            TextBoxHelper.SetupWatermarkTextBox(textBoxSearch, Resources.SearchWatermark);
            RefreshProductList();
            ProductToolStripMenuItem.Click += (s, e) => OpenAddProductForm();
            CategoryToolStripMenuItem.Click += (s, e) => OpenAddCategoryForm();
            ProductToolStripMenuItem1.Click += (s, e) => OpenEditProductForm();
            CategoryToolStripMenuItem1.Click += (s, e) => OpenEditCategoryForm();
            ProductToolStripMenuItem2.Click += (s, e) => OpenDeleteProductForm();
            CategoryToolStripMenuItem2.Click += (s, e) => OpenDeleteCategoryForm();
        }
        /// <summary>
        /// Открывает форму редактирования категории
        /// </summary>
        private void OpenEditCategoryForm()
        {
            try
            {
                using (var editCategoryForm = new EditCategory())
                {
                    if (editCategoryForm.ShowDialog() == DialogResult.OK)
                    {
                        RefreshProductList();                     }
                }
            }
            catch (Exception ex)
            {
                Program.LogError("Ошибка при открытии формы редактирования категории", ex);
                MessageBox.Show("Не удалось открыть форму редактирования категории",
                    Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Обновляет список товаров с учетом текущего поискового запроса
        /// </summary>
        public void RefreshProductList()
        {
            LoadProducts(textBoxSearch.Text);
        }
        /// <summary>
        /// Получает выбранный товар из таблицы
        /// </summary>
        private Product GetSelectedProduct()
        {
            if (dataGridViewMainForm.SelectedRows.Count == 0)
            {
                return null;
            }
            try
            {
                var selectedRow = dataGridViewMainForm.SelectedRows[0];
                var selectedItem = selectedRow.DataBoundItem;
                var article = selectedItem.GetType()
                    .GetProperty("Артикул")
                    ?.GetValue(selectedItem)
                    ?.ToString();

                if (!string.IsNullOrEmpty(article))
                {
                    return db.Products.FirstOrDefault(p => p.Article == article);
                }
            }
            catch (Exception ex)
            {
                Program.LogError("Ошибка при получении выбранного товара", ex);
            }

            return null;
        }

        ///// <summary>
        ///// Обработчик события загрузки формы
        ///// </summary>
        private void AdminForm_Load(object sender, EventArgs e)
        {
            try
            {
                toolStripTextBoxAdmin.Text = "Администратор";
                dataGridViewMainForm.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridViewMainForm.AllowUserToResizeColumns = false;
                dataGridViewMainForm.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewMainForm.MultiSelect = false;
            }
            catch (Exception ex)
            {
                toolStripTextBoxAdmin.Text = "Ошибка";
                Program.LogError("Ошибка при загрузке AdminForm", ex);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки Выйти
        /// Завершает работу приложения
        /// </summary>
        private void ButtonExit_Click(object sender, EventArgs e)
        {
            Program.LogInfo("Приложение закрыто пользователем");
            Application.Exit();
        }
        /// <summary>
        /// Обработчик нажатия кнопки История отгрузок
        /// Открывает форму просмотра истории отгрузок
        /// </summary>
        private void ButtonHistory_Click(object sender, EventArgs e)
        {
            try
            {
                using (var historyForm = new ShipmentHistoryForm())
                {
                    historyForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Program.LogError("Ошибка при открытии формы истории", ex);
                MessageBox.Show("Не удалось открыть историю",
                    Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Открывает форму добавления товара
        /// </summary>
        private void OpenAddProductForm()
        {
            try
            {
                using (var addProductForm = new AddProduct())
                {
                    if (addProductForm.ShowDialog() == DialogResult.OK)
                    {
                        RefreshProductList();
                    }
                }
            }
            catch (Exception ex)
            {
                Program.LogError("Ошибка при открытии формы добавления товара", ex);
                MessageBox.Show("Не удалось открыть форму добавления товара",
                    Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Открывает форму добавления категории
        /// </summary>
        private void OpenAddCategoryForm()
        {
            try
            {
                using (var addCategoryForm = new AddCategory())
                {
                    if (addCategoryForm.ShowDialog() == DialogResult.OK)
                    {
                        RefreshProductList();
                    }
                }
            }
            catch (Exception ex)
            {
                Program.LogError("Ошибка при открытии формы добавления категории", ex);
                MessageBox.Show("Не удалось открыть форму добавления категории",
                    Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Открывает форму редактирования товара для выбранного товара
        /// </summary>
        private void OpenEditProductForm()
        {
            var product = GetSelectedProduct();
            if (product == null)
            {
                MessageBox.Show("Выберите товар для редактирования", Resources.TitleInformation,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                using (var editForm = new RedactProduct(product.Id))
                {
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        RefreshProductList();
                    }
                }
            }
            catch (Exception ex)
            {
                FormHelper.HandleException("Ошибка при редактировании товара", ex);
            }
        }
        /// <summary>
        /// Открывает форму удаления товара для выбранного товара
        /// </summary>
        private void OpenDeleteProductForm()
        {
            var product = GetSelectedProduct();
            if (product == null)
            {
                MessageBox.Show("Выберите товар для удаления", Resources.TitleInformation,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                using (var deleteForm = new DeleteProduct(product.Id))
                {
                    if (deleteForm.ShowDialog() == DialogResult.OK)
                    {
                        RefreshProductList();
                    }
                }
            }
            catch (Exception ex)
            {
                FormHelper.HandleException("Ошибка при удалении товара", ex);
            }
        }
        /// <summary>
        /// Открывает форму удаления категории
        /// </summary>
        private void OpenDeleteCategoryForm()
        {
            try
            {
                using (var deleteCategoryForm = new DeleteCategory())
                {
                    if (deleteCategoryForm.ShowDialog() == DialogResult.OK)
                    {
                        RefreshProductList();
                    }
                }
            }
            catch (Exception ex)
            {
                Program.LogError("Ошибка при удалении категории", ex);
                MessageBox.Show("Не удалось удалить категорию",
                    Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Загружает список товаров из базы данных с учетом поискового запроса
        /// </summary>
        private void LoadProducts(string searchText = "")
        {
            try
            {
                var query = db.Products
                    .Join(db.Categories,
                        product => product.CategoryId,
                        category => category.Id,
                        (product, category) => new
                        {
                            Артикул = product.Article,
                            Название = product.Name,
                            Категория = category.Name,
                            ЕдИзмерения = product.Unit,
                            Цена = product.Price,
                            Остаток = product.Balance
                        });

                if (!string.IsNullOrWhiteSpace(searchText) && searchText != Resources.SearchWatermark)
                {
                    searchText = searchText.ToLower();
                    query = query.Where(p =>
                        p.Артикул.ToLower().Contains(searchText) ||
                        p.Название.ToLower().Contains(searchText) ||
                        p.Категория.ToLower().Contains(searchText));
                }

                dataGridViewMainForm.DataSource = query.ToList();
            }
            catch (Exception ex)
            {
                Program.LogError($"Ошибка при загрузке товаров. Поисковый запрос: {searchText}", ex);
                MessageBox.Show("Не удалось загрузить список товаров. Попробуйте позже.",
                    Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Обработчик изменения текста в поле поиска
        /// Выполняет фильтрацию товаров при каждом изменении
        /// </summary>
        private void TextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            var searchText = textBoxSearch.Text;
            if (searchText == Resources.SearchWatermark ||
                string.IsNullOrWhiteSpace(searchText))
            {
                LoadProducts("");  
            }
            else
            {
                LoadProducts(searchText);
            }
        }
    }
}