using AutomechanicsProject.Classes;
using AutomechanicsProject.Dtos;
using AutomechanicsProject.Dtos.UI;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Properties;
using AutomechanicsProject.ViewModels;
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
        private List<ProductComboViewModel> allProducts;
        private SearchableComboBoxHelper.ComboBoxState comboBoxState;
        private List<Product> availableProducts;

        /// <summary>
        /// Инициализирует новый экземпляр формы создания отгрузки
        /// </summary>
        public CreateShipment(DateBase database)
        {
            InitializeComponent();
            db = database ?? throw new ArgumentNullException(nameof(database));
            shipmentItems = new List<ShipmentItem>();
            totalAmount = 0;
            totalItemsCount = 0;
            allProducts = new List<ProductComboViewModel>();
            availableProducts = new List<Product>();

            TextBoxHelper.SetupWatermarkTextBox(textBoxUnit, Resources.ShipmentQuantityWatermark);

            UpdateDisplay();
        }

        /// <summary>
        /// Обработчик загрузки формы
        /// </summary>
        private void CreateShipment_Load(object sender, EventArgs e)
        {
            LoadProducts();
            LoadRecipients();

            comboBoxProduct.Text = "";
            comboBoxProduct.SelectedIndex = -1;
        }

        /// <summary>
        /// Настраивает выпадающий список для поиска по названию товара
        /// </summary>
        private void SetupSearchableComboBox()
        {
            comboBoxState = new SearchableComboBoxHelper.ComboBoxState();

            var searchProducts = allProducts.Select(p => new ProductComboViewModel
            {
                Id = p.Id,
                Article = p.Article,
                Name = p.Name,
                Text = p.Text,
                Balance = p.Balance,
                Price = p.Price,
                UnitName = p.UnitName,
                UnitId = p.UnitId
            }).ToList();

            SearchableComboBoxHelper.SetupProductSearchComboBox(
                comboBoxProduct,
                comboBoxState,
                allProducts,
                OnProductSelected
            );
        }

        /// <summary>
        /// Обработчик выбора товара
        /// </summary>
        private void OnProductSelected(ProductComboViewModel selectedProduct)
        {
            LoadExpiryDatesForProduct(selectedProduct.Id);
        }

        /// <summary>
        /// Загружает доступные сроки годности для выбранного товара
        /// </summary>
        private void LoadExpiryDatesForProduct(Guid productId)
        {
            try
            {
                var selectedProduct = db.Products
                    .Include(p => p.Unit)
                    .FirstOrDefault(p => p.Id == productId);

                if (selectedProduct == null)
                {
                    return;
                }

                var productsWithSameName = db.Products
                    .Include(p => p.Unit)
                    .Where(p => p.Name == selectedProduct.Name && p.Balance > 0)
                    .Select(p => new
                    {
                        ProductId = p.Id,
                        DisplayText = FormatHelper.FormatExpiryDateDisplay(p.ExpiryDate, p.Balance),
                        ExpiryDate = p.ExpiryDate,
                        Balance = p.Balance
                    })
                    .OrderBy(p => p.ExpiryDate)
                    .ToList();

                if (productsWithSameName.Count >= 1)
                {
                    comboBoxExpiry.Enabled = productsWithSameName.Count > 1;
                    comboBoxExpiry.DisplayMember = "DisplayText";
                    comboBoxExpiry.ValueMember = "ProductId";
                    comboBoxExpiry.DataSource = productsWithSameName;
                    if (productsWithSameName.Any())
                    {
                        comboBoxExpiry.SelectedIndex = 0;
                    }
                }
                else
                {
                    comboBoxExpiry.Enabled = false;
                    comboBoxExpiry.DataSource = null;
                    comboBoxExpiry.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                Program.LogError("Ошибка при загрузке сроков годности", ex);
                comboBoxExpiry.Enabled = false;
            }
        }

        /// <summary>
        /// Загружает все товары
        /// </summary>
        private void LoadProducts()
        {
            try
            {
                var productsList = db.Products
                    .Where(p => p.Balance > 0)
                    .Select(p => new
                    {
                        p.Id,
                        p.Article,
                        p.Name,
                        p.Balance,
                        p.Price,
                        UnitName = p.Unit != null ? p.Unit.Name : "шт",
                        p.UnitId
                    })
                    .ToList();

                var products = productsList
                    .GroupBy(p => p.Name)
                    .Select(g => new ProductComboViewModel
                    {
                        Id = g.First().Id,
                        Text = FormatHelper.FormatProductWithBalance(
                            g.First().Article,
                            g.Key,
                            g.Sum(x => x.Balance),
                            g.First().UnitName),
                        Article = g.First().Article,
                        Name = g.Key,
                        Price = g.First().Price,
                        Balance = g.Sum(x => x.Balance),
                        UnitName = g.First().UnitName,
                        UnitId = g.First().UnitId
                    })
                    .ToList();

                allProducts = products;

                if (allProducts.Any())
                {
                    SetupSearchableComboBox();
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
                    .Where(a => a.CompanyName != null && a.CompanyName.Trim() != "" && a.CompanyName.Trim() != "-")
                    .OrderBy(a => a.CompanyName)
                    .Select(a => new ComboItemDto
                    {
                        Id = a.Id,
                        Text = a.CompanyName
                    })
                    .ToList();

                comboBoxRecipient1.DisplayMember = "Text";
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
                Program.LogError("Ошибка при загрузке списка получателей", ex);
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

            if (comboBoxExpiry.Enabled && comboBoxExpiry.SelectedItem != null)
            {
                var selectedExpiry = (ExpiryItemDto)comboBoxExpiry.SelectedItem; 
                Guid actualProductId = selectedExpiry.ProductId;

                var productWithExpiry = db.Products.FirstOrDefault(p => p.Id == actualProductId);

                if (productWithExpiry != null)
                {
                    if (quantity > productWithExpiry.Balance)
                    {
                        MessageBox.Show(string.Format(Resources.ErrorInsufficientStockWithDetails,
                            productWithExpiry.Balance, productWithExpiry.Unit?.Name ?? "шт"),
                            Resources.TitleWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBoxUnit.Focus();
                        return false;
                    }

                    db.SaveChanges();

                    return AddOrUpdateShipmentItem(
                        productWithExpiry.Id,
                        productWithExpiry.Name,
                        productWithExpiry.Article,
                        quantity,
                        productWithExpiry.Price,
                        productWithExpiry.Price * 2,
                        productWithExpiry.Unit?.Name ?? "шт"
                    );
                }
            }

            var selectedProduct = (ProductComboViewModel)comboBoxProduct.SelectedItem;
            var productToShip = db.Products.FirstOrDefault(p => p.Name == selectedProduct.Name && p.Balance > 0);

            if (productToShip == null)
            {
                MessageBox.Show(Resources.ErrorSelectProduct, Resources.TitleWarning);
                return false;
            }

            if (quantity > productToShip.Balance)
            {
                MessageBox.Show(string.Format(Resources.ErrorInsufficientStockWithDetails,
                    productToShip.Balance, productToShip.Unit?.Name ?? "шт"),
                    Resources.TitleWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxUnit.Focus();
                return false;
            }

            db.SaveChanges();

            return AddOrUpdateShipmentItem(
                productToShip.Id,
                productToShip.Name,
                productToShip.Article,
                quantity,
                productToShip.Price,
                productToShip.Price * 2,
                productToShip.Unit?.Name ?? "шт"
            );
        }

        /// <summary>
        /// Очищает выпадающий список сроков годности
        /// </summary>
        private void ClearExpiryComboBox()
        {
            comboBoxExpiry.DataSource = null;
            comboBoxExpiry.Items.Clear();
            comboBoxExpiry.Enabled = false;
        }

        /// <summary>
        /// Добавляет или обновляет позицию в списке отгрузки
        /// </summary>
        private bool AddOrUpdateShipmentItem(Guid productId, string productName, string article, int quantity, decimal price, decimal purchasePrice, string unitName)
        {
            var existingItem = shipmentItems.FirstOrDefault(i => i.ProductId == productId);

            if (existingItem != null)
            {
                var result = MessageBox.Show(
                    string.Format(Resources.ProductAlreadyInList, productName, existingItem.Quantity, unitName),
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
                ProductName = productName,
                Article = article,
                Quantity = quantity,
                Price = price,
                PurchasePrice = purchasePrice
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

            if (comboBoxState != null && allProducts.Any())
            {
                SearchableComboBoxHelper.ClearAndReloadProducts(comboBoxProduct, comboBoxState);
            }

            ClearExpiryComboBox();
        }

        /// <summary>
        /// Обновляет список товаров в отгрузке
        /// </summary>
        private void RefreshShipmentList()
        {
            totalAmount = 0;
            totalItemsCount = 0;
            decimal totalProfit = 0;
            decimal totalCost = 0;

            var recipientName = "Не выбран";
            if (comboBoxRecipient1.SelectedItem != null)
            {
                var selectedRecipient = (ComboItemDto)comboBoxRecipient1.SelectedItem;
                recipientName = selectedRecipient.Text;
            }

            var displayList = shipmentItems.Select(item =>
            {
                var itemTotal = item.Quantity * item.PurchasePrice;
                var itemCost = item.Quantity * item.Price;
                var profit = itemTotal - itemCost;

                totalAmount += itemTotal;
                totalCost += itemCost;
                totalProfit += profit;
                totalItemsCount += item.Quantity;

                return new ShipmentViewModel
                {
                    Article = item.Article,
                    Name = item.ProductName,
                    Quantity = item.Quantity,
                    Price = item.PurchasePrice,
                    Profit = profit,
                    Total = itemTotal,
                    RecipientName = recipientName
                };
            }).ToList();

            dataGridViewShipment.DataSource = displayList;

            UpdateDisplay(totalProfit);
        }

        /// <summary>
        /// Обновляет отображение итоговой суммы и прибыли
        /// </summary>
        private void UpdateDisplay(decimal totalProfit = 0)
        {
            if (labelTotalValue != null)
            {
                labelTotalValue.Text = totalAmount.ToString("F2");
            }
            Text = $"Формирование отгрузки - Общая сумма: {totalAmount:F2}, Прибыль: {totalProfit:F2}";
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

            if (!IsRecipientSelected())
            {
                return;
            }

            var selectedRecipient = (ComboItemDto)comboBoxRecipient1.SelectedItem;
            var recipientName = selectedRecipient.Text;
            var recipientId = selectedRecipient.Id;

            var confirmResult = MessageBox.Show(
                string.Format(Resources.ConfirmShipment, recipientName, shipmentItems.Count, totalAmount),
                Resources.TitleConfirmation,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult != DialogResult.Yes)
            {
                return;
            }

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
                            PurchasePrice = item.PurchasePrice,
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

                    Program.LogInfo($"Отгрузка успешно оформлена! Получатель: {recipientName}, Количество позиций: {shipmentItems.Count}, Общая сумма: {totalAmount:C2}");

                    MessageBox.Show(string.Format(Resources.SuccessShipmentCreatedWithDetails, recipientName, shipmentItems.Count, totalAmount),
                        Resources.TitleSuccess, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (Exception ex)
            {
                Program.LogError($"Не удалось оформить отгрузку.", ex);
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

                if (result != DialogResult.Yes)
                {
                    return;
                }
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
            if (dataGridViewShipment.CurrentRow?.DataBoundItem == null)
            {
                return;
            }

            var selectedItem = (ShipmentViewModel)dataGridViewShipment.CurrentRow.DataBoundItem;
            var productName = selectedItem.Name;

            var result = MessageBox.Show(
                string.Format(Resources.ConfirmRemoveShipmentItem, productName),
                Resources.TitleRemoveConfirmation,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                return;
            }

            var itemToRemove = shipmentItems.FirstOrDefault(i => i.ProductName == productName);
            if (itemToRemove != null)
            {
                shipmentItems.Remove(itemToRemove);
                RefreshShipmentList();
            }
        }
    }
}