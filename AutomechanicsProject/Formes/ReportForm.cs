using AutomechanicsProject.Classes;
using AutomechanicsProject.Properties;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutomechanicsProject.Formes
{
    /// <summary>
    /// Форма отчета по отгрузкам и списаниям
    /// </summary>
    public partial class ReportForm : Form
    {
        private readonly DateBase _db;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Инициализирует новый экземпляр формы отчета
        /// </summary>
        public ReportForm(DateBase database)
        {
            InitializeComponent();
            _db = database ?? throw new ArgumentNullException(nameof(database));
        }

        /// <summary>
        /// Обработчик события загрузки формы
        /// Устанавливает начальные даты (последний месяц) и загружает отчет
        /// </summary>
        private void ReportForm_Load(object sender, EventArgs e)
        {
            dateTimePickerFrom.Value = DateTime.Now.AddMonths(-1);
            dateTimePickerTo.Value = DateTime.Now;
            LoadReport();
        }

        /// <summary>
        /// Загружает данные отчета за выбранный период из базы данных
        /// </summary>
        private void LoadReport()
        {
            try
            {
                DateTime startDate = dateTimePickerFrom.Value.Date;
                DateTime endDate = dateTimePickerTo.Value.Date.AddDays(1);

                if (startDate > endDate)
                {
                    MessageBox.Show(Resources.WarningNoDataForPeriod,
                        Resources.TitleWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var shipments = _db.Shipments
                    .Include(s => s.User)
                    .Include(s => s.CreatedByUser)
                    .Include(s => s.Items)
                    .Where(s => s.Date.Date >= startDate && s.Date.Date < endDate)
                    .OrderByDescending(s => s.Date)
                    .ToList();

                var supplies = _db.Supplies
                    .Include(s => s.User)
                    .Include(s => s.Positions)
                    .Where(s => s.DateCreated.Date >= startDate && s.DateCreated.Date < endDate)
                    .ToList();

                if (shipments.Count == 0 && supplies.Count == 0)
                {
                    dataGridViewReport.DataSource = null;
                    MessageBox.Show(Resources.InfoNoDataForPeriod, Resources.TitleInformation,
                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UpdateTotalInfo(0, 0);
                    return;
                }

                var shipmentDisplayList = shipments
                    .Where(s => s.Items != null && s.Items.Any())
                    .SelectMany(s => s.Items.Select(item => new
                    {
                        Article = item.Article ?? "Н/Д",
                        Name = item.ProductName ?? "Н/Д",
                        Quantity = (item.PurchasePrice == 0) ? -Math.Abs(item.Quantity) : Math.Abs(item.Quantity),
                        Price = (s.ShipmentType == "Shipment")
                            ? item.PurchasePrice
                            : 0m,
                        Profit = (s.ShipmentType == "Shipment")
                            ? (item.PurchasePrice - item.Price) * item.Quantity
                            : -(item.Price * Math.Abs(item.Quantity)),
                        Total = (s.ShipmentType == "Shipment")
                            ? item.PurchasePrice * item.Quantity
                            : 0m,
                        Recipient = (s.ShipmentType == "Shipment")
                            ? (s.User?.CompanyName ?? "Не указан")
                            : (s.ShipmentType == "Defect") ? "Defect" : "WriteOff",
                        Storekeeper = s.CreatedByUser?.FullName ?? "Не указан",
                        Date = s.Date.ToString("dd.MM.yyyy HH:mm"),
                        Type = "Shipment"
                    }))
                    .ToList();

                var supplyDisplayList = supplies
                    .Where(s => s.Positions != null && s.Positions.Any())
                    .SelectMany(s => s.Positions.Select(item => new
                    {
                        Article = item.Article ?? "Н/Д",
                        Name = item.ProductName ?? "Н/Д",
                        Quantity = item.Quantity,  
                        Price = item.Price,
                        Profit = 0m,  
                        Total = item.Price * item.Quantity,
                        Recipient = item.SupplierName ?? "Не указан",
                        Storekeeper = s.User?.FullName ?? "Не указан",
                        Date = s.DateCreated.ToString("dd.MM.yyyy HH:mm"),
                        Type = "Поставка"
                    }))
                    .ToList();

                var displayList = shipmentDisplayList.Concat(supplyDisplayList).ToList();

                if (!displayList.Any())
                {
                    dataGridViewReport.DataSource = null;
                    MessageBox.Show(Resources.InfoNoPositionsToDisplay, Resources.TitleInformation,
                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    UpdateTotalInfo(0, 0);
                    return;
                }

                var finalList = displayList
                    .OrderByDescending(x => x.Date)
                    .Select((item, index) => new
                    {
                        Number = index + 1,
                        item.Article,
                        item.Name,
                        item.Quantity,
                        Price = item.Price.ToString("F2"),
                        Profit = item.Profit.ToString("F2"),
                        Total = item.Total.ToString("F2"),
                        item.Recipient,
                        item.Storekeeper,
                        item.Date
                    })
                    .ToList();

                dataGridViewReport.DataSource = finalList;

                ConfigureRussianColumns();

                var totalSum = displayList
                    .Where(x => x.Type == "Shipment" && x.Quantity > 0)
                    .Sum(x => x.Total);

                var totalProfit = displayList
                    .Where(x => x.Type == "Shipment")
                    .Sum(x => x.Profit);

                UpdateTotalInfo(totalSum, totalProfit);
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка загрузки отчета", ex);
                MessageBox.Show(Resources.ErrorLoadReportFormat,
                    Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Настраивает заголовки колонок DataGridView на русском языке
        /// </summary>
        private void ConfigureRussianColumns()
        {
            if (dataGridViewReport.Columns.Count == 0) return;

            if (dataGridViewReport.Columns["Number"] != null)
                dataGridViewReport.Columns["Number"].HeaderText = "№";

            if (dataGridViewReport.Columns["Article"] != null)
                dataGridViewReport.Columns["Article"].HeaderText = "Артикул";

            if (dataGridViewReport.Columns["Name"] != null)
                dataGridViewReport.Columns["Name"].HeaderText = "Название";

            if (dataGridViewReport.Columns["Quantity"] != null)
                dataGridViewReport.Columns["Quantity"].HeaderText = "Количество";

            if (dataGridViewReport.Columns["Price"] != null)
                dataGridViewReport.Columns["Price"].HeaderText = "Цена";

            if (dataGridViewReport.Columns["Profit"] != null)
                dataGridViewReport.Columns["Profit"].HeaderText = "Прибыль";

            if (dataGridViewReport.Columns["Total"] != null)
                dataGridViewReport.Columns["Total"].HeaderText = "Сумма";

            if (dataGridViewReport.Columns["Recipient"] != null)
                dataGridViewReport.Columns["Recipient"].HeaderText = "Получатель";

            if (dataGridViewReport.Columns["Storekeeper"] != null)
                dataGridViewReport.Columns["Storekeeper"].HeaderText = "Кладовщик";

            if (dataGridViewReport.Columns["Date"] != null)
                dataGridViewReport.Columns["Date"].HeaderText = "Дата";

            if (dataGridViewReport.Columns["Number"] != null)
                dataGridViewReport.Columns["Number"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            if (dataGridViewReport.Columns["Quantity"] != null)
                dataGridViewReport.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            if (dataGridViewReport.Columns["Price"] != null)
                dataGridViewReport.Columns["Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            if (dataGridViewReport.Columns["Profit"] != null)
                dataGridViewReport.Columns["Profit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            if (dataGridViewReport.Columns["Total"] != null)
                dataGridViewReport.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            if (dataGridViewReport.Columns["Date"] != null)
                dataGridViewReport.Columns["Date"].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";

            dataGridViewReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridViewReport.CellFormatting += DataGridViewReport_CellFormatting;

            dataGridViewReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        /// <summary>
        /// Форматирование ячеек - перевод английских значений на русский
        /// </summary>
        private void DataGridViewReport_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewReport.Columns[e.ColumnIndex].Name == "Recipient" && e.Value != null)
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
        /// Обновляет итоговую информацию в нижней панели формы
        /// </summary>
        private void UpdateTotalInfo(decimal totalSum, decimal totalProfit)
        {
            labelTotalAmountValue.Text = string.Format("{0:N2} руб.", totalSum);
            labelProfitValue.Text = string.Format("{0:N2} руб.", totalProfit);
        }

        /// <summary>
        /// Экспортирует данные отчета в CSV файл
        /// </summary>
        private void ExportToCsv()
        {
            try
            {
                if (dataGridViewReport.Rows.Count == 0)
                {
                    MessageBox.Show(Resources.ErrorNoDataToExport, Resources.TitleWarning,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "CSV файлы (*.csv)|*.csv|Все файлы (*.*)|*.*";
                    saveFileDialog.DefaultExt = "csv";
                    saveFileDialog.FileName = string.Format("Отчет_{0}", DateTime.Now.ToString("yyyyMMdd_HHmmss"));
                    saveFileDialog.Title = Resources.SaveCsvFileTitle;
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        StringBuilder csvContent = new StringBuilder();
                        csvContent.AppendLine("\uFEFF");

                        string[] headers = {
                            "№", "Артикул", "Название", "Количество", "Цена",
                            "Прибыль", "Сумма", "Получатель", "Кладовщик", "Дата"
                        };
                        csvContent.AppendLine(string.Join(";", headers));

                        foreach (DataGridViewRow row in dataGridViewReport.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                string[] rowData = new string[headers.Length];
                                for (int i = 0; i < headers.Length; i++)
                                {
                                    var cellValue = row.Cells[i].Value;
                                    var cellString = cellValue?.ToString() ?? "";

                                    if (cellString.Contains("\""))
                                    {
                                        cellString = cellString.Replace("\"", "\"\"");
                                    }
                                    if (cellString.Contains(";") || cellString.Contains("\n"))
                                    {
                                        cellString = $"\"{cellString}\"";
                                    }

                                    rowData[i] = cellString;
                                }
                                csvContent.AppendLine(string.Join(";", rowData));
                            }
                        }

                        File.WriteAllText(saveFileDialog.FileName, csvContent.ToString(), Encoding.UTF8);

                        MessageBox.Show(string.Format(Resources.SuccessExportFormat, saveFileDialog.FileName),
                            Resources.TitleSuccess, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка загрузки экспортируемого файла");
                MessageBox.Show(Resources.ErrorExportToCsvWithMessage,
                    Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик изменения даты "с"
        /// Автоматически обновляет отчет при изменении даты
        /// </summary>
        private void dateTimePickerFrom_ValueChanged(object sender, EventArgs e)
        {
            LoadReport();
        }

        /// <summary>
        /// Обработчик изменения даты "по"
        /// Автоматически обновляет отчет при изменении даты
        /// </summary>
        private void dateTimePickerTo_ValueChanged(object sender, EventArgs e)
        {
            LoadReport();
        }

        /// <summary>
        /// Обработчик нажатия кнопки экспорта отчета
        /// </summary>
        private void buttonExport_Click(object sender, EventArgs e)
        {
            ExportToCsv();
        }
    }
}