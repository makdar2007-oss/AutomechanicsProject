using AutomechanicsProject.Classes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AutomechanicsProject.Formes
{
    public partial class CreateShipment : Form
    {
        private DateBase db;
        private List<ShipmentItem> shipmentItems;
        private decimal totalAmount;    
        public CreateShipment()
        {
            InitializeComponent();
            db = new DateBase();
            shipmentItems = new List<ShipmentItem>();
            totalAmount = 0;
        }
        private void CreateShipment_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }
        private void LoadProducts()
        {
            try
            {
                var products = db.Products
                    .Include(p => p.Category)
                    .Where(p => p.Balance > 0) 
                    .Select(p => new
                    {
                        p.Id,
                        DisplayName = $"{p.Article} - {p.Name} (в наличии: {p.Balance} {p.Unit})",
                        p.Article,
                        p.Name,
                        p.Price,
                        p.Balance,
                        p.Unit
                    })
                    .ToList();

                comboBoxProduct.DisplayMember = "DisplayName";
                comboBoxProduct.ValueMember = "Id";
                comboBoxProduct.DataSource = products;

                if (comboBoxProduct.Items.Count > 0)
                {
                    comboBoxProduct.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке товаров: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxProduct.SelectedItem == null)
                {
                    MessageBox.Show("Выберите товар из списка!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(textBoxUser.Text) || textBoxUser.Text == "Введите кому")
                {
                    MessageBox.Show("Введите ФИО получателя!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(textBoxUnit.Text) || textBoxUnit.Text == "Введите количество товара")
                {
                    MessageBox.Show("Введите количество товара!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(textBoxUnit.Text, out int quantity) || quantity <= 0)
                {
                    MessageBox.Show("Введите корректное количество (целое положительное число)!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var selectedProduct = (dynamic)comboBoxProduct.SelectedItem;
                Guid productId = selectedProduct.Id;
                string article = selectedProduct.Article;
                string productName = selectedProduct.Name;
                decimal price = selectedProduct.Price;
                int availableBalance = selectedProduct.Balance;
                string unit = selectedProduct.Unit;

                if (quantity > availableBalance)
                {
                    MessageBox.Show($"Недостаточно товара на складе!\nДоступно: {availableBalance} {unit}",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var existingItem = shipmentItems.FirstOrDefault(i => i.ProductId == productId);
                if (existingItem != null)
                {
                    var result = MessageBox.Show(
                        $"Товар \"{productName}\" уже добавлен в список.\n" +
                        $"Текущее количество: {existingItem.Quantity} {unit}\n\n" +
                        "Заменить количество новым значением?",
                        "Товар уже в списке",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        int totalQuantity = quantity;
                        if (totalQuantity > availableBalance)
                        {
                            MessageBox.Show($"Недостаточно товара на складе!\nДоступно: {availableBalance} {unit}",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        existingItem.Quantity = quantity;
                    }
                }
                else
                {
                    var newItem = new ShipmentItem
                    {
                        ProductId = productId,
                        ProductName = productName,
                        Article = article,
                        Quantity = quantity,
                        Price = price
                    };
                    shipmentItems.Add(newItem);
                }

                RefreshShipmentList();
                textBoxUnit.Text = "Введите количество";
                textBoxUnit.ForeColor = Color.Gray;
                comboBoxProduct.SelectedIndex = -1;

                MessageBox.Show("Товар добавлен в список отгрузки!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении товара: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void RefreshShipmentList()
        {
            totalAmount = 0;

            var displayList = shipmentItems.Select(item =>
            {
                decimal itemTotal = item.Quantity * item.Price;
                totalAmount += itemTotal;

                return new
                {
                    item.Article,
                    item.ProductName,
                    item.Quantity,
                    item.Price,
                    Total = itemTotal,
                    Recipient = textBoxUser.Text
                };
            }).ToList();

            dataGridViewShipment.DataSource = null;
            dataGridViewShipment.DataSource = displayList;

            this.Text = $"Отгрузка - Общая сумма: {totalAmount:C}";
        }
        private void textBoxUser_Enter(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb.Text == "Введите кому")
            {
                tb.Text = string.Empty;
                tb.ForeColor = Color.Black;
            }
        }
        private void textBoxUser_Leave(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "Введите кому";
                tb.ForeColor = Color.Gray;
            }
        }

        private void textBoxUnit_Leave(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "Введите количество товара";
                tb.ForeColor = Color.Gray;
            }
        }

        private void textBoxUnit_Enter(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb.Text == "Введите количество товара")
            {
                tb.Text = string.Empty;
                tb.ForeColor = Color.Black;
            }
        }
        private void ButtonShipment_Click(object sender, EventArgs e)
        {
            if (shipmentItems.Count == 0)
            {
                MessageBox.Show("Добавьте товары в список отгрузки!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string recipientName = textBoxUser.Text.Trim();
            if (string.IsNullOrWhiteSpace(recipientName) || recipientName == "Введите кому")
            {
                MessageBox.Show("Введите ФИО получателя!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var confirmResult = MessageBox.Show(
                $"Подтвердить отгрузку?\n\n" +
                $"Получатель: {recipientName}\n" +
                $"Количество позиций: {shipmentItems.Count}\n" +
                $"Общая сумма: {totalAmount:C}",
                "Подтверждение отгрузки",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult != DialogResult.Yes)
            {
                return;
            }
            try
            {
                var recipient = db.Users.FirstOrDefault(u => u.FullName == recipientName);
                Guid recipientId;

                if (recipient == null)
                {
                    var newRecipient = new User
                    {
                        Id = Guid.NewGuid(),
                        Surname = recipientName,
                        Name = "Внешний",
                        Login = $"external_{DateTime.Now.Ticks}",
                        Password = "external",
                        RoleId = db.Roles.FirstOrDefault(r => r.Position == "Кладовщик")?.Id ?? Guid.Empty
                    };
                    db.Users.Add(newRecipient);
                    db.SaveChanges();
                    recipientId = newRecipient.Id;
                }
                else
                {
                    recipientId = recipient.Id;
                }
                var shipment = new Shipment
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    UserId = recipientId,
                    CreatedByUserId = Program.CurrentUser?.Id ?? Guid.Empty,
                    TotalAmount = totalAmount
                };
                db.Shipments.Add(shipment);
                foreach (var item in shipmentItems)
                {
                    db.ShipmentItems.Add(new ShipmentItem
                    {
                        Id = Guid.NewGuid(),
                        ShipmentId = shipment.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = item.Price,
                        ProductName = item.ProductName,
                        Article = item.Article
                    });
                    var product = db.Products.Find(item.ProductId);
                    if (product != null)
                    {
                        product.Balance -= item.Quantity;
                    }
                }
                db.SaveChanges();
                MessageBox.Show($"Отгрузка успешно оформлена!\n\n" +
                      $"Получатель: {recipientName}\n" +
                      $"Количество позиций: {shipmentItems.Count}\n" +
                      $"Общая сумма: {totalAmount:C}",
                      "Успех",
                      MessageBoxButtons.OK,
                      MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при оформлении отгрузки: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            if (shipmentItems.Count > 0)
            {
                var result = MessageBox.Show(
                    "У вас есть добавленные товары в списке отгрузки.\n" +
                    "Вы действительно хотите отменить создание отгрузки?",
                    "Подтверждение отмены",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}
