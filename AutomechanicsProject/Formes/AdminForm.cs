using AutomechanicsProject.Classes;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Properties;
using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;

namespace AutomechanicsProject.Formes
{
    /// <summary>
    /// Главная форма администратора системы
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

            AutoWriteOffExpiredProducts();

            RefreshProductList();

            ProductToolStripMenuItem.Click += (s, e) => OpenAddProductForm();
            CategoryToolStripMenuItem.Click += (s, e) => OpenAddCategoryForm();
            ProductToolStripMenuItem1.Click += (s, e) => OpenEditProductForm();
            CategoryToolStripMenuItem1.Click += (s, e) => OpenEditCategoryForm();
            ProductToolStripMenuItem2.Click += (s, e) => OpenDeleteProductForm();
            CategoryToolStripMenuItem2.Click += (s, e) => OpenDeleteCategoryForm();

            dataGridViewMainForm.DataBindingComplete += DataGridViewMainForm_DataBindingComplete;
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
                        RefreshProductList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ErrorOpenEditCategoryForm, Resources.TitleError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Автоматически списывает товары с истекшим сроком годности
        /// </summary>
        private void AutoWriteOffExpiredProducts()
        {
            try
            {
                var today = DateTime.Today;

                var expiredProducts = db.Products
                    .Where(p => p.ExpiryDate.HasValue
                        && p.ExpiryDate.Value < today
                        && p.Balance > 0)
                    .ToList();

                if (!expiredProducts.Any()) return;

                Guid writeOffUserId = new Guid("4adf792a-247b-435d-a15e-37314224c761");
                Guid writeOffAddressId = new Guid("dc40ff88-af12-4841-b101-9da423f7f777");

                foreach (var product in expiredProducts)
                {
                    var shipment = new Shipment
                    {
                        Id = Guid.NewGuid(),
                        Date = DateTime.Now,
                        UserId = writeOffAddressId,
                        CreatedByUserId = writeOffUserId
                    };

                    db.Shipments.Add(shipment);
                    db.SaveChanges();

                    var shipmentItem = new ShipmentItem
                    {
                        ShipmentId = shipment.Id,
                        Product = product,
                        Article = product.Article,
                        ProductName = product.Name,
                        Quantity = -product.Balance,
                        Price = product.Price,
                        PurchasePrice = product.Price * 2
                    };

                    db.ShipmentItems.Add(shipmentItem);
                    product.Balance = 0;
                }

                db.SaveChanges();
                RefreshProductList();

                MessageBox.Show($"Списано {expiredProducts.Count} просроченных товаров.",
                    "Автоматическое списание", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                if (ex.InnerException != null)
                    error += "\n\n" + ex.InnerException.Message;

                MessageBox.Show($"Ошибка: {error}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обновляет список товаров в таблице
        /// </summary>
        public void RefreshProductList()
        {
            LoadProducts(textBoxSearch.Text);
        }

        /// <summary>
        /// Получает выбранный товар из DataGridView
        /// </summary>
        /// <returns>Выбранный товар или null, если ничего не выбрано</returns>
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
            catch (Exception)
            {
                // Ошибка логируется в ресурсах
            }

            return null;
        }

        /// <summary>
        /// Обработчик события загрузки формы администратора
        /// </summary>
        private void AdminForm_Load(object sender, EventArgs e)
        {
            try
            {
                toolStripTextBoxAdmin.Text = Resources.AdminRoleName;
                dataGridViewMainForm.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridViewMainForm.AllowUserToResizeColumns = false;
                dataGridViewMainForm.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewMainForm.MultiSelect = false;

                RefreshProductList();
            }
            catch (Exception)
            {
                toolStripTextBoxAdmin.Text = Resources.ErrorLabel;
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Выйти"
        /// </summary>
        private void ButtonExit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                var loginForm = new Autorization();
                loginForm.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show(Resources.ErrorLogout, Resources.TitleError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "История"
        /// Открывает форму истории отгрузок
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
            catch (Exception)
            {
                MessageBox.Show(Resources.ErrorOpenHistory, Resources.TitleError,
                     MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            catch (Exception)
            {
                MessageBox.Show(Resources.ErrorOpenAddProductForm, Resources.TitleError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            catch (Exception)
            {
                MessageBox.Show(Resources.ErrorOpenAddCategoryForm, Resources.TitleError,
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Открывает форму редактирования товара
        /// </summary>
        private void OpenEditProductForm()
        {
            var product = GetSelectedProduct();
            if (product == null)
            {
                MessageBox.Show(Resources.SelectProductForEdit, Resources.TitleInformation,
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
                FormHelper.HandleException(Resources.ErrorEditProduct, ex);
            }
        }

        /// <summary>
        /// Открывает форму удаления товара
        /// </summary>
        private void OpenDeleteProductForm()
        {
            var product = GetSelectedProduct();
            if (product == null)
            {
                MessageBox.Show(Resources.SelectProductForDelete, Resources.TitleInformation,
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
                FormHelper.HandleException(Resources.ErrorDeleteProduct, ex);
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
            catch (Exception)
            {
                MessageBox.Show(Resources.ErrorOpenDeleteCategoryForm, Resources.TitleError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Загружает список товаров из базы данных с учётом поискового запроса
        /// </summary>
        /// <param name="searchText">Поисковый запрос</param>
        private void LoadProducts(string searchText = "")
        {
            try
            {
                var today = DateTime.Today;

                var query = db.Products
                    .Where(p => p.Balance > 0)
                    .Select(p => new
                    {
                        p.Article,
                        p.Name,
                        CategoryName = p.Category.Name,
                        UnitName = p.Unit.Name,
                        p.ExpiryDate,
                        p.Price,
                        p.Balance
                    })
                    .AsEnumerable()
                    .Select(p => new
                    {
                        Артикул = p.Article,
                        Название = p.Name,
                        Категория = p.CategoryName,
                        ЕдИзмерения = p.UnitName,
                        СрокГодности = p.ExpiryDate,
                        Остаток = p.Balance,
                        ЦенаЗакупки = p.Price,
                        ТребуетСкидки = p.ExpiryDate.HasValue &&
               p.ExpiryDate.Value > today &&
               (p.ExpiryDate.Value - today).Days <= 30,

                        Просрочен = p.ExpiryDate.HasValue && p.ExpiryDate.Value < today,
                        Цена = ChoosingCurrency.ConvertPrice(
                            p.ExpiryDate.HasValue &&
                            p.ExpiryDate.Value > today &&
                            (p.ExpiryDate.Value - today).Days <= 30
                                ? p.Price * 2 * 0.9m
                                : p.Price * 2
                        )
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
                FormatDataGridViewColumns();
            }
            catch (Exception)
            {
                MessageBox.Show(Resources.ErrorLoadProductsList, Resources.TitleError,
                     MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Настраивает форматирование колонок DataGridView
        /// </summary>
        private void FormatDataGridViewColumns()
        {
            try
            {
                string[] columnOrder = {
                    "Артикул", "Название", "Категория", "ЕдИзмерения",
                    "СрокГодности", "Цена", "ЦенаЗакупки", "Остаток"
                };

                for (int i = 0; i < columnOrder.Length; i++)
                {
                    if (dataGridViewMainForm.Columns[columnOrder[i]] != null)
                    {
                        dataGridViewMainForm.Columns[columnOrder[i]].DisplayIndex = i;
                    }
                }

                if (dataGridViewMainForm.Columns["ТребуетСкидки"] != null)
                    dataGridViewMainForm.Columns["ТребуетСкидки"].Visible = false;
                if (dataGridViewMainForm.Columns["Просрочен"] != null)
                    dataGridViewMainForm.Columns["Просрочен"].Visible = false;

                if (dataGridViewMainForm.Columns["Цена"] != null)
                {
                    dataGridViewMainForm.Columns["Цена"].DefaultCellStyle.Format = "F2";
                    dataGridViewMainForm.Columns["Цена"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridViewMainForm.Columns["Цена"].HeaderText = string.Format(Resources.PriceColumnHeader, ChoosingCurrency.SelectedCurrencyCode);
                }

                if (dataGridViewMainForm.Columns["ЦенаЗакупки"] != null)
                {
                    dataGridViewMainForm.Columns["ЦенаЗакупки"].HeaderText = Resources.PurchasePriceColumnHeader;
                    dataGridViewMainForm.Columns["ЦенаЗакупки"].DefaultCellStyle.Format = "F2";
                    dataGridViewMainForm.Columns["ЦенаЗакупки"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                if (dataGridViewMainForm.Columns["СрокГодности"] != null)
                {
                    dataGridViewMainForm.Columns["СрокГодности"].HeaderText = Resources.ExpiryDateColumnHeader;
                    dataGridViewMainForm.Columns["СрокГодности"].DefaultCellStyle.Format = "dd.MM.yyyy";
                    dataGridViewMainForm.Columns["СрокГодности"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                if (dataGridViewMainForm.Columns["ЕдИзмерения"] != null)
                {
                    dataGridViewMainForm.Columns["ЕдИзмерения"].HeaderText = Resources.UnitColumnHeader;
                }

                if (dataGridViewMainForm.Columns["Остаток"] != null)
                {
                    dataGridViewMainForm.Columns["Остаток"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
            catch (Exception)
            {
                // Ошибка форматирования
            }
        }

        /// <summary>
        /// Обработчик завершения привязки данных для подсветки строк
        /// </summary>
        private void DataGridViewMainForm_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewMainForm.Rows)
            {
                if (row.DataBoundItem == null) continue;

                try
                {
                    var dataItem = row.DataBoundItem;
                    var requiresDiscount = (bool)dataItem.GetType().GetProperty("ТребуетСкидки")?.GetValue(dataItem);
                    var isExpired = (bool)dataItem.GetType().GetProperty("Просрочен")?.GetValue(dataItem);

                    if (isExpired)
                    {
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.DarkRed;
                        row.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
                    }
                    else if (requiresDiscount)
                    {
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.LightCoral;
                    }
                }
                catch (Exception)
                {
                    // Ошибка подсветки строки
                }
            }
        }

        /// <summary>
        /// Обработчик изменения текста в поле поиска
        /// </summary>
        private void TextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            var searchText = textBoxSearch.Text;
            if (searchText == Resources.SearchWatermark || string.IsNullOrWhiteSpace(searchText))
            {
                LoadProducts("");
            }
            else
            {
                LoadProducts(searchText);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки выбора валюты
        /// </summary>
        private void buttonCurrency_Click(object sender, EventArgs e)
        {
            using (var currencyForm = new ChoosingCurrency())
            {
                if (currencyForm.ShowDialog() == DialogResult.OK)
                {
                    RefreshProductList();
                }
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки поставки
        /// </summary>
        private void buttonSupply_Click(object sender, EventArgs e)
        {
            // Здесь будет код для формы поставок
        }
    }
}