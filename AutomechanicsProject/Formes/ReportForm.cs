using AutomechanicsProject.Classes;
using AutomechanicsProject.Enum;
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
            dateTimePickerFrom.Value = MoscowTime.Now.AddMonths(-1);
            dateTimePickerTo.Value = MoscowTime.Now;
            LoadReport();
        }

        /// <summary>
        /// Загружает данные отчета за выбранный период из базы данных
        /// </summary>
        private void LoadReport()
        {
            try
            {
                DateTime selectedFromDate = dateTimePickerFrom.Value;
                DateTime selectedToDate = dateTimePickerTo.Value;

                DateTime moscowStartDate = MoscowTime.ConvertToMoscow(selectedFromDate).Date;
                DateTime moscowEndDate = MoscowTime.ConvertToMoscow(selectedToDate).Date.AddDays(1);


                if (moscowStartDate > moscowEndDate)
                {
                    MessageBox.Show(Resources.WarningNoDataForPeriod,
                        Resources.TitleWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var shipments = _db.Shipments
                    .Include(s => s.User)
                    .Include(s => s.CreatedByUser)
                    .Include(s => s.Items)
                    .Where(s => s.Date >= moscowStartDate && s.Date < moscowEndDate).OrderByDescending(s => s.Date)
                    .ToList();

                var supplies = _db.Supplies
                    .Include(s => s.User)
                    .Include(s => s.Positions)
                    .Where(s => s.DateCreated >= moscowStartDate && s.DateCreated < moscowEndDate).ToList();

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
                        Article = item.Article ?? Resources.ND,
                        Name = item.ProductName ?? Resources.ND,
                        Quantity = (item.PurchasePrice == 0) ? -Math.Abs(item.Quantity) : Math.Abs(item.Quantity),
                        Price = (s.ShipmentType == ShipmentTypeEnum.Shipment.ToString())
                            ? item.PurchasePrice
                            : 0m,
                        Profit = (s.ShipmentType == ShipmentTypeEnum.Shipment.ToString())
                            ? (item.PurchasePrice - item.Price) * item.Quantity
                            : (s.ShipmentType == ShipmentTypeEnum.Defect.ToString())
                            ? -(item.PurchasePrice * Math.Abs(item.Quantity)) + (item.ScrapReturn)  
                            : -(item.PurchasePrice * Math.Abs(item.Quantity)),  
                        Total = (s.ShipmentType == ShipmentTypeEnum.Shipment.ToString())
                            ? item.PurchasePrice * item.Quantity
                            : 0m,
                        Recipient = (s.ShipmentType == ShipmentTypeEnum.Shipment.ToString())
                        ? (s.User?.CompanyName ?? Resources.NotListed)
                        : (s.ShipmentType == ShipmentTypeEnum.Defect.ToString())
                        ? Resources.ShipmentType_Defect
                        : Resources.ShipmentType_WriteOff,
                        Storekeeper = s.CreatedByUser?.FullName ?? Resources.NotListed,
                        Date = MoscowTime.Format(s.Date),
                        ScrapReturn = item.ScrapReturn,
                        Type = Resources.ShipmentType_Shipment
                    }))
                    .ToList();

                var supplyDisplayList = supplies
                    .Where(s => s.Positions != null && s.Positions.Any())
                    .SelectMany(s => s.Positions.Select(item => new
                    {
                        Article = item.Article ?? Resources.ND,
                        Name = item.ProductName ?? Resources.ND,
                        Quantity = item.Quantity,  
                        Price = item.Price,
                        Profit = 0m,  
                        Total = item.Price * item.Quantity,
                        Recipient = item.SupplierName ?? Resources.NotListed,
                        Storekeeper = s.User?.FullName ?? Resources.NotListed,
                        Date = MoscowTime.Format(s.DateCreated),
                        ScrapReturn = 0m,
                        Type = Resources.ShipmentType_Supply
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
                        item.Date,
                    })
                    .ToList();

                dataGridViewReport.DataSource = finalList;

                ConfigureRussianColumns();

                var totalSum = displayList
                    .Where(x => x.Type == Resources.ShipmentType_Shipment && x.Quantity > 0)
                    .Sum(x => x.Total);

                var totalProfit = displayList
                    .Where(x => x.Type == Resources.ShipmentType_Shipment)
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
        /// Настраивает заголовки колонок таблицы на русском языке
        /// </summary>
        private void ConfigureRussianColumns()
        {
            if (dataGridViewReport.Columns.Count == 0) return;

            if (dataGridViewReport.Columns["Number"] != null)
                dataGridViewReport.Columns["Number"].HeaderText = "№";

            if (dataGridViewReport.Columns["Article"] != null)
                dataGridViewReport.Columns["Article"].HeaderText = Resources.Report_Article;

            if (dataGridViewReport.Columns["Name"] != null)
                dataGridViewReport.Columns["Name"].HeaderText = Resources.Report_Name;

            if (dataGridViewReport.Columns["Quantity"] != null)
                dataGridViewReport.Columns["Quantity"].HeaderText = Resources.Report_Quantity;

            if (dataGridViewReport.Columns["Price"] != null)
                dataGridViewReport.Columns["Price"].HeaderText = Resources.Report_Price;

            if (dataGridViewReport.Columns["Profit"] != null)
                dataGridViewReport.Columns["Profit"].HeaderText = Resources.Report_Profit;

            if (dataGridViewReport.Columns["Total"] != null)
                dataGridViewReport.Columns["Total"].HeaderText = Resources.Report_Total;

            if (dataGridViewReport.Columns["Recipient"] != null)
                dataGridViewReport.Columns["Recipient"].HeaderText = Resources.Report_Recipient;

            if (dataGridViewReport.Columns["Storekeeper"] != null)
                dataGridViewReport.Columns["Storekeeper"].HeaderText = Resources.Report_Storekeeper;

            if (dataGridViewReport.Columns["Date"] != null)
                dataGridViewReport.Columns["Date"].HeaderText = Resources.Report_Date;

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
        /// Форматирование ячеек - перевод значений на русский
        /// </summary>
        private void DataGridViewReport_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewReport.Columns[e.ColumnIndex].Name == Resources.Report_Recipient && e.Value != null)
            {
                string value = e.Value.ToString();
                if (value == "Shipment")
                {
                    e.Value = Resources.ShipmentType_Shipment;
                }

                else if (value == "WriteOff")
                {
                    e.Value = Resources.ShipmentType_WriteOff;
                }

                else if (value == "Defect")
                {
                    e.Value = Resources.ShipmentType_Defect;
                }
            }
        }

        /// <summary>
        /// Обновляет итоговую информацию в нижней панели формы
        /// </summary>
        private void UpdateTotalInfo(decimal totalSum, decimal totalProfit)
        {
            labelTotalAmountValue.Text = string.Format(Resources.CurrencyFormat_Rub, totalSum);
            labelProfitValue.Text = string.Format(Resources.CurrencyFormat_Rub, totalProfit);
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
                    saveFileDialog.Filter = Resources.SaveFileFilter_CSV;
                    saveFileDialog.DefaultExt = Resources.SaveFileDefaultExt_CSV;
                    saveFileDialog.FileName = string.Format(
                        Resources.SaveFileFileNameTemplate_Report,
                        MoscowTime.Now.ToString(Resources.FileNameDateFormat));                   
                        saveFileDialog.Title = Resources.SaveCsvFileTitle;
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        StringBuilder csvContent = new StringBuilder();
                        csvContent.AppendLine("\uFEFF");

                        string[] headers = {
                            Resources.Report_ColumnNumber,
                            Resources.Report_ColumnArticle,
                            Resources.Report_ColumnName,
                            Resources.Report_ColumnQuantity,
                            Resources.Report_ColumnPrice,
                            Resources.Report_ColumnProfit,
                            Resources.Report_ColumnTotal,
                            Resources.Report_ColumnRecipient,
                            Resources.Report_ColumnStorekeeper,
                            Resources.Report_ColumnDate
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
                logger.Error("Ошибка загрузки экспортируемого файла", ex);
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