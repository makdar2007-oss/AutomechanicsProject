using AutomechanicsProject.Classes;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Properties;
using System;
using System.Linq;
using System.Windows.Forms;
using AutomechanicsProject.Classes.Dtos;

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
            db = DbContextManager.GetContext();
            DbContextManager.AddReference();
            AutoWriteOffExpiredProducts();

            TextBoxHelper.SetupWatermarkTextBox(textBoxSearch, Resources.SearchWatermark);

            buttonCurrency.Click += ButtonCurrency_Click;
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
                    Program.LogError("Ошибка при подсветке строки", ex);
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
                        Program.LogInfo("Валюта изменена");
                    }
                }
            }
            catch (Exception ex)
            {
                Program.LogError("Ошибка при открытии формы выбора валюты", ex);
                MessageBox.Show(Resources.ErrorOpenCurrencyForm, Resources.TitleError,
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
                        Program.LogInfo("Отгрузка успешно оформлена");
                    }
                }
            }
            catch (Exception ex)
            {
                Program.LogError("Ошибка при открытии формы отгрузки", ex);
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
                Program.LogInfo("Пользователь вышел из системы");
                this.Close();
                var loginForm = new Autorization();
                loginForm.ShowDialog();
            }
            catch (Exception ex)
            {
                Program.LogError("Ошибка при выходе из системы", ex);
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
                    .Where(p => p.Balance > 0)
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
                Program.LogError("Ошибка при загрузке товаров", ex);
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

            if (dataGridViewStore.Columns["Артикул"] != null)
                dataGridViewStore.Columns["Артикул"].HeaderText = "Артикул";

            if (dataGridViewStore.Columns["Название"] != null)
                dataGridViewStore.Columns["Название"].HeaderText = "Наименование";

            if (dataGridViewStore.Columns["Категория"] != null)
                dataGridViewStore.Columns["Категория"].HeaderText = "Категория";

            if (dataGridViewStore.Columns["ЕдИзмерения"] != null)
                dataGridViewStore.Columns["ЕдИзмерения"].HeaderText = "Ед. изм.";

            if (dataGridViewStore.Columns["СрокГодности"] != null)
                dataGridViewStore.Columns["СрокГодности"].HeaderText = "Срок годности";

            if (dataGridViewStore.Columns["Цена"] != null)
                dataGridViewStore.Columns["Цена"].HeaderText = $"Цена ({ChoosingCurrency.SelectedCurrencyCode})";

            if (dataGridViewStore.Columns["ЦенаЗакупки"] != null)
                dataGridViewStore.Columns["ЦенаЗакупки"].HeaderText = "Цена закупки";

            if (dataGridViewStore.Columns["Остаток"] != null)
                dataGridViewStore.Columns["Остаток"].HeaderText = "Остаток";

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

            dataGridViewStore.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewStore.RowHeadersVisible = false;
            dataGridViewStore.AllowUserToAddRows = false;
            dataGridViewStore.AllowUserToDeleteRows = false;
            dataGridViewStore.ReadOnly = true;
            dataGridViewStore.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewStore.MultiSelect = false;
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
            if (string.IsNullOrWhiteSpace(text) || text == "Поиск...")
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
                toolStripTextBoxStorekeeper.Text = "Кладовщик";
                Program.LogInfo("Кладовщик вошёл в StorekeeperForm");
            }
            catch (Exception ex)
            {
                toolStripTextBoxStorekeeper.Text = "Ошибка";
                Program.LogError("Ошибка при загрузке формы кладовщика", ex);
            }
        }

        /// <summary>
        /// Освобождает ресурсы контекста базы данных при закрытии формы
        /// </summary>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            DbContextManager.ReleaseReference();
        }

        /// <summary>
        /// Обработчик нажатия кнопки поставки
        /// </summary>
        private void buttonSupply_Click_1(object sender, EventArgs e)
        {
            CreateSupply supplyForm = new CreateSupply();
            supplyForm.ShowDialog();
        }
    }
}