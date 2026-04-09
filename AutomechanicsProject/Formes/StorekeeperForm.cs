using AutomechanicsProject.Classes;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Properties;
using System;
using System.Linq;
using System.Windows.Forms;

namespace AutomechanicsProject.Formes
{
    /// <summary>
    /// Главная форма кладовщика
    /// </summary>
    public partial class StorekeeperForm : Form
    {
        private readonly DateBase db;

        /// <summary>
        /// Инициализирует новый экземпляр формы кладовщика
        /// </summary>
        public StorekeeperForm()
        {
            InitializeComponent();
            db = new DateBase();

            TextBoxHelper.SetupWatermarkTextBox(textBoxSearch, Resources.SearchWatermark);

            buttonCurrency.Click += ButtonCurrency_Click;
            buttonSupply.Click += ButtonSupply_Click;
            buttonShipment.Click += ButtonShipment_Click;
            buttonExit.Click += ButtonExit_Click;
            textBoxSearch.TextChanged += TextBoxSearch_TextChanged;
            dataGridViewStore.DataBindingComplete += DataGridViewStore_DataBindingComplete;

            LoadProducts();
        }

        /// <summary>
        /// Обработчик события завершения привязки данных для подсветки строк
        /// </summary>
        private void DataGridViewStore_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var today = DateTime.Today;

            foreach (DataGridViewRow row in dataGridViewStore.Rows)
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
                    Program.LogError(Resources.ErrorHighlightRow, ex);
                }
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Выбор валюты"
        /// Открывает форму выбора валюты и обновляет список товаров
        /// </summary>
        private void ButtonCurrency_Click(object sender, EventArgs e)
        {
            try
            {
                using (var currencyForm = new ChoosingCurrency())
                {
                    if (currencyForm.ShowDialog() == DialogResult.OK)
                    {
                        RefreshProductList();
                        Program.LogInfo(Resources.CurrencyChanged);
                    }
                }
            }
            catch (Exception ex)
            {
                Program.LogError(Resources.ErrorOpenCurrencyForm, ex);
                MessageBox.Show(Resources.ErrorOpenCurrencyForm, Resources.TitleError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Поставка"
        /// Открывает форму добавления поставки
        /// </summary>
        private void ButtonSupply_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(Resources.SupplyFormInDevelopment, Resources.TitleInformation,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Program.LogError(Resources.ErrorOpenSupplyForm, ex);
                MessageBox.Show(Resources.ErrorOpenSupplyForm, Resources.TitleError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Оформить отгрузку"
        /// Открывает форму создания отгрузки и обновляет список товаров после её завершения
        /// </summary>
        private void ButtonShipment_Click(object sender, EventArgs e)
        {
            try
            {
                using (var shipmentForm = new CreateShipment())
                {
                    if (shipmentForm.ShowDialog() == DialogResult.OK)
                    {
                        RefreshProductList();
                        Program.LogInfo(Resources.ShipmentSuccess);
                    }
                }
            }
            catch (Exception ex)
            {
                Program.LogError(Resources.ErrorOpenShipmentForm, ex);
                MessageBox.Show(Resources.ErrorOpenShipmentForm, Resources.TitleError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Выйти"
        /// Завершает работу приложения
        /// </summary>
        private void ButtonExit_Click(object sender, EventArgs e)
        {
            try
            {
                Program.LogInfo(Resources.UserLogout);
                this.Close();
                var loginForm = new Autorization();
                loginForm.ShowDialog();
            }
            catch (Exception ex)
            {
                Program.LogError(Resources.ErrorLogout, ex);
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
                var today = DateTime.Today;
                var normalizedSearch = NormalizeSearch(searchText);

                var query = db.Products
                    .Select(p => new
                    {
                        p.Id,
                        Артикул = p.Article,
                        Название = p.Name,
                        Категория = p.Category.Name,
                        ЕдИзмерения = p.Unit.Name,
                        СрокГодности = p.ExpiryDate,
                        Цена = ChoosingCurrency.ConvertPrice(p.Price * 2),
                        ЦенаЗакупки = p.Price,
                        Остаток = p.Balance,
                        ТребуетСкидки = p.ExpiryDate.HasValue &&
                            p.ExpiryDate.Value > today &&
                            (p.ExpiryDate.Value - today).Days <= 30,
                        Просрочен = p.ExpiryDate.HasValue && p.ExpiryDate.Value < today
                    });

                if (!string.IsNullOrEmpty(normalizedSearch))
                {
                    query = query.Where(p =>
                        p.Артикул.ToLower().Contains(normalizedSearch) ||
                        p.Название.ToLower().Contains(normalizedSearch) ||
                        p.Категория.ToLower().Contains(normalizedSearch));
                }

                var products = query.ToList();
                dataGridViewStore.DataSource = products;
                ConfigureGrid();
                FormatDateColumn();
            }
            catch (Exception ex)
            {
                Program.LogError(Resources.ErrorLoadProducts, ex);
                MessageBox.Show(Resources.ErrorLoadProductsList, Resources.TitleError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Настраивает внешний вид таблицы
        /// </summary>
        private void ConfigureGrid()
        {
            if (dataGridViewStore.Columns.Contains("Id"))
                dataGridViewStore.Columns["Id"].Visible = false;

            if (dataGridViewStore.Columns.Contains("ТребуетСкидки"))
                dataGridViewStore.Columns["ТребуетСкидки"].Visible = false;
            if (dataGridViewStore.Columns.Contains("Просрочен"))
                dataGridViewStore.Columns["Просрочен"].Visible = false;

            SetHeader("Артикул", Resources.ArticleColumnHeader);
            SetHeader("Название", Resources.NameColumnHeader);
            SetHeader("Категория", Resources.CategoryColumnHeader);
            SetHeader("ЕдИзмерения", Resources.UnitColumnHeader);
            SetHeader("СрокГодности", Resources.ExpiryDateColumnHeader);
            SetHeader("Цена", string.Format(Resources.PriceColumnHeader, ChoosingCurrency.SelectedCurrencyCode));
            SetHeader("ЦенаЗакупки", Resources.PurchasePriceColumnHeader);
            SetHeader("Остаток", Resources.BalanceColumnHeader);

            SetColumnOrder();
        }

        /// <summary>
        /// Устанавливает порядок колонок в таблице
        /// </summary>
        private void SetColumnOrder()
        {
            string[] columnOrder = {
                "Артикул", "Название", "Категория", "ЕдИзмерения",
                "СрокГодности", "Цена", "ЦенаЗакупки", "Остаток"
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
            if (dataGridViewStore.Columns["СрокГодности"] != null)
            {
                dataGridViewStore.Columns["СрокГодности"].DefaultCellStyle.Format = "dd.MM.yyyy";
                dataGridViewStore.Columns["СрокГодности"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dataGridViewStore.Columns["Цена"] != null)
            {
                dataGridViewStore.Columns["Цена"].DefaultCellStyle.Format = "F2";
                dataGridViewStore.Columns["Цена"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (dataGridViewStore.Columns["ЦенаЗакупки"] != null)
            {
                dataGridViewStore.Columns["ЦенаЗакупки"].DefaultCellStyle.Format = "F2";
                dataGridViewStore.Columns["ЦенаЗакупки"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (dataGridViewStore.Columns["Остаток"] != null)
            {
                dataGridViewStore.Columns["Остаток"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        /// <summary>
        /// Устанавливает заголовок для указанной колонки таблицы
        /// </summary>
        private void SetHeader(string columnName, string header)
        {
            if (dataGridViewStore.Columns[columnName] != null)
                dataGridViewStore.Columns[columnName].HeaderText = header;
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
        /// Нормализует поисковый запрос (удаляет лишние пробелы, приводит к нижнему регистру)
        /// </summary>
        private string NormalizeSearch(string text)
        {
            if (string.IsNullOrWhiteSpace(text) || text == Resources.SearchWatermark)
            {
                return "";
            }
            return text.Trim().ToLower();
        }

        /// <summary>
        /// Обработчик события загрузки формы
        /// </summary>
        private void StorekeeperForm_Load(object sender, EventArgs e)
        {
            try
            {
                toolStripTextBoxStorekeeper.Text = Resources.StorekeeperRoleName;
                Program.LogInfo(Resources.StorekeeperFormLoaded);
            }
            catch (Exception ex)
            {
                toolStripTextBoxStorekeeper.Text = Resources.ErrorLabel;
                Program.LogError(Resources.ErrorLoadStorekeeperForm, ex);
            }
        }
    }
}