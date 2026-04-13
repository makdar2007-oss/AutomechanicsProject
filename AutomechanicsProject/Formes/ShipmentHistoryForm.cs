using AutomechanicsProject.Classes;
using AutomechanicsProject.Properties;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text;
using AutomechanicsProject.Classes.Dtos;

namespace AutomechanicsProject.Formes
{
    /// <summary>
    /// Форма просмотра истории отгрузок и списаний
    /// </summary>
    public partial class ShipmentHistoryForm : Form
    {
        private readonly DateBase db;

        /// <summary>
        /// Инициализирует новый экземпляр формы истории отгрузок
        /// </summary>
        public ShipmentHistoryForm()
        {
            InitializeComponent();
            db = DbContextManager.GetContext();
            DbContextManager.AddReference();
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

                var shipments = db.Shipments
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
                        Количество = item.Quantity,
                        ЦенаЗакупки = item.Price,
                        ЦенаПродажи = item.Quantity >= 0 ? item.PurchasePrice : 0,
                        Прибыль = item.Quantity >= 0
                            ? (item.PurchasePrice - item.Price) * item.Quantity
                            : item.Price * item.Quantity,
                        Сумма = item.Quantity >= 0
                            ? item.PurchasePrice * item.Quantity
                            : 0,
                        Получатель = item.Quantity < 0 ? "Списание" : (s.User?.CompanyName ?? "Не указан"),
                        Кладовщик = s.CreatedByUser?.FullName ?? "Не указан",
                        Дата = s.Date.ToString("dd.MM.yyyy HH:mm")
                    }))
                    .OrderByDescending(x => x.Дата)
                    .ToList();

                if (!displayList.Any())
                {
                    dataGridViewHistory.DataSource = null;
                    MessageBox.Show(string.Format(Resources.WarningShipmentsWithoutItems, shipments.Count),
                        Resources.TitleInformation, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    UpdateTotalInfo(0, 0, 0, 0, 0);
                    return;
                }

                var counter = 1;
                var finalList = displayList.Select(item => new
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

                var totalItemsShipped = displayList.Where(x => x.Количество > 0).Sum(x => x.Количество);
                var totalItemsWrittenOff = displayList.Where(x => x.Количество < 0).Sum(x => -x.Количество);
                var totalSum = displayList.Where(x => x.Количество > 0).Sum(x => x.Сумма);
                var totalProfit = displayList.Where(x => x.Количество > 0).Sum(x => x.Прибыль);
                var totalLoss = displayList.Where(x => x.Количество < 0).Sum(x => -x.Прибыль);

                UpdateTotalInfo(totalItemsShipped, totalItemsWrittenOff, totalSum, totalProfit, totalLoss);
            }
            catch (Exception ex)
            {
                Program.LogError("Не удалось загрузить историю отгрузок.", ex);
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

        /// <summary>
        /// Освобождает ресурсы контекста базы данных при закрытии формы
        /// </summary>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            DbContextManager.ReleaseReference();
        }
    }
}