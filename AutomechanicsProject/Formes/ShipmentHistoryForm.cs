using AutomechanicsProject.Classes;
using AutomechanicsProject.Properties;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;

namespace AutomechanicsProject.Formes
{
    /// <summary>
    /// Форма для просмотра истории отгрузок
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
            db = new DateBase();
        }
        /// <summary>
        /// Обработчик загрузки формы
        /// </summary>
        private void ShipmentHistoryForm_Load(object sender, EventArgs e)
        {
            LoadShipmentHistory();
        }
        /// <summary>
        /// Загружает историю отгрузок из базы данных и отображает в DataGridView
        /// </summary>
        private void LoadShipmentHistory()
        {
            try
            {
                var shipments = db.Shipments
                    .Include(s => s.User)          
                    .Include(s => s.CreatedByUser) 
                    .Include(s => s.Items)        
                    .OrderByDescending(s => s.Date) 
                    .ToList();

                if (shipments.Count == 0 || !shipments.Any(s => s.Items.Any()))
                {
                    dataGridViewHistory.DataSource = null;
                    MessageBox.Show("История отгрузок пуста", Resources.TitleInformation,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                foreach (var shipment in shipments)
                {
                    Program.LogInfo($"Отгрузка ID: {shipment.Id}, User: {shipment.User?.CompanyName ?? "NULL"}, CreatedBy: {shipment.CreatedByUser?.FullName ?? "NULL"}");
                }

                var displayList = shipments
                    .Where(s => s.Items.Any()) 
                    .SelectMany(s => s.Items.Select((item, index) => new
                    {
                        ShipmentId = s.Id,
                        ItemIndex = index + 1,
                        item.Article,
                        item.ProductName,
                        item.Quantity,
                        item.Price,
                        Total = item.Quantity * item.Price,
                        Recipient = s.User?.CompanyName ?? "Не указан",
                        Storekeeper = s.CreatedByUser?.FullName ?? "Не указан",
                        s.Date
                    }))
                    .ToList();

                var counter = 1;
                var finalList = displayList.Select(item => new
                {
                    Number = counter++,
                    item.Article,
                    item.ProductName,
                    item.Quantity,
                    item.Price,
                    item.Total,
                    item.Recipient,
                    item.Storekeeper,
                    Date = item.Date.ToString("dd.MM.yyyy HH:mm")
                }).ToList();
                dataGridViewHistory.DataSource = finalList;
                var totalShipments = shipments.Count(s => s.Items.Any());
                var totalItems = displayList.Count;
                var totalSum = displayList.Sum(x => x.Total);
                Text = $"История отгрузок - Всего отгрузок: {totalShipments}, позиций: {totalItems}, сумма: {totalSum:C}";
            }
            catch (Exception ex)
            {
                Program.LogError("Ошибка при загрузке истории отгрузок", ex);
                MessageBox.Show($"Не удалось загрузить историю отгрузок: {ex.Message}",
                    Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Обработчик нажатия кнопки обновления
        /// Перезагружает историю отгрузок
        /// </summary>
        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadShipmentHistory();
        }
        /// <summary>
        /// Освобождает ресурсы при закрытии формы
        /// </summary>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            db?.Dispose();
            base.OnFormClosed(e);
        }
    }
}