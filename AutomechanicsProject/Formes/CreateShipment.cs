using AutomechanicsProject.Classes;
using AutomechanicsProject.Dtos;
using AutomechanicsProject.Dtos.UI;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Properties;
using AutomechanicsProject.ViewModels;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AutomechanicsProject.Formes
{
    /// <summary>
    /// Перечисление типов отгрузки
    ///</summary>
    public enum ShipmentTypeEnum
    {
        Shipment,
        WriteOff,
        Defect
    }

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
        private bool isShipmentTypeLocked = false;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private static readonly Guid WriteOffUserId = Guid.Parse("dc40ff88-af12-4841-b101-9da423f7f777");
        private static readonly Guid DefectUserId = Guid.Parse("fda70302-e336-4a5d-9783-eff33827adc8"); 
        private ShipmentTypeEnum currentShipmentType = ShipmentTypeEnum.Shipment;

        /// <summary>
        /// Инициализирует новый экземпляр формы создания отгрузки
        /// </summary>
        public CreateShipment(DateBase database)
        {
            InitializeComponent();
            db = database ?? throw new ArgumentNullException(nameof(database));
            shipmentItems = new List<ShipmentItem>();
            totalAmount = 0;
            allProducts = new List<ProductComboViewModel>();

            UpdateDisplay();
        }

        /// <summary>
        /// Обработчик загрузки формы
        /// </summary>
        private void CreateShipment_Load(object sender, EventArgs e)
        {
            TextBoxHelper.SetupWatermarkTextBox(textBoxUnit, Resources.ShipmentQuantityWatermark);
            TextBoxHelper.SetupWatermarkComboBox(comboBoxProduct, Resources.SProductWatermark);
            TextBoxHelper.SetupWatermarkComboBox(comboBoxRecipient1, Resources.ShipmentRecipientWatermark);
            TextBoxHelper.SetupWatermarkComboBox(comboBox1, Resources.ShipmentTypeWatermark);

            comboBoxExpiry.Enabled = false;
            comboBoxExpiry.DropDownStyle = ComboBoxStyle.DropDown;
            comboBoxExpiry.Text = "Нет срока годности";
            comboBoxExpiry.ForeColor = Color.Gray;

            LoadProducts();
            LoadRecipients();

            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
        }

        /// <summary>
        /// Настраивает выпадающий список для поиска по названию товара
        /// </summary>
        private void SetupSearchableComboBox()
        {
            comboBoxState = new SearchableComboBoxHelper.ComboBoxState();

            SearchableComboBoxHelper.SetupProductSearchComboBox(
                comboBoxProduct,
                comboBoxState,
                allProducts,
                OnProductSelected
            );

            comboBoxProduct.SelectedIndex = -1;
            comboBoxProduct.Text = Resources.SProductWatermark;
            comboBoxProduct.ForeColor = Color.Gray;
        }

        /// <summary>
        /// Обработчик выбора товара
        /// </summary>
        private void OnProductSelected(ProductComboViewModel selectedProduct)
        {
            comboBoxProduct.ForeColor = SystemColors.WindowText;
            LoadExpiryDatesForProduct(selectedProduct.Id);
        }

        /// <summary>
        /// Выбор типа отгрузки
        /// </summary>
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isShipmentTypeLocked) 
            {
                comboBox1.SelectedItem = comboBox1.Items.Cast<object>().FirstOrDefault(x => x.ToString() == currentShipmentType.ToString());
                return;
            }

            if (comboBox1.SelectedItem == null || comboBox1.Text == Resources.ShipmentTypeWatermark)
            {
                return;
            }

            string selectedType = comboBox1.SelectedItem.ToString();

            switch (selectedType)
            {
                case "Shipment":
                    currentShipmentType = ShipmentTypeEnum.Shipment;
                    comboBoxRecipient1.Enabled = true;
                    comboBoxRecipient1.BackColor = System.Drawing.SystemColors.Window;
                    break;
                case "Списание":
                    currentShipmentType = ShipmentTypeEnum.WriteOff;
                    comboBoxRecipient1.Enabled = false;
                    comboBoxRecipient1.BackColor = System.Drawing.SystemColors.ControlLight;
                    comboBoxRecipient1.SelectedIndex = -1;
                    break;
                case "Брак":
                    currentShipmentType = ShipmentTypeEnum.Defect;
                    comboBoxRecipient1.Enabled = false;
                    comboBoxRecipient1.BackColor = System.Drawing.SystemColors.ControlLight;
                    comboBoxRecipient1.SelectedIndex = -1;
                    break;
            }
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
                    .Select(p => new ExpiryItemDto
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
                    comboBoxExpiry.ForeColor = SystemColors.WindowText;

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
                    comboBoxExpiry.Text = "Нет срока годности";
                    comboBoxExpiry.ForeColor = Color.Gray;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка при загрузке сроков годности", ex);
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
                logger.Error("Ошибка при загрузке товаров в отгрузку", ex);
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
                var excludedNames = new[] { "Брак", "Списание", "-", "" };

                var recipients = db.Addresses
                    .Where(a => a.CompanyName != null &&
                       !excludedNames.Contains(a.CompanyName.Trim()))
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

                buttonShipment.Enabled = recipients.Count > 0;

                if (recipients.Count == 0)
                {
                    MessageBox.Show(Resources.InfoNoRecipientsAvailable, Resources.TitleInformation,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка при загрузке списка получателей", ex);
                MessageBox.Show(Resources.ErrorLoadRecipients, Resources.TitleError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки Добавить в список
        /// </summary>
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (shipmentItems.Count == 0 && TryAddProductToShipment())
            {
                isShipmentTypeLocked = true;
                comboBox1.Enabled = false;
                comboBox1.BackColor = SystemColors.ControlLight;

                RefreshShipmentList();
                ClearAddFields();
                MessageBox.Show(Resources.SuccessProductAddedToList, Resources.TitleSuccess,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else if (TryAddProductToShipment())
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

            if (currentShipmentType == ShipmentTypeEnum.Shipment && comboBoxRecipient1.SelectedItem == null)
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
            comboBoxExpiry.Text = "Нет срока годности";
            comboBoxExpiry.ForeColor = Color.Gray;
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

            comboBoxRecipient1.SelectedIndex = -1;
            comboBoxRecipient1.Text = Resources.ShipmentRecipientWatermark;
            comboBoxRecipient1.ForeColor = Color.Gray;

            comboBoxExpiry.Enabled = false;
            comboBoxExpiry.Text = Resources.NoExpiryDateWatermark;
            comboBoxExpiry.ForeColor = Color.Gray;

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

            string recipientName;
            switch (currentShipmentType)
            {
                case ShipmentTypeEnum.WriteOff:
                    recipientName = "Списание";
                    break;
                case ShipmentTypeEnum.Defect:
                    recipientName = "Брак";
                    break;
                default:
                    if (comboBoxRecipient1.SelectedItem != null)
                    {
                        var selectedRecipient = (ComboItemDto)comboBoxRecipient1.SelectedItem;
                        recipientName = selectedRecipient.Text;
                    }
                    else
                    {
                        recipientName = "Не выбран";
                    }
                    break;
            }

            var displayList = shipmentItems.Select(item =>
            {
                var itemTotal = item.Quantity * item.PurchasePrice;
                var itemCost = item.Quantity * item.Price;
                var profit = (currentShipmentType == ShipmentTypeEnum.Shipment)
                ? itemTotal - itemCost   
                : 0;

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

            string recipientName;
            Guid? recipientId = null;
            decimal displayTotal;

            switch (currentShipmentType)
            {
                case ShipmentTypeEnum.WriteOff:
                    recipientName = "Списание";
                    recipientId = WriteOffUserId;
                    displayTotal = -totalAmount;
                    break;
                case ShipmentTypeEnum.Defect:
                    recipientName = "Брак";
                    recipientId = DefectUserId;
                    displayTotal = -totalAmount;
                    break;
                default:
                    if (comboBoxRecipient1.SelectedItem == null)
                    {
                        MessageBox.Show(Resources.ErrorSelectRecipient, Resources.TitleWarning,
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    var selectedRecipient = (ComboItemDto)comboBoxRecipient1.SelectedItem;
                    recipientName = selectedRecipient.Text;
                    recipientId = selectedRecipient.Id;
                    displayTotal = totalAmount;
                    break;
            }

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
                        UserId = recipientId.Value,
                        CreatedByUserId = Program.CurrentUser?.Id ?? Guid.Empty,
                        TotalAmount = totalAmount,
                        ShipmentType = currentShipmentType.ToString()
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
                            Quantity = currentShipmentType == ShipmentTypeEnum.Shipment ? item.Quantity : -Math.Abs(item.Quantity),
                            Price = item.Price,
                            PurchasePrice = currentShipmentType == ShipmentTypeEnum.Shipment
                            ? item.PurchasePrice      
                            : 0,
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

                    logger.Info($"Отгрузка успешно оформлена! Получатель: {recipientName}, Количество позиций: {shipmentItems.Count}, Общая сумма: {totalAmount:C2}");

                    MessageBox.Show(string.Format(Resources.SuccessShipmentCreatedWithDetails, recipientName, shipmentItems.Count, totalAmount),
                        Resources.TitleSuccess, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Не удалось оформить отгрузку", ex);
                MessageBox.Show(Resources.ErrorCreateShipment,
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