using AutomechanicsProject.Classes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AutomechanicsProject.Formes
{
    public partial class ShipmentHistoryForm : Form
    {
        private DateBase db;

        public ShipmentHistoryForm()
        {
            db = new DateBase();
            InitializeComponent();
        }
        private void ShipmentHistoryForm_Load(object sender, EventArgs e)
        {
            LoadShipmentHistory();
        }

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
                if (shipments.Count == 0)
                {
                    dataGridViewHistory.DataSource = null;
                    MessageBox.Show("История отгрузок пуста", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                var displayList = shipments.SelectMany(s => s.Items.Select((item, index) => new
                {
                    ShipmentId = s.Id,
                    ItemIndex = index + 1,
                    item.Article,
                    item.ProductName,
                    item.Quantity,
                    item.Price,
                    Total = item.Quantity * item.Price,
                    Recipient = s.User?.FullName ?? "Не указан",
                    Storekeeper = s.CreatedByUser?.FullName ?? "Не указан",
                    s.Date
                })).ToList();
                int counter = 1;
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
                int totalShipments = shipments.Count;
                int totalItems = displayList.Count;
                decimal totalSum = displayList.Sum(x => x.Total);
                this.Text = $"История отгрузок - Всего отгрузок: {totalShipments}, " +
                           $"позиций: {totalItems}, сумма: {totalSum:C}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке истории отгрузок: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadShipmentHistory();
        }
    }
}