using AutomechanicsProject.Classes;
using AutomechanicsProject.Properties;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;
using NLog;

namespace AutomechanicsProject.Formes
{
    /// <summary>
    /// Форма просмотра истории отгрузок и списаний
    /// </summary>
    public partial class ShipmentHistoryForm : Form
    {
        private readonly DateBase _db;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Инициализирует новый экземпляр формы истории отгрузок
        /// </summary>
        public ShipmentHistoryForm(DateBase database)
        {
            InitializeComponent();
            _db = database ?? throw new ArgumentNullException(nameof(database));
        }

        /// <summary>
        /// Обработчик события загрузки формы
        /// Устанавливает начальные даты и загружает историю
        /// </summary>
        private void ShipmentHistoryForm_Load(object sender, EventArgs e)
        {
            dateTimePickerFrom.Value = DateTime.Now.AddMonths(-1);
            dateTimePickerTo.Value = DateTime.Now;
            LoadShipmentHistory();
        }

        /// <summary>
        /// Загружает историю отгрузок за выбранный период
        /// </summary>
        private void LoadShipmentHistory()
        {
            try
            {
                DateTime startDate = dateTimePickerFrom.Value.Date;
                DateTime endDate = dateTimePickerTo.Value.Date.AddDays(1).AddSeconds(-1);

                var shipments = _db.Shipments
                    .Include(s => s.User)
                    .Include(s => s.CreatedByUser)
                    .Include(s => s.Items)
                    .Where(s => s.Date >= startDate && s.Date <= endDate)
                    .OrderByDescending(s => s.Date)
                    .ToList();

                if (shipments.Count == 0)
                {
                    dataGridViewHistory.DataSource = null;
                    MessageBox.Show(Resources.InfoHistoryEmpty, Resources.TitleInformation,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UpdateTotalInfo(0, 0, 0, 0, 0);
                    return;
                }



                var displayList = shipments
                    .Where(s => s.Items != null && s.Items.Any())
                    .SelectMany(s => s.Items.Select(item => new
                    {
                        Article = item.Article ?? "Н/Д",
                        Name = item.ProductName ?? "Н/Д",
                        Quantity = (s.ShipmentType == "Shipment") ? item.Quantity : -item.Quantity,
                        PurchasePrice = item.Price,
                        SalePrice = item.PurchasePrice,
                        Profit = (s.ShipmentType == "WriteOff" || s.ShipmentType == "Defect")
                        ? -(Math.Abs(item.Price) * Math.Abs(item.Quantity))
                        : (item.PurchasePrice - item.Price) * item.Quantity,
                        Total = (s.ShipmentType == "WriteOff" || s.ShipmentType == "Defect")
                        ? 0m
                        : item.PurchasePrice * item.Quantity,
                        Recipient = (s.ShipmentType == "WriteOff") ? "WriteOff"
                        : (s.ShipmentType == "Defect") ? "Defect"
                        : (s.User?.CompanyName ?? "Не указан"),
                        Storekeeper = s.CreatedByUser?.FullName ?? "Не указан",
                        Date = s.Date.ToString("dd.MM.yyyy HH:mm")
                    }))
                    .ToList();

                var finalDisplayList = displayList.Select(item => {
                    bool isWriteOff = (item.Recipient == "WriteOff" || item.Recipient == "Defect");

                    return new
                    {
                        item.Article,
                        item.Name,
                        Quantity = isWriteOff ? -item.Quantity : item.Quantity,
                        PurchasePrice = item.PurchasePrice,
                        SalePrice = isWriteOff ? 0m : item.SalePrice,
                        Profit = item.Profit,
                        Total = isWriteOff ? 0m : item.Total,
                        item.Recipient,
                        item.Storekeeper,
                        item.Date
                    };
                }).ToList();

                if (!finalDisplayList.Any())
                {
                    dataGridViewHistory.DataSource = null;
                    MessageBox.Show(string.Format(Resources.WarningShipmentsWithoutItems, shipments.Count),
                        Resources.TitleInformation, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    UpdateTotalInfo(0, 0, 0, 0, 0);
                    return;
                }

                var counter = 1;
                var finalList = finalDisplayList.Select(item => new
                {
                    Number = counter++,
                    item.Article,
                    item.Name,
                    item.Quantity,
                    Price = item.SalePrice.ToString("F2"),
                    Profit = item.Profit.ToString("F2"),
                    Total = item.Total.ToString("F2"),
                    item.Recipient,
                    item.Storekeeper,
                    item.Date
                }).ToList();

                dataGridViewHistory.DataSource = finalList;

                ConfigureRussianColumns();

                var totalItemsShipped = finalDisplayList
                        .Where(x => x.Recipient != "WriteOff" && x.Recipient != "Defect" && x.Quantity > 0)
                        .Sum(x => x.Quantity);

                var totalItemsWrittenOff = finalDisplayList
                    .Where(x => (x.Recipient == "WriteOff" || x.Recipient == "Defect") && x.Quantity < 0)
                    .Sum(x => -x.Quantity);

                var totalSum = finalDisplayList
                    .Where(x => x.Recipient != "WriteOff" && x.Recipient != "Defect" && x.Quantity > 0)
                    .Sum(x => x.Total);

                var totalProfit = finalDisplayList
                    .Where(x => x.Recipient != "WriteOff" && x.Recipient != "Defect")
                    .Sum(x => x.Profit);

                var totalLoss = finalDisplayList
                    .Where(x => x.Recipient == "WriteOff" || x.Recipient == "Defect")
                    .Sum(x => -x.Profit);

                UpdateTotalInfo(totalItemsShipped, totalItemsWrittenOff, totalSum, totalProfit, totalLoss);
            }
            catch (Exception ex)
            {
                logger.Error("Не удалось загрузить историю отгрузок", ex);
                MessageBox.Show(Resources.ErrorLoadShipmentHistory,
                    Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Настраивает заголовки колонок DataGridView на русском языке
        /// </summary>
        private void ConfigureRussianColumns()
        {
            if (dataGridViewHistory.Columns.Count == 0) return;

            if (dataGridViewHistory.Columns["Number"] != null)
                dataGridViewHistory.Columns["Number"].HeaderText = "№";

            if (dataGridViewHistory.Columns["Article"] != null)
                dataGridViewHistory.Columns["Article"].HeaderText = "Артикул";

            if (dataGridViewHistory.Columns["Name"] != null)
                dataGridViewHistory.Columns["Name"].HeaderText = "Название";

            if (dataGridViewHistory.Columns["Quantity"] != null)
                dataGridViewHistory.Columns["Quantity"].HeaderText = "Количество";

            if (dataGridViewHistory.Columns["Price"] != null)
                dataGridViewHistory.Columns["Price"].HeaderText = "Цена";

            if (dataGridViewHistory.Columns["Profit"] != null)
                dataGridViewHistory.Columns["Profit"].HeaderText = "Прибыль";

            if (dataGridViewHistory.Columns["Total"] != null)
                dataGridViewHistory.Columns["Total"].HeaderText = "Сумма";

            if (dataGridViewHistory.Columns["Recipient"] != null)
                dataGridViewHistory.Columns["Recipient"].HeaderText = "Получатель";

            if (dataGridViewHistory.Columns["Storekeeper"] != null)
                dataGridViewHistory.Columns["Storekeeper"].HeaderText = "Кладовщик";

            if (dataGridViewHistory.Columns["Date"] != null)
                dataGridViewHistory.Columns["Date"].HeaderText = "Дата";

            if (dataGridViewHistory.Columns["Number"] != null)
                dataGridViewHistory.Columns["Number"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            if (dataGridViewHistory.Columns["Quantity"] != null)
                dataGridViewHistory.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            if (dataGridViewHistory.Columns["Price"] != null)
                dataGridViewHistory.Columns["Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            if (dataGridViewHistory.Columns["Profit"] != null)
                dataGridViewHistory.Columns["Profit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            if (dataGridViewHistory.Columns["Total"] != null)
                dataGridViewHistory.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            if (dataGridViewHistory.Columns["Date"] != null)
                dataGridViewHistory.Columns["Date"].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";

            dataGridViewHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridViewHistory.CellFormatting += DataGridViewHistory_CellFormatting;
        }

        /// <summary>
        /// Форматирование ячеек - перевод английских значений на русский
        /// </summary>
        private void DataGridViewHistory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewHistory.Columns[e.ColumnIndex].Name == "Recipient" && e.Value != null)
            {
                string value = e.Value.ToString();
                if (value == "Shipment")
                    e.Value = "Отгрузка";
                else if (value == "WriteOff")
                    e.Value = "Списание";
                else if (value == "Defect")
                    e.Value = "Брак";
            }
        }

        /// <summary>
        /// Обновляет информацию в заголовке формы (итоги по отгрузкам)
        /// </summary>
        private void UpdateTotalInfo(int totalItemsShipped, int totalItemsWrittenOff, decimal totalSum, decimal totalProfit, decimal totalLoss)
        {
            Text = string.Format(Resources.ShipmentHistoryTitle,
                totalItemsShipped, totalItemsWrittenOff, totalSum, totalProfit, totalLoss);
        }

        /// <summary>
        /// Обработчик нажатия кнопки применения фильтра
        /// </summary>
        private void buttonApplyFilter_Click(object sender, EventArgs e)
        {
            LoadShipmentHistory();
        }
    }
}