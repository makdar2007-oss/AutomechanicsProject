using AutomechanicsProject.Classes;
using AutomechanicsProject.Dtos.Service;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Mappers;
using AutomechanicsProject.Properties;
using AutomechanicsProject.Services.Interfaces;
using AutomechanicsProject.Services;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AutomechanicsProject.Formes
{
    /// <summary>
    /// Главная форма кладовщика
    /// </summary>
    public partial class StorekeeperForm : Form
    {
        private readonly IProductService _productService;
        private readonly IAuthService _authService;
        private readonly IShipmentService _shipmentService;
        private readonly ISupplyService _supplyService;
        private readonly IExpiredProductsService _expiredProductsService;
        private readonly ISupplyCurrencyService _supplyCurrencyService;
        private readonly ICategoryService _categoryService;
        private readonly IReportService _reportService;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICurrencySettingsService _currencySettingsService;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Инициализирует новый экземпляр формы кладовщика
        /// </summary>
        public StorekeeperForm(
             IProductService productService,
             ICategoryService categoryService,
             IAuthService authService,
             IShipmentService shipmentService,
             ISupplyService supplyService,
             IReportService reportService,
             IExpiredProductsService expiredProductsService,
             ISupplyCurrencyService supplyCurrencyService,
            ICurrentUserService currentUserService,
            ICurrencySettingsService currencySettingsService)
        {
            InitializeComponent();

            
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _shipmentService = shipmentService ?? throw new ArgumentNullException(nameof(shipmentService));
            _supplyService = supplyService ?? throw new ArgumentNullException(nameof(supplyService));
            _expiredProductsService = expiredProductsService ?? throw new ArgumentNullException(nameof(expiredProductsService));
            _supplyCurrencyService = supplyCurrencyService ?? throw new ArgumentNullException(nameof(supplyCurrencyService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _reportService = reportService ?? throw new ArgumentNullException(nameof(reportService));
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
            _currencySettingsService = currencySettingsService ?? throw new ArgumentNullException(nameof(currencySettingsService));

            AutoWriteOffExpiredProducts();

            TextBoxHelper.SetupWatermarkTextBox(textBoxSearch, Resources.SearchWatermark);

            LoadProducts();
        }

        /// <summary>
        /// Обработчик события завершения привязки данных для подсветки строк
        /// </summary>
        private void DataGridViewStore_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var today = MoscowTime.Today;

            foreach (DataGridViewRow row in dataGridViewStore.Rows)
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
                    logger.Error(ex,"Ошибка при подсветке строки");
                }
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
                MessageBox.Show(Resources.ErrorWithDetails,
                    Resources.TitleError,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                logger.Error(ex,"Ошибка при списании товаров с истекшим сроком годности");
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Выбор валюты"
        /// </summary>
        private void ButtonCurrency_Click(object sender, EventArgs e)
        {
            try
            {
                using (var currencyForm = new ChoosingCurrency(_currencySettingsService))
                {
                    if (currencyForm.ShowDialog() == DialogResult.OK)
                    {
                        RefreshProductList();
                        logger.Info("Валюта изменена");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex,"Ошибка при открытии формы выбора валюты");
                MessageBox.Show(Resources.ErrorOpenCurrencyForm, Resources.TitleError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Оформить отгрузку"
        /// </summary>
        private void ButtonShipment_Click(object sender, EventArgs e)
        {
            try
            {
                using (var shipmentForm = new CreateShipment(_shipmentService, _currentUserService))
                {
                    if (shipmentForm.ShowDialog() == DialogResult.OK)
                    {
                        RefreshProductList();
                        logger.Info("Отгрузка успешно оформлена");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
                logger.Info("Пользователь вышел из системы, валюта сброшена");

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
            catch (Exception ex)
            {
                logger.Error(ex, "Ошибка при выходе из системы");
                MessageBox.Show(Resources.ErrorLogout, Resources.TitleError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обновляет список товаров с учётом текущего поискового запроса
        /// </summary>
        public void RefreshProductList()
        {
            LoadProducts(textBoxSearch.Text);
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
                    Price = GetPurchasePriceBySupplyRate(p.Id, p.Price).ToString("F2"),
                    RequiresDiscount = p.RequiresDiscount,
                    IsExpired = p.IsExpired,
                    PurchasePrice = CalculateProductPrice(p).ToString("F2"),
                    Balance = p.Balance,

                }).ToList();

                dataGridViewStore.DataSource = displayData;
                ConfigureGrid();
                FormatDateColumn();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Ошибка при загрузке товаров");

                MessageBox.Show(Resources.ErrorLoadProducts,
                    Resources.TitleError,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Метод для расчета скидки у товара с истекающим сроком годности
        /// </summary>
        private decimal CalculateProductPrice(ProductListItemDto product)
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
        /// Получает цену закупки в выбранной валюте по по курсу поставки
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
                logger.Error($"Ошибка конвертации цены для товара {productId}", ex);
                return _currencySettingsService.ConvertPrice(priceInRub);
            }
        }

        /// <summary>
        /// Настраивает внешний вид таблицы
        /// </summary>
        private void ConfigureGrid()
        {
            if (dataGridViewStore.Columns.Contains("Id"))
            {
                dataGridViewStore.Columns["Id"].Visible = false;
            }

            if (dataGridViewStore.Columns.Contains("RequiresDiscount"))
            {
                dataGridViewStore.Columns["RequiresDiscount"].Visible = false;
            }
            if (dataGridViewStore.Columns.Contains("IsExpired"))
            {
                dataGridViewStore.Columns["IsExpired"].Visible = false;
            }

            if (dataGridViewStore.Columns["Article"] != null)
            {
                dataGridViewStore.Columns["Article"].HeaderText = Resources.ColumnArticle;
            }

            if (dataGridViewStore.Columns["Name"] != null)
            {
                dataGridViewStore.Columns["Name"].HeaderText = Resources.ColumnName;
            }

            if (dataGridViewStore.Columns["Category"] != null)
            {
                dataGridViewStore.Columns["Category"].HeaderText = Resources.ColumnCategory;
            }

            if (dataGridViewStore.Columns["Unit"] != null)
            {
                dataGridViewStore.Columns["Unit"].HeaderText = Resources.ColumnUnit;

            }
            if (dataGridViewStore.Columns["ExpiryDate"] != null)
            {
                dataGridViewStore.Columns["ExpiryDate"].HeaderText = Resources.ColumnExpiryDate;
            }

            if (dataGridViewStore.Columns["PurchasePrice"] != null)
            {
                dataGridViewStore.Columns["PurchasePrice"].HeaderText = string.Format(Resources.ColumnPurchasePriceFormat, _currencySettingsService.SelectedCurrencyCode);
            }

            if (dataGridViewStore.Columns["Price"] != null)
            {
                dataGridViewStore.Columns["Price"].HeaderText = Resources.ColumnPrice;
            }

            if (dataGridViewStore.Columns["Balance"] != null)
            {
                dataGridViewStore.Columns["Balance"].HeaderText = Resources.ColumnBalance;
            }

            SetColumnOrder();
        }

        /// <summary>
        /// Устанавливает порядок колонок в таблице
        /// </summary>
        private void SetColumnOrder()
        {
            string[] columnOrder = {
                "Article", "Name", "Category", "Unit",
                "ExpiryDate", "PurchasePrice", "Price", "Balance"
            };

            for (int i = 0; i < columnOrder.Length; i++)
            {
                if (dataGridViewStore.Columns[columnOrder[i]] != null)
                    dataGridViewStore.Columns[columnOrder[i]].DisplayIndex = i;
            }
        }

        /// <summary>
        /// Форматирует колонки с датой и ценой
        /// </summary>
        private void FormatDateColumn()
        {
            if (dataGridViewStore.Columns["ExpiryDate"] != null)
            {
                dataGridViewStore.Columns["ExpiryDate"].DefaultCellStyle.Format = "dd.MM.yyyy";
                dataGridViewStore.Columns["ExpiryDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dataGridViewStore.Columns["PurchasePrice"] != null)
            {
                dataGridViewStore.Columns["PurchasePrice"].DefaultCellStyle.Format = "F2";
                dataGridViewStore.Columns["PurchasePrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (dataGridViewStore.Columns["Price"] != null)
            {
                dataGridViewStore.Columns["Price"].DefaultCellStyle.Format = "F2";
                dataGridViewStore.Columns["Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (dataGridViewStore.Columns["Balance"] != null)
            {
                dataGridViewStore.Columns["Balance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            dataGridViewStore.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewStore.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        /// <summary>
        /// Выполняет фильтрацию товаров при каждом изменении текста в поле поиска
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
        /// Обработчик события загрузки формы
        /// </summary>
        private void StorekeeperForm_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridViewStore.CellMouseEnter += DataGridViewStore_CellMouseEnter;
                dataGridViewStore.CellMouseLeave += DataGridViewStore_CellMouseLeave;

                logger.Info("Кладовщик вошёл в StorekeeperForm");
            }
            catch (Exception ex)
            {
                toolStripTextBoxStorekeeper.Text = Resources.StatusError;
                logger.Error(ex,"Ошибка при загрузке формы кладовщика");
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки поставки
        /// </summary>
        private void buttonSupply_Click_1(object sender, EventArgs e)
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
        /// Показывает информацию о курсе при наведении на ячейку "ЦенаЗакупки"
        /// </summary>
        private void DataGridViewStore_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string columnName = dataGridViewStore.Columns[e.ColumnIndex].Name;

                if (columnName == "Price")
                {
                    var article = dataGridViewStore.Rows[e.RowIndex].Cells["Article"]?.Value?.ToString();
                    if (!string.IsNullOrEmpty(article))
                    {
                        var product = _productService.GetProductByArticle(article);
                        if (product != null)
                        {
                            var tooltipText = _supplyCurrencyService.GetTooltipText(product.Id, product.Name);
                            dataGridViewStore.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = tooltipText;
                        }
                    }
                }
                else
                {
                    dataGridViewStore.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = null;
                }
            }
        }
        /// <summary>
        /// Прячет информацию о курсе
        /// </summary>
        private void DataGridViewStore_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dataGridViewStore.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = null;
            }
        }
    }
}