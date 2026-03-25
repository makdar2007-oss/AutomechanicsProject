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

            LoadProducts();
        }
        /// <summary>
        /// Обработчик события загрузки формы
        /// </summary>
        private void StorekeeperForm_Load(object sender, EventArgs e)
        {
            try
            {
                toolStripTextBoxStorekeeper.Text = "Кладовщик";
                Program.LogInfo("Кладовщик вошел в StorekeeperForm");
            }
            catch (Exception ex)
            {
                toolStripTextBoxStorekeeper.Text = "Ошибка";
                Program.LogError("Ошибка при загрузке StorekeeperForm", ex);
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
        /// Обработчик нажатия кнопки Выйти
        /// Завершает работу приложения
        /// </summary>
        private void ButtonExit_Click(object sender, EventArgs e)
        {
            Program.LogInfo("Приложение закрыто пользователем");
            Application.Exit();
        }
        /// <summary>
        /// Обработчик нажатия кнопки Оформить отгрузку
        /// Открывает форму создания отгрузки и обновляет список товаров после ее завершения
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
                MessageBox.Show("Не удалось открыть форму отгрузки",
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
                var normalizedSearch = NormalizeSearch(searchText);
                var products = GetProducts(normalizedSearch);

                dataGridViewStore.DataSource = products;
                ConfigureGrid();
            }
            catch (Exception ex)
            {
                Program.LogError("Ошибка при загрузке товаров", ex);
                MessageBox.Show("Не удалось загрузить список товаров. Попробуйте позже.",
                    Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Получает список товаров из базы данных с применением фильтрации
        /// </summary>
        private object GetProducts(string search)
        {
            var query = db.Products
                .Select(p => new
                {
                    p.Id,
                    Артикул = p.Article,
                    Название = p.Name,
                    Категория = p.Category.Name,
                    ЕдИзмерения = p.Unit,
                    Цена = p.Price,
                    Остаток = p.Balance
                });

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p =>
                    p.Артикул.ToLower().Contains(search) ||
                    p.Название.ToLower().Contains(search) ||
                    p.Категория.ToLower().Contains(search));
            }

            return query.ToList();
        }
        /// <summary>
        /// Настраивает внешний вид таблицы
        /// </summary>
        private void ConfigureGrid()
        {
            if (dataGridViewStore.Columns.Contains("Id"))
                dataGridViewStore.Columns["Id"].Visible = false;

            SetHeader("Артикул", "Артикул");
            SetHeader("Название", "Название");
            SetHeader("Категория", "Категория");
            SetHeader("ЕдИзмерения", "Ед. изм.");
            SetHeader("Цена", "Цена");
            SetHeader("Остаток", "Остаток");
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
        /// <summary>
        /// Нормализует поисковый запрос
        /// </summary>
        private string NormalizeSearch(string text)
        {
            if (string.IsNullOrWhiteSpace(text) ||
                text == Resources.SearchWatermark)
            {
                return "";
            }
            return text.Trim().ToLower(); 
        }
    }
}