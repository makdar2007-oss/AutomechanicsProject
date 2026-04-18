using AutomechanicsProject.Classes;
using AutomechanicsProject.Dtos;
using AutomechanicsProject.Dtos.Service;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Mappers;
using AutomechanicsProject.Properties;
using AutomechanicsProject.Services;
using Microsoft.EntityFrameworkCore;
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
        public StorekeeperForm(DateBase database)
        {
            InitializeComponent();
            db = database ?? throw new ArgumentNullException(nameof(database));
            AutoWriteOffExpiredProducts();

            TextBoxHelper.SetupWatermarkTextBox(textBoxSearch, Resources.SearchWatermark);

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
                if (row.DataBoundItem == null)
                {
                    continue;
                }

                try
                {
                    var dataItem = row.DataBoundItem;

                    var expiryDateProp = dataItem.GetType().GetProperty("СрокГодности");
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
                var count = ExpiredProductsService.AutoWriteOffExpiredProducts(db);

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
                MessageBox.Show(
                    string.Format(Resources.ErrorWithDetails, ex.Message),
                    Resources.TitleError,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                Program.LogError("Ошибка при списании товаров", ex);
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
                using (var shipmentForm = new CreateShipment(db))
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
                ChoosingCurrency.SelectedCurrencyCode = CurrencyCodes.RUB;
                ChoosingCurrency.CurrentExchangeRate = 1m;
                ChoosingCurrency.SelectedCurrencyName = "Российский рубль";
                Program.LogInfo("Пользователь вышел из системы, валюта сброшена");

                Program.LogInfo("Пользователь вышел из системы");
                this.Close();

                var loginForm = new Autorization(db);
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

                var products = db.Products
                    .Include(p => p.Category)
                    .Include(p => p.Unit)
                    .Where(p => p.Balance > 0)
                    .Select(p => new
                    {
                        p.Id,
                        p.Article,
                        p.Name,
                        CategoryName = p.Category.Name,
                        UnitName = p.Unit.Name,
                        p.ExpiryDate,
                        p.Price,
                        p.PurchasePrice,
                        p.Balance
                    })
                    .ToList();

                var query = products
                    .GroupBy(p => p.Name)
                    .Select(g => new
                    {
                        Артикул = g.First().Article,
                        Название = g.Key,
                        Категория = g.First().CategoryName,
                        ЕдИзмерения = g.First().UnitName,
                        СрокГодности = g.Where(x => x.ExpiryDate.HasValue)
                                        .OrderBy(x => x.ExpiryDate)
                                        .Select(x => x.ExpiryDate)
                                        .FirstOrDefault(),
                        ЦенаЗакупки = g.First().Price,
                        Остаток = g.Sum(x => x.Balance),
                        ТребуетСкидки = g.Where(x => x.ExpiryDate.HasValue)
                                        .Any(x => x.ExpiryDate.Value > today &&
                                                 (x.ExpiryDate.Value - today).Days <= 30),
                        Просрочен = g.Where(x => x.ExpiryDate.HasValue)
                                     .Any(x => x.ExpiryDate.Value < today),
                        Цена = ChoosingCurrency.ConvertPrice(g.First().PurchasePrice)
                    });

                if (!string.IsNullOrWhiteSpace(searchText) && searchText != Resources.SearchWatermark)
                {
                    searchText = searchText.ToLower();
                    query = query.Where(p =>
                        p.Артикул.ToLower().Contains(searchText) ||
                        p.Название.ToLower().Contains(searchText) ||
                        p.Категория.ToLower().Contains(searchText));
                }

                dataGridViewStore.DataSource = query.ToList();
                ConfigureGrid();
                FormatDateColumn();
            }
            catch (Exception ex)
            {
                Program.LogError("Ошибка при загрузке товаров.", ex);
                MessageBox.Show(Resources.ErrorLoadProductsList, Resources.TitleError,
                     MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Конвертируем цену
        /// </summary>
        private decimal ConvertPriceSafely(decimal originalPrice)
        {
            try
            {
                if (originalPrice <= 0)
                {
                    return 0;
                }

                return ChoosingCurrency.ConvertPrice(originalPrice);
            }
            catch (Exception ex)
            {
                Program.LogError($"Ошибка конвертации цены {originalPrice}", ex);
                return originalPrice;
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

            if (dataGridViewStore.Columns.Contains("ТребуетСкидки"))
            {
                dataGridViewStore.Columns["ТребуетСкидки"].Visible = false;
            }
            if (dataGridViewStore.Columns.Contains("Просрочен"))
            {
                dataGridViewStore.Columns["Просрочен"].Visible = false;
            }

            if (dataGridViewStore.Columns["Артикул"] != null)
            {
                dataGridViewStore.Columns["Артикул"].HeaderText = "Артикул";
            }

            if (dataGridViewStore.Columns["Название"] != null)
            {
                dataGridViewStore.Columns["Название"].HeaderText = "Наименование";
            }

            if (dataGridViewStore.Columns["Категория"] != null)
            {
                dataGridViewStore.Columns["Категория"].HeaderText = "Категория";
            }

            if (dataGridViewStore.Columns["ЕдИзмерения"] != null)
            {
                dataGridViewStore.Columns["ЕдИзмерения"].HeaderText = "Ед. изм.";

            }
            if (dataGridViewStore.Columns["СрокГодности"] != null)
            {
                dataGridViewStore.Columns["СрокГодности"].HeaderText = "Срок годности";
            }
            ;

            if (dataGridViewStore.Columns["Цена"] != null)
            {
                dataGridViewStore.Columns["Цена"].HeaderText = $"Цена ({ChoosingCurrency.SelectedCurrencyCode})";
            }

            if (dataGridViewStore.Columns["ЦенаЗакупки"] != null)
            {
                dataGridViewStore.Columns["ЦенаЗакупки"].HeaderText = "Цена закупки";

            }

            if (dataGridViewStore.Columns["Остаток"] != null)
            {
                dataGridViewStore.Columns["Остаток"].HeaderText = "Остаток";

            }
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
        /// Обработчик нажатия кнопки поставки
        /// </summary>
        private void buttonSupply_Click_1(object sender, EventArgs e)
        {
            using (CreateSupply supplyForm = new CreateSupply(db))
            {
                if (supplyForm.ShowDialog() == DialogResult.OK)
                {
                    RefreshProductList();
                }
            }
        }
    }
}