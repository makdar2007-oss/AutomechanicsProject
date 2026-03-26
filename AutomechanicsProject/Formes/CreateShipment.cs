using AutomechanicsProject.Classes;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Properties;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AutomechanicsProject.Formes
{
    /// <summary>
    /// Форма для создания новой отгрузки товаров
    /// </summary>
    public partial class CreateShipment : Form
    {
        private readonly DateBase db;
        private List<ShipmentItem> shipmentItems;
        private decimal totalAmount;
        private int totalItemsCount;
        /// <summary>
        /// Инициализирует новый экземпляр формы создания отгрузки
        /// </summary>
        public CreateShipment()
        {
            InitializeComponent();
            db = new DateBase();
            shipmentItems = new List<ShipmentItem>();
            totalAmount = 0;
            totalItemsCount = 0;

            TextBoxHelper.SetupWatermarkTextBox(textBoxUnit, Resources.ShipmentQuantityWatermark);
            comboBoxProduct.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxRecipient1.DropDownStyle = ComboBoxStyle.DropDownList;
            UpdateDisplay();
        }
        /// <summary>
        /// Обработчик загрузки формы
        /// </summary>
        private void CreateShipment_Load(object sender, EventArgs e)
        {
            LoadProducts();
            LoadRecipients();
        }
        /// <summary>
        /// Загружает товары, доступные для отгрузки 
        /// </summary>
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
                        DisplayName = $"{p.Article} - {p.Name} (остаток: {p.Balance} {p.Unit})",
                        p.Article,
                        p.Name,
                        p.Price,
                        p.Balance,
                        p.Unit
                    })
                    .OrderBy(p => p.Name)
                    .ToList();
                comboBoxProduct.DisplayMember = "DisplayName";
                comboBoxProduct.ValueMember = "Id";
                comboBoxProduct.DataSource = products;
                if (comboBoxProduct.Items.Count > 0)
                {
                    comboBoxProduct.SelectedIndex = -1;
                }
                if (products.Count == 0)
                {
                    MessageBox.Show(Resources.InfoNoProductsForShipment, Resources.TitleInformation,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Program.LogError("Ошибка при загрузке товаров в CreateShipment", ex);
                MessageBox.Show(Resources.ErrorLoadProducts, Resources.TitleError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Загружает список получателей 
        /// </summary>
        private void LoadRecipients()
        {
            try
            {
                var recipients = db.Addresses
                    .OrderBy(a => a.CompanyName)
                    .Select(a => new
                    {
                        a.Id,
                        CompanyName = a.CompanyName
                    })
                    .ToList();

                comboBoxRecipient1.DisplayMember = "CompanyName";
                comboBoxRecipient1.ValueMember = "Id";
                comboBoxRecipient1.DataSource = recipients;

                if (comboBoxRecipient1.Items.Count > 0)
                {
                    comboBoxRecipient1.SelectedIndex = -1;
                }

                buttonShipment.Enabled = recipients.Count > 0;

                if (recipients.Count == 0)
                {
                    MessageBox.Show(Resources.InfoNoRecipientsAvailable, Resources.TitleInformation,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Program.LogError("Ошибка при загрузке получателей", ex);
                MessageBox.Show(Resources.ErrorLoadRecipients, Resources.TitleError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Обработчик нажатия кнопки Добавить в список
        /// </summary>
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (TryAddProductToShipment())
            {
                RefreshShipmentList();
                ClearAddFields();
                MessageBox.Show(Resources.SuccessProductAddedToList, Resources.TitleSuccess,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        /// <summary>
        /// Пытается добавить товар в список отгрузки
        /// </summary>
        private bool TryAddProductToShipment()
        {
            if (comboBoxProduct.SelectedItem == null)
            {
                MessageBox.Show(Resources.ErrorSelectProduct, Resources.TitleWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxProduct.Focus();
                return false;
            }

            if (comboBoxRecipient1.SelectedItem == null)
            {
                MessageBox.Show(Resources.ErrorSelectRecipient, Resources.TitleWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxRecipient1.Focus();
                return false;
            }

            if (!Validation.ValidateQuantity(textBoxUnit.Text, out var quantity) || quantity <= 0)
            {
                MessageBox.Show(Resources.ErrorEnterCorrectQuantity, Resources.TitleWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxUnit.Focus();
                return false;
            }

            dynamic selectedProduct = comboBoxProduct.SelectedItem;
            if (quantity > selectedProduct.Balance)
            {
                MessageBox.Show(string.Format(Resources.ErrorInsufficientStockWithDetails, selectedProduct.Balance, selectedProduct.Unit),
                    Resources.TitleWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxUnit.Focus();
                return false;
            }
            return AddOrUpdateShipmentItem(selectedProduct, quantity);
        }
        /// <summary>
        /// Добавляет или обновляет позицию в списке отгрузки
        /// </summary>
        private bool AddOrUpdateShipmentItem(dynamic selectedProduct, int quantity)
        {
            var productId = (Guid)selectedProduct.Id;
            var existingItem = shipmentItems.FirstOrDefault(i => i.ProductId == productId);
            if (existingItem != null)
            {
                var result = MessageBox.Show(
                    string.Format(Resources.ProductAlreadyInList, selectedProduct.Name, existingItem.Quantity, selectedProduct.Unit),
                    Resources.TitleProductAlreadyInList,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    existingItem.Quantity = quantity;
                    return true;
                }
                return false;
            }
            shipmentItems.Add(new ShipmentItem
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                ProductName = selectedProduct.Name,
                Article = selectedProduct.Article,
                Quantity = quantity,
                Price = selectedProduct.Price
            });
            return true;
        }
        /// <summary>
        /// Очищает поля добавления товара
        /// </summary>
        private void ClearAddFields()
        {
            textBoxUnit.Text = Resources.ShipmentQuantityWatermark;
            textBoxUnit.ForeColor = Color.Gray;
            if (comboBoxProduct.Items.Count > 0)
            {
                comboBoxProduct.SelectedIndex = -1;
            }
        }
        /// <summary>
        /// Обновляет список товаров в отгрузке и отображает итоговую информацию
        /// </summary>
        private void RefreshShipmentList()
        {
            totalAmount = 0;
            totalItemsCount = 0;
            var recipientName = "Не выбран";
            if (comboBoxRecipient1.SelectedItem != null)
            {
                dynamic selectedRecipient = comboBoxRecipient1.SelectedItem;
                recipientName = selectedRecipient.CompanyName;
            }
            var displayList = shipmentItems.Select(item =>
            {
                var itemTotal = item.Quantity * item.Price;
                totalAmount += itemTotal;
                totalItemsCount += item.Quantity;

                return new
                {
                    Артикул = item.Article,
                    Название = item.ProductName,
                    Колво = item.Quantity,
                    Цена = item.Price,
                    Сумма = itemTotal,
                    Кому = recipientName
                };
            }).ToList();

            dataGridViewShipment.DataSource = displayList;
            UpdateDisplay();
        }
        /// <summary>
        /// Обновляет отображение итоговой суммы
        /// </summary>
        private void UpdateDisplay()
        {
            if (labelTotalValue != null)
            {
                labelTotalValue.Text = totalAmount.ToString("F2");
            }
            Text = $"Формирование отгрузки - Общая сумма: {totalAmount:F2}";
        }
        /// <summary>
        /// Проверяет, выбран ли получатель
        /// </summary>
        private bool IsRecipientSelected()
        {
            if (comboBoxRecipient1.SelectedItem == null)
            {
                MessageBox.Show(Resources.ErrorSelectRecipient, Resources.TitleWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxRecipient1.Focus();
                return false;
            }
            return true;
        }
        /// <summary>
        /// Обработчик нажатия кнопки Подтвердить отгрузку
        /// </summary>
        private void ButtonShipment_Click(object sender, EventArgs e)
        {
            if (shipmentItems.Count == 0)
            {
                MessageBox.Show(Resources.ErrorNoItemsInShipment, Resources.TitleWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsRecipientSelected()) return;

            dynamic selectedRecipient = comboBoxRecipient1.SelectedItem;
            var recipientName = selectedRecipient.CompanyName;
            var recipientId = (Guid)selectedRecipient.Id;

            var confirmResult = MessageBox.Show(
                string.Format(Resources.ConfirmShipment, recipientName, shipmentItems.Count, totalAmount),
                Resources.TitleConfirmation,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult != DialogResult.Yes) return;

            try
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    var shipment = new Shipment
                    {
                        Id = Guid.NewGuid(),
                        Date = DateTime.Now,
                        UserId = recipientId,
                        CreatedByUserId = Program.CurrentUser?.Id ?? Guid.Empty,
                        TotalAmount = totalAmount
                    };

                    db.Shipments.Add(shipment);
                    db.SaveChanges();
                    foreach (var item in shipmentItems)
                    {
                        var shipmentItem = new ShipmentItem
                        {
                            Id = Guid.NewGuid(),
                            ShipmentId = shipment.Id,
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            Price = item.Price,
                            ProductName = item.ProductName,
                            Article = item.Article
                        };

                        db.ShipmentItems.Add(shipmentItem);
                        var product = db.Products.Find(item.ProductId);
                        if (product != null)
                        {
                            product.Balance -= item.Quantity;
                        }
                    }

                    db.SaveChanges();
                    transaction.Commit();

                    Program.LogInfo($"Отгрузка оформлена: получатель {recipientName}, сумма {totalAmount:C2}, позиций: {shipmentItems.Count}");

                    MessageBox.Show(string.Format(Resources.SuccessShipmentCreated, recipientName, shipmentItems.Count, totalAmount),
                        Resources.TitleSuccess, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (Exception ex)
            {
                Program.LogError("Ошибка при оформлении отгрузки", ex);
                MessageBox.Show(string.Format(Resources.ErrorCreateShipment, ex.Message),
                    Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Обработчик нажатия кнопки Отмена
        /// </summary>
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            if (shipmentItems.Count > 0)
            {
                var result = MessageBox.Show(
                    Resources.ConfirmCancelShipment,
                    Resources.TitleConfirmation,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes) return;
            }

            DialogResult = DialogResult.Cancel;
            Close();
        }
        /// <summary>
        /// Обработчик двойного клика по списку отгрузки - удаление товара
        /// </summary>
        private void DataGridViewShipment_DoubleClick(object sender, EventArgs e)
        {
            RemoveSelectedItem();
        }
        /// <summary>
        /// Удаляет выбранный товар из списка отгрузки
        /// </summary>
        private void RemoveSelectedItem()
        {
            if (dataGridViewShipment.CurrentRow?.DataBoundItem == null) return;

            dynamic selectedItem = dataGridViewShipment.CurrentRow.DataBoundItem;
            var productName = (string)selectedItem.Название;

            var result = MessageBox.Show(
                string.Format(Resources.ConfirmRemoveShipmentItem, productName),
                Resources.TitleRemoveConfirmation,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            var itemToRemove = shipmentItems.FirstOrDefault(i => i.ProductName == productName);
            if (itemToRemove != null)
            {
                shipmentItems.Remove(itemToRemove);
                RefreshShipmentList();
            }
        }
    }
}