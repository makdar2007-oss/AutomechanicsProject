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

                var shipments = _db.Shipments
                    .Include(s => s.User)
                    .Include(s => s.CreatedByUser)
                    .Include(s => s.Items)
                    .Where(s => s.Date.Date >= startDate && s.Date.Date < endDate)
                    .OrderByDescending(s => s.Date)
                    .ToList();

                var supplies = _db.Supplies
                    .Include(s => s.Supplier)
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
                        Артикул = item.Article ?? "Н/Д",
                        Название = item.ProductName ?? "Н/Д",
                        Количество = (item.PurchasePrice == 0) ? -Math.Abs(item.Quantity) : Math.Abs(item.Quantity),
                        Цена = (s.ShipmentType == "Отгрузка")
                            ? item.PurchasePrice
                            : 0m,
                        Прибыль = (s.ShipmentType == "Отгрузка")
                            ? (item.PurchasePrice - item.Price) * item.Quantity
                            : -(item.Price * Math.Abs(item.Quantity)),
                        Сумма = (s.ShipmentType == "Отгрузка")
                            ? item.PurchasePrice * item.Quantity
                            : 0m,
                        Получатель = (s.ShipmentType == "Отгрузка")
                            ? (s.User?.CompanyName ?? "Не указан")
                            : (s.ShipmentType == "Брак") ? "Брак" : "Списание",
                        Кладовщик = s.CreatedByUser?.FullName ?? "Не указан",
                        Дата = s.Date.ToString("dd.MM.yyyy HH:mm"),
                        Тип = "Отгрузка"
                    }))
                    .ToList();

                var supplyDisplayList = supplies
                    .Where(s => s.Positions != null && s.Positions.Any())
                    .SelectMany(s => s.Positions.Select(item => new
                    {
                        Артикул = item.Article ?? "Н/Д",
                        Название = item.ProductName ?? "Н/Д",
                        Количество = item.Quantity,  
                        Цена = item.Price,
                        Прибыль = 0m,  
                        Сумма = item.Price * item.Quantity,
                        Получатель = item.SupplierName ?? "Не указан",
                        Кладовщик = s.User?.FullName ?? "Не указан",
                        Дата = s.DateCreated.ToString("dd.MM.yyyy HH:mm"),
                        Тип = "Поставка"
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
                    .OrderByDescending(x => x.Дата)
                    .Select((item, index) => new
                    {
                        Номер = index + 1,
                        item.Артикул,
                        item.Название,
                        item.Количество,
                        Цена = item.Цена.ToString("F2"),
                        Прибыль = item.Прибыль.ToString("F2"),
                        Сумма = item.Сумма.ToString("F2"),
                        item.Получатель,
                        item.Кладовщик,
                        item.Дата
                    })
                    .ToList();

                dataGridViewReport.DataSource = finalList;

                var totalSum = displayList
                    .Where(x => x.Тип == "Отгрузка" && x.Количество > 0)
                    .Sum(x => x.Сумма);

                var totalProfit = displayList
                    .Where(x => x.Тип == "Отгрузка")
                    .Sum(x => x.Прибыль);

                UpdateTotalInfo(totalSum, totalProfit);
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка загрузки отчета");
                MessageBox.Show(string.Format(Resources.ErrorLoadReportFormat, ex.Message),
                    Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        /// <summary>
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
                MessageBox.Show(string.Format(Resources.ErrorExportToCsvWithMessage, ex.Message),
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