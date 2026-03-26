using AutomechanicsProject.Classes;
using AutomechanicsProject.Properties;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;

namespace AutomechanicsProject.Formes
{
    /// <summary>
    /// Форма для просмотра истории отгрузок товаров
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
        /// Обработчик события загрузки формы
        /// </summary>
        private void ShipmentHistoryForm_Load(object sender, EventArgs e)
        {
            LoadShipmentHistory();
        }
        /// <summary>
        /// Загружает историю отгрузок из базы данных и отображает в таблице
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

                if (shipments.Count == 0)
                {
                    dataGridViewHistory.DataSource = null;
                    MessageBox.Show(Resources.InfoHistoryEmpty, Resources.TitleInformation,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                var displayList = shipments
                    .Where(s => s.Items != null && s.Items.Any())
                    .SelectMany(s => s.Items.Select(item => new
                    {
                        Артикул = item.Article ?? "Н/Д",
                        Название = item.ProductName ?? "Н/Д",
                        Количество = item.Quantity,
                        Цена = item.Price,
                        Сумма = item.Quantity * item.Price,
                        Получатель = s.User?.CompanyName ?? "Не указан",
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
                    return;
                }
                var counter = 1;
                var finalList = displayList.Select(item => new
                {
                    Номер = counter++,
                    item.Артикул,
                    item.Название,
                    item.Количество,
                    ЦенаРуб = item.Цена.ToString("F2"),
                    СуммаРуб = item.Сумма.ToString("F2"),
                    item.Получатель,
                    item.Кладовщик,
                    item.Дата
                }).ToList();

                dataGridViewHistory.DataSource = finalList;

                var totalItems = displayList.Count;
                var totalSum = displayList.Sum(x => x.Сумма);
                Text = $"История отгрузок - Всего позиций: {totalItems}, общая сумма: {totalSum:C2}";
            }
            catch (Exception ex)
            {
                Program.LogError("Ошибка при загрузке истории отгрузок", ex);
                MessageBox.Show(string.Format(Resources.ErrorLoadShipmentHistory, ex.Message),
                    Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}