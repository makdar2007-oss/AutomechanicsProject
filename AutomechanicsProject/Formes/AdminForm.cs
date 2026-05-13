using AutomechanicsProject.Classes;
using AutomechanicsProject.Dtos.Service;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Mappers;
using AutomechanicsProject.Properties;
using AutomechanicsProject.Services;
using AutomechanicsProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IAuthService _authService;
        private readonly ISupplyService _supplyService;
        private readonly IReportService _reportService;
        private readonly IShipmentService _shipmentService;
        private readonly IExpiredProductsService _expiredProductsService;
        private readonly ISupplyCurrencyService _supplyCurrencyService;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICurrencySettingsService _currencySettingsService;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Инициализирует новый экземпляр формы администратора
        /// </summary>
        public AdminForm(
            IProductService productService,
            ICategoryService categoryService,
            IAuthService authService,
            ISupplyService supplyService,
            IReportService reportService,
            IShipmentService shipmentService,
            IExpiredProductsService expiredProductsService,
            ISupplyCurrencyService supplyCurrencyService,
            ICurrentUserService currentUserService,
            ICurrencySettingsService currencySettingsService)
        {
            InitializeComponent();

            
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _supplyService = supplyService ?? throw new ArgumentNullException(nameof(supplyService));
            _reportService = reportService ?? throw new ArgumentNullException(nameof(reportService));
            _shipmentService = shipmentService ?? throw new ArgumentNullException(nameof(shipmentService));
            _expiredProductsService = expiredProductsService ?? throw new ArgumentNullException(nameof(expiredProductsService));
            _supplyCurrencyService = supplyCurrencyService ?? throw new ArgumentNullException(nameof(supplyCurrencyService));
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
            _currencySettingsService = currencySettingsService ?? throw new ArgumentNullException(nameof(currencySettingsService));

            TextBoxHelper.SetupWatermarkTextBox(textBoxSearch, Resources.SearchWatermark);
            dataGridViewMainForm.DataBindingComplete += DataGridViewStore_DataBindingComplete;

            AutoWriteOffExpiredProducts();
            RefreshProductList();

            Logger.Info("AdminForm успешно инициализирована");
        }

        /// <summary>
        /// Открывает форму редактирования категории
        /// </summary>
        private void OpenEditCategoryForm()
        {
            try
            {
                using (var editCategoryForm = new EditCategory(_categoryService))
                {
                    if (editCategoryForm.ShowDialog() == DialogResult.OK)
                    {
                        RefreshProductList();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Ошибка при открытии формы редактирования категории");
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
                var count = _expiredProductsService.AutoWriteOffExpiredProducts();

                if (count > 0)
                {
                    RefreshProductList();

                    MessageBox.Show(
                        string.Format(Resources.SuccessAutoWriteOffMessage, count),
                        Resources.TitleAutoWriteOff,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Ошибка при списании товаров");

                MessageBox.Show(Resources.ErrorAutoWriteOff, Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    .GetProperty("Article")
                    ?.GetValue(selectedItem)
                    ?.ToString();

                if (!string.IsNullOrEmpty(article))
                {
                    return _productService.GetProductByArticle(article);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Ошибка при получении выбранного товара");
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
                Logger.Info("Открыта форма администратора");
                RefreshProductList();
            }
            catch (Exception ex)
            {
                toolStripTextBoxAdmin.Text = Resources.StatusError;
                Logger.Error(ex, "Ошибка при загрузке формы администратора");
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Выйти"
        /// </summary>
        private void ButtonExit_Click(object sender, EventArgs e)
        {
            try
            {
                _currencySettingsService.ResetToRub();
                Logger.Info("Пользователь вышел из системы, валюта сброшена");

                Close();
                var loginForm = new Autorization(
                    _authService,
                    _productService,
                    _categoryService,
                    _supplyService,
                    _reportService,
                    _shipmentService,
                    _expiredProductsService,
                    _supplyCurrencyService,
                    _currentUserService,
                    _currencySettingsService);
                loginForm.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show(Resources.ErrorLogout, Resources.TitleError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
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
                using (var historyForm = new ShipmentHistoryForm(_shipmentService))
                {
                    historyForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Ошибка при отркытии формы истории");
                MessageBox.Show(Resources.ErrorOpenHistory, Resources.TitleError,
                     MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Открывает форму добавления товара
        /// </summary>
        /// <summary>
        /// Открывает форму добавления товара
        /// </summary>
        private void OpenAddProductForm()
        {
            try
            {
                using (var addProductForm = new AddProduct(_productService))
                {
                    if (addProductForm.ShowDialog() == DialogResult.OK)
                    {
                        RefreshProductList();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex,"Ошибка при открытии формы добавления товара");
                MessageBox.Show(Resources.ErrorOpenAddProductForm, Resources.TitleError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// Открывает форму добавления категории
        /// </summary>
        private void OpenAddCategoryForm()
        {
            try
            {
                using (var form = new AddCategory(_categoryService))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        RefreshProductList();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Ошибка при открытии формы добавления категории");
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
                using (var editForm = new RedactProduct(_productService, product.Id))
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
                using (var deleteForm = new DeleteProduct(_productService, product.Id))
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
                using (var deleteCategoryForm = new DeleteCategory(_categoryService))
                {
                    if (deleteCategoryForm.ShowDialog() == DialogResult.OK)
                    {
                        RefreshProductList();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Ошибка при удалении категории");
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
                var today = MoscowTime.Today;

                var products = _productService.GetAllProducts();
                
                    
            
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
                    Article = p.Article,
                    Name = p.Name,
                    Category = p.CategoryName,
                    Unit = p.UnitName,
                    ExpiryDate = p.ExpiryDate,
                    Balance = p.Balance,
                    Price = GetPurchasePriceBySupplyRate(p.Id, p.Price).ToString("F2"),
                    RequiresDiscount = p.RequiresDiscount,
                    IsExpired = p.IsExpired,
                    PurchasePrice = CalculateProductPrice(p).ToString("F2")
                }).ToList();

                dataGridViewMainForm.DataSource = displayData;
                dataGridViewMainForm.ShowCellToolTips = true;
                ConfigureColumns();
            }
            catch (Exception ex)
            {
                Logger.Error(ex,"Ошибка при загрузке товаров");
                MessageBox.Show(Resources.ErrorLoadProductsList, Resources.TitleError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Метод для расчета скидки у товара с истекающим сроком годности
        /// </summary>
        internal decimal CalculateProductPrice(ProductListItemDto product)
        {
            decimal priceInRub = product.PurchasePrice;

            if (product.ExpiryDate.HasValue &&
                product.ExpiryDate.Value.Date >= MoscowTime.Today &&
                (product.ExpiryDate.Value.Date - MoscowTime.Today).Days <= 30)
            {
                priceInRub = priceInRub * 0.9m;
            }

            return _currencySettingsService.ConvertPrice(priceInRub);
        }

        /// <summary>
        /// Ручная настройка колонок таблицы
        /// </summary>
        private void ConfigureColumns()
        {
            string[] columnOrder = { "Article", "Name", "Category", "Unit", "ExpiryDate", "PurchasePrice", "Price", "Balance" };
            for (int i = 0; i < columnOrder.Length; i++)
            {
                if (dataGridViewMainForm.Columns[columnOrder[i]] != null)
                {
                    dataGridViewMainForm.Columns[columnOrder[i]].DisplayIndex = i;
                }
            }

            if (dataGridViewMainForm.Columns["Article"] != null)
            {
                dataGridViewMainForm.Columns["Article"].HeaderText = Resources.ColumnArticle;
            }

            if (dataGridViewMainForm.Columns["Name"] != null)
            {
                dataGridViewMainForm.Columns["Name"].HeaderText = Resources.ColumnName;
            }

            if (dataGridViewMainForm.Columns["Category"] != null)
            {
                dataGridViewMainForm.Columns["Category"].HeaderText = Resources.ColumnCategory;
            }

            if (dataGridViewMainForm.Columns["Unit"] != null)
            {
                dataGridViewMainForm.Columns["Unit"].HeaderText = Resources.ColumnUnit;
            }

            if (dataGridViewMainForm.Columns["ExpiryDate"] != null)
            {
                dataGridViewMainForm.Columns["ExpiryDate"].HeaderText = Resources.ColumnExpiryDate;
                dataGridViewMainForm.Columns["ExpiryDate"].DefaultCellStyle.Format = "dd.MM.yyyy";
                dataGridViewMainForm.Columns["ExpiryDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dataGridViewMainForm.Columns["IsExpired"] != null)
            {
                dataGridViewMainForm.Columns["IsExpired"].Visible = false;
            }
            if (dataGridViewMainForm.Columns["RequiresDiscount"] != null)
                dataGridViewMainForm.Columns["RequiresDiscount"].Visible = false;

            if (dataGridViewMainForm.Columns["PurchasePrice"] != null)
            {
                dataGridViewMainForm.Columns["PurchasePrice"].DefaultCellStyle.Format = "F2";
                dataGridViewMainForm.Columns["PurchasePrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridViewMainForm.Columns["PurchasePrice"].HeaderText = string.Format(Resources.ColumnPurchasePriceFormat, _currencySettingsService.SelectedCurrencyCode);
            }

            if (dataGridViewMainForm.Columns["Price"] != null)
            {
                dataGridViewMainForm.Columns["Price"].DefaultCellStyle.Format = "F2";
                dataGridViewMainForm.Columns["Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridViewMainForm.Columns["Price"].HeaderText = Resources.ColumnPrice;
            }
            if (dataGridViewMainForm.Columns["Balance"] != null)
            {
                dataGridViewMainForm.Columns["Balance"].HeaderText = Resources.ColumnBalance;
                dataGridViewMainForm.Columns["Balance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        /// <summary>
        /// Получает цену закупки в выбранной валюте по курсу поставки 
        /// </summary>
        private decimal GetPurchasePriceBySupplyRate(Guid productId, decimal priceInRub)
        {
            if (_currencySettingsService.SelectedCurrencyCode == CurrencyCodes.RUB)
            {
                return priceInRub;
            }

            try
            {
                var (supplyCurrency, supplyRate, _) = _supplyCurrencyService.GetProductCurrency(productId);

                if (supplyCurrency != CurrencyCodes.RUB && supplyRate != 1.00m)
                {
                    decimal priceInSupplyCurrency = priceInRub * supplyRate;

                    decimal priceInRubAgain = priceInSupplyCurrency / supplyRate;

                    return _currencySettingsService.ConvertPrice(priceInRubAgain);
                }

                return _currencySettingsService.ConvertPrice(priceInRub);
            }
            catch (Exception ex)
            {
                Logger.Error($"Ошибка конвертации цены для товара {productId}", ex);
                return _currencySettingsService.ConvertPrice(priceInRub);
            }
        }

        /// <summary>
        /// Обработчик завершения привязки данных для подсветки строк
        /// </summary>
        private void DataGridViewStore_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var today = MoscowTime.Today;

            foreach (DataGridViewRow row in dataGridViewMainForm.Rows)
            {
                if (row.DataBoundItem == null)
                {
                    continue;
                }

                try
                {
                    var dataItem = row.DataBoundItem;

                    var expiryDateProp = dataItem.GetType().GetProperty("ExpiryDate");
                    if (expiryDateProp == null)
                    {
                        continue;
                    }

                    var expiryDate = expiryDateProp.GetValue(dataItem) as DateTime?;

                    if (expiryDate.HasValue)
                    {
                        if (expiryDate.Value <= today)
                        {
                            row.DefaultCellStyle.BackColor = System.Drawing.Color.Coral;
                            row.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                        }
                        else if ((expiryDate.Value - today).Days <= 30)
                        {
                            row.DefaultCellStyle.BackColor = System.Drawing.Color.LightCoral;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, "Ошибка при подсветке строки");
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

            using (var currencyForm = new ChoosingCurrency(_currencySettingsService))
            {
                if (currencyForm.ShowDialog() == DialogResult.OK)
                {
                    RefreshProductList();
                    Logger.Info("Валюта изменена");
                }
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки поставки
        /// </summary>
        private void buttonSupply_Click(object sender, EventArgs e)
        {
            using (CreateSupply supplyForm = new CreateSupply(_supplyService, _currentUserService))
            {
                if (supplyForm.ShowDialog() == DialogResult.OK)
                {
                    RefreshProductList();
                }
            }
        }

        /// <summary>
        /// Открытие формы отчета
        /// </summary>
        private void buttonReport_Click(object sender, EventArgs e)
        {
            using (var reportForm = new ReportForm(_reportService))
            {
                reportForm.ShowDialog();
            }
        }

        /// <summary>
        /// Показывает информацию о курсе при закупке  при наведении мыши на ячейку
        /// </summary>
        private void DataGridViewMainForm_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string headerText = dataGridViewMainForm.Columns[e.ColumnIndex].HeaderText;

                if (headerText == Resources.ColumnPrice)
                {
                    var article = dataGridViewMainForm.Rows[e.RowIndex].Cells["Article"]?.Value?.ToString();
                    if (!string.IsNullOrEmpty(article))
                    {
                        var product = _productService.GetProductByArticle(article);
                        if (product != null)
                        {
                            var tooltipText = _supplyCurrencyService.GetTooltipText(product.Id, product.Name);

                            dataGridViewMainForm.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = tooltipText;
                        }
                    }
                }
                else
                {
                    dataGridViewMainForm.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = null;
                }
            }
        }

        /// <summary>
        /// Скрывает информацию о курсе при удалении мыши с ячейки
        /// </summary>
        private void DataGridViewMainForm_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dataGridViewMainForm.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = null;
            }
        }
    }
}