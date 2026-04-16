using AutomechanicsProject.Classes;
using AutomechanicsProject.Dtos.Service;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Mappers;
using AutomechanicsProject.Properties;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
            db = DbContextManager.GetContext();
            DbContextManager.AddReference();

            TextBoxHelper.SetupWatermarkTextBox(textBoxSearch, Resources.SearchWatermark);

            AutoWriteOffExpiredProducts();

            RefreshProductList();

            dataGridViewMainForm.DataBindingComplete += DataGridViewStore_DataBindingComplete;
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
                Program.LogError("Ошибка при открытии формы редактирования категории", ex);
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

                if (!expiredProducts.Any())
                {
                    return;
                }

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
                        Price = product.Price
                    };

                    db.ShipmentItems.Add(shipmentItem);
                    product.Balance = 0;
                }

                db.SaveChanges();
                RefreshProductList();

                MessageBox.Show(string.Format(Resources.SuccessAutoWriteOffMessage, expiredProducts.Count),
                      Resources.TitleAutoWriteOff, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                if (ex.InnerException != null)
                    error += ex.InnerException.Message;

                MessageBox.Show(string.Format(Resources.ErrorWithDetails, error), Resources.TitleError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// </summary>>
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

        /// <summary>
        /// Обработчик события загрузки формы администратора
        /// </summary>
        private void AdminForm_Load(object sender, EventArgs e)
        {
            try
            {
                toolStripTextBoxAdmin.Text = "Администратор";
                dataGridViewMainForm.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridViewMainForm.AllowUserToResizeColumns = false;
                dataGridViewMainForm.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewMainForm.MultiSelect = false;



                RefreshProductList();
            }
            catch (Exception ex)
            {
                toolStripTextBoxAdmin.Text = "Ошибка";
                Program.LogError("Ошибка при загрузке формы администратора", ex);
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

        // <summary>
        /// Обработчик нажатия на пункт меню "Добавить товар"
        /// </summary>
        private void ProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenAddProductForm();
        }

        /// <summary>
        /// Обработчик нажатия на пункт меню "Добавить категорию"
        /// </summary>
        private void CategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenAddCategoryForm();
        }

        /// <summary>
        /// Обработчик нажатия на пункт меню "Редактировать товар"
        /// </summary>
        private void ProductToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenEditProductForm();
        }

        /// <summary>
        /// Обработчик нажатия на пункт меню "Редактировать категорию"
        /// </summary>
        private void CategoryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenEditCategoryForm();
        }

        /// <summary>
        /// Обработчик нажатия на пункт меню "Удалить товар"
        /// </summary>
        private void ProductToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            OpenDeleteProductForm();
        }

        /// <summary>
        /// Обработчик нажатия на пункт меню "Удалить категорию"
        /// </summary>
        private void CategoryToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            OpenDeleteCategoryForm();
        }

        /// <summary>
        /// Обработчик нажатия кнопки История
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
            catch (Exception ex)
            {
                Program.LogError("Ошибка при открытии формы истории", ex);
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
            catch (Exception ex)
            {
                Program.LogError("Ошибка при открытии формы добавления товара", ex);
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
            catch (Exception ex)
            {
                Program.LogError("Ошибка при открытии формы добавления категории", ex);
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
        /// Открывает форму удаления для выбранного товара
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
            catch (Exception ex)
            {
                Program.LogError("Ошибка при удалении категории", ex);
                MessageBox.Show(Resources.ErrorOpenDeleteCategoryForm, Resources.TitleError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Загружает список товаров из базы данных с учётом поискового запроса
        /// </summary>
        private void LoadProducts(string searchText = "")
        {
            try
            {
                var today = DateTime.Today;

                var products = db.Products
                    .Include(p => p.Category)
                    .Include(p => p.Unit)
                    .Where(p => p.Balance > 0)
                    .ToList();

                var productList = new List<ProductListItemDto>();

                foreach (var product in products)
                {
                    var existingItem = productList.FirstOrDefault(p => p.Name == product.Name);
                    if (existingItem != null)
                    {
                        existingItem.Balance += product.Balance;
                    }
                    else
                    {
                        var item = ProductMapper.ToListItemDto(product);
                        productList.Add(item);
                    }
                }

                if (!Validation.IsWatermark(searchText, Resources.SearchWatermark))
                {
                    searchText = searchText.ToLower();
                    productList = productList
                        .Where(p => p.Article.ToLower().Contains(searchText) ||
                                   p.Name.ToLower().Contains(searchText) ||
                                   p.CategoryName.ToLower().Contains(searchText))
                        .ToList();
                }

                var displayData = productList.Select(p => new
                {
                    Артикул = p.Article,
                    Название = p.Name,
                    Категория = p.CategoryName,
                    ЕдИзмерения = p.UnitName,
                    СрокГодности = p.ExpiryDate,
                    Остаток = p.Balance,
                    Закупка = p.Price,
                    ТребуетСкидки = p.RequiresDiscount,
                    Просрочен = p.IsExpired,
                    Цена = ChoosingCurrency.ConvertPrice(p.PurchasePrice)
                }).ToList();

                dataGridViewMainForm.DataSource = displayData;
                GridViewHelper.ConfigureProductColumns(dataGridViewMainForm, ChoosingCurrency.SelectedCurrencyCode);
            }
            catch (Exception ex)
            {
                Program.LogError("Ошибка при загрузке товаров.", ex);
                MessageBox.Show(Resources.ErrorLoadProductsList, Resources.TitleError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик завершения привязки данных для подсветки строк
        /// </summary>
        private void DataGridViewStore_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var today = DateTime.Today;

            foreach (DataGridViewRow row in dataGridViewMainForm.Rows)
            {
                if (row.DataBoundItem == null) continue;

                try
                {
                    var dataItem = row.DataBoundItem;

                    var expiryDateProp = dataItem.GetType().GetProperty("СрокГодности");
                    if (expiryDateProp == null) continue;

                    var expiryDate = expiryDateProp.GetValue(dataItem) as DateTime?;

                    if (expiryDate.HasValue)
                    {
                        if (expiryDate.Value < today)
                        {
                            row.DefaultCellStyle.BackColor = System.Drawing.Color.DarkRed;
                            row.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
                        }
                        else if ((expiryDate.Value - today).Days <= 30)
                        {
                            row.DefaultCellStyle.BackColor = System.Drawing.Color.LightCoral;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Program.LogError("Ошибка при подсветке строки", ex);
                }
            }
        }
        /// <summary>
        /// Обработчик изменения текста в поле поиска
        /// </summary>
        private void TextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            var searchText = textBoxSearch.Text;
            LoadProducts(Validation.IsWatermark(searchText, Resources.SearchWatermark) ? "" : searchText);
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
            CreateSupply supplyForm = new CreateSupply();
            supplyForm.ShowDialog();
        }

        /// <summary>
        /// Открытие формы отчета
        /// </summary>
        private void buttonReport_Click(object sender, EventArgs e)
        {
            ReportForm reportForm = new ReportForm();
            reportForm.ShowDialog();
        }

        /// <summary>
        /// Освобождает ресурсы контекста базы данных при закрытии формы
        /// </summary>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            DbContextManager.ReleaseReference();
        }
    }
}