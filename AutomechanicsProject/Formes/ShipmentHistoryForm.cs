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
                        Артикул = item.Article ?? "Н/Д",
                        Название = item.ProductName ?? "Н/Д",
                        Количество = (s.ShipmentType == "Отгрузка") ? item.Quantity : -item.Quantity,
                        ЦенаЗакупки = item.Price,
                        ЦенаПродажи = item.PurchasePrice,
                        Прибыль = (s.ShipmentType == "Списание" || s.ShipmentType == "Брак")
                        ? -(Math.Abs(item.Price) * Math.Abs(item.Quantity))  
                        : (item.PurchasePrice - item.Price) * item.Quantity,
                        Сумма = (s.ShipmentType == "Списание" || s.ShipmentType == "Брак")
                            ? 0m
                            : item.PurchasePrice * item.Quantity,
                        Получатель = (s.ShipmentType == "Списание") ? "Списание"
                            : (s.ShipmentType == "Брак") ? "Брак"
                            : (s.User?.CompanyName ?? "Не указан"),
                        Кладовщик = s.CreatedByUser?.FullName ?? "Не указан",
                        Дата = s.Date.ToString("dd.MM.yyyy HH:mm")
                    }))
                    .ToList();

                var finalDisplayList = displayList.Select(item => {
                    bool isWriteOff = (item.Получатель == "Списание" || item.Получатель == "Брак");

                    return new
                    {
                        item.Артикул,
                        item.Название,
                        Количество = isWriteOff ? -item.Количество : item.Количество,
                        ЦенаЗакупки = item.ЦенаЗакупки,
                        ЦенаПродажи = isWriteOff ? 0m : item.ЦенаПродажи,
                        Прибыль = item.Прибыль,
                        Сумма = isWriteOff ? 0m : item.Сумма,
                        item.Получатель,
                        item.Кладовщик,
                        item.Дата
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
                    Номер = counter++,
                    item.Артикул,
                    item.Название,
                    Количество = item.Количество,
                    Цена = item.ЦенаПродажи.ToString("F2"),
                    Прибыль = item.Прибыль.ToString("F2"),
                    Сумма = item.Сумма.ToString("F2"),
                    item.Получатель,
                    item.Кладовщик,
                    item.Дата
                }).ToList();

                dataGridViewHistory.DataSource = finalList;

                var totalItemsShipped = finalDisplayList
                    .Where(x => x.Получатель != "Списание" && x.Получатель != "Брак" && x.Количество > 0)
                    .Sum(x => x.Количество);

                var totalItemsWrittenOff = finalDisplayList
                    .Where(x => (x.Получатель == "Списание" || x.Получатель == "Брак") && x.Количество < 0)
                    .Sum(x => -x.Количество);

                var totalSum = finalDisplayList
                    .Where(x => x.Получатель != "Списание" && x.Получатель != "Брак" && x.Количество > 0)
                    .Sum(x => x.Сумма);

                var totalProfit = finalDisplayList
                    .Where(x => x.Получатель != "Списание" && x.Получатель != "Брак")
                    .Sum(x => x.Прибыль);

                var totalLoss = finalDisplayList
                    .Where(x => x.Получатель == "Списание" || x.Получатель == "Брак")
                    .Sum(x => -x.Прибыль);

                UpdateTotalInfo(totalItemsShipped, totalItemsWrittenOff, totalSum, totalProfit, totalLoss);
            }
            catch (Exception ex)
            {
                logger.Error("Не удалось загрузить историю отгрузок");
                MessageBox.Show(string.Format(Resources.ErrorLoadShipmentHistory, ex.Message),
                    Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
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