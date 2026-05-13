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
using AutomechanicsProject.Enum;
using AutomechanicsProject.Services.Interfaces;



namespace AutomechanicsProject.Formes
{

    /// <summary>
    /// Форма для создания новой отгрузки товаров
    /// </summary>
    public partial class CreateShipment : Form
    {
       
        /// <summary>
        /// Сервис отгрузок
        /// </summary>
        private readonly IShipmentService _shipmentService;
        private readonly ICurrentUserService _currentUserService;
        private List<ShipmentItem> shipmentItems;
        private decimal totalAmount;
        private int totalItemsCount;
        private List<ProductComboViewModel> allProducts;
        private SearchableComboBoxHelper.ComboBoxState comboBoxState;
        private bool isShipmentTypeLocked = false;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private ShipmentTypeEnum currentShipmentType = ShipmentTypeEnum.Shipment;


        /// <summary>
        /// Инициализирует новый экземпляр формы создания отгрузки
        /// </summary>
        public CreateShipment(
            IShipmentService shipmentService,
            ICurrentUserService currentUserService)
        {
            InitializeComponent();

            _shipmentService = shipmentService ?? throw new ArgumentNullException(nameof(shipmentService));
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
            shipmentItems = new List<ShipmentItem>();
            totalAmount = 0;
            allProducts = new List<ProductComboViewModel>();


            TextBoxHelper.SetupWatermarkTextBox(textBoxUnit, Resources.ShipmentQuantityWatermark);
            TextBoxHelper.SetupWatermarkComboBox(comboBoxProduct, Resources.SProductWatermark);
            TextBoxHelper.SetupWatermarkComboBox(comboBoxRecipient1, Resources.ShipmentRecipientWatermark);
            TextBoxHelper.SetupWatermarkComboBox(comboBox1, Resources.ShipmentTypeWatermark);

            UpdateDisplay();
        }

        /// <summary>
        /// Обработчик загрузки формы
        /// </summary>
        private void CreateShipment_Load(object sender, EventArgs e)
        {
            comboBoxExpiry.Enabled = false;
            comboBoxExpiry.DropDownStyle = ComboBoxStyle.DropDown;
            comboBoxExpiry.Text = Resources.NoExpiryDateWatermark;
            comboBoxExpiry.ForeColor = Color.Gray;
            dataGridViewShipment.ReadOnly = false;
            foreach (DataGridViewColumn col in dataGridViewShipment.Columns)
            {
                if (col.Name != "ScrapMetal")
                {
                    col.ReadOnly = true;
                }
            }

            LoadProducts();
            LoadRecipients();

            comboBox1.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            dataGridViewShipment.CurrentCellDirtyStateChanged += DataGridViewShipment_CurrentCellDirtyStateChanged;
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
        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isShipmentTypeLocked)
            {
                var currentLocalizedName = ShipmentTypeHelper.GetLocalizedName(currentShipmentType);
                comboBox1.SelectedItem = comboBox1.Items.Cast<string>().FirstOrDefault(x => x == currentLocalizedName);
                return;
            }

            if (comboBox1.SelectedItem == null || comboBox1.Text == Resources.ShipmentTypeWatermark)
            {
                return;
            }

            string selectedType = comboBox1.SelectedItem.ToString();

            if (selectedType == Resources.ShipmentType_Shipment)
            {
                currentShipmentType = ShipmentTypeEnum.Shipment;
                comboBoxRecipient1.Enabled = true;
                comboBoxRecipient1.BackColor = System.Drawing.SystemColors.Window;
            }
            else if (selectedType == Resources.ShipmentType_WriteOff)
            {
                currentShipmentType = ShipmentTypeEnum.WriteOff;
                comboBoxRecipient1.Enabled = false;
                comboBoxRecipient1.BackColor = System.Drawing.SystemColors.ControlLight;
                comboBoxRecipient1.SelectedIndex = -1;
            }
            else if (selectedType == Resources.ShipmentType_Defect)
            {
                currentShipmentType = ShipmentTypeEnum.Defect;
                comboBoxRecipient1.Enabled = false;
                comboBoxRecipient1.BackColor = System.Drawing.SystemColors.ControlLight;
                comboBoxRecipient1.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Загружает доступные сроки годности для выбранного товара
        /// </summary>
     
        private void LoadExpiryDatesForProduct(Guid productId)
        {
            try
            {
                var productsWithSameName = _shipmentService.GetExpiryDatesForProduct(productId);

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
                    comboBoxExpiry.Text = Resources.NoExpiryDateWatermark;
                    comboBoxExpiry.ForeColor = Color.Gray;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Ошибка при загрузке сроков годности");
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
                allProducts = _shipmentService.GetProductsForShipment();

                if (allProducts.Any())
                {
                    SetupSearchableComboBox();
                }

                if (allProducts.Count == 0)
                {
                    MessageBox.Show(Resources.InfoNoProductsForShipment,
                        Resources.TitleInformation,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Ошибка при загрузке товаров в отгрузку");

                MessageBox.Show(Resources.ErrorLoadProducts,
                    Resources.TitleError,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Загружает список получателей
        /// </summary>
        private void LoadRecipients()
        {
            try
            {
                var recipients = _shipmentService.GetRecipientsForCombo();

                comboBoxRecipient1.DataSource = recipients;
                buttonShipment.Enabled = recipients.Count > 0;

                if (recipients.Count == 0)
                {
                    MessageBox.Show(Resources.InfoNoRecipientsAvailable,
                        Resources.TitleInformation,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Ошибка при загрузке списка получателей");

                MessageBox.Show(Resources.ErrorLoadRecipients,
                    Resources.TitleError,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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

            if (currentShipmentType == ShipmentTypeEnum.Shipment &&
                (comboBoxRecipient1.SelectedItem == null ||
                 comboBoxRecipient1.Text == Resources.ShipmentRecipientWatermark ||
                 string.IsNullOrWhiteSpace(comboBoxRecipient1.Text)))
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

                var productWithExpiry = _shipmentService.GetProductForShipmentById(actualProductId);

                if (productWithExpiry != null)
                {
                    if (quantity > productWithExpiry.Balance)
                    {
                        MessageBox.Show(string.Format(Resources.ErrorInsufficientStockWithDetails,
                            productWithExpiry.Balance, productWithExpiry.Unit?.Name ?? Resources.Unit_Piece_Short),
                            Resources.TitleWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBoxUnit.Focus();
                        return false;
                    }

                    return AddOrUpdateShipmentItem(
                        productWithExpiry.Id,
                        productWithExpiry.Name,
                        productWithExpiry.Article,
                        quantity,
                        productWithExpiry.Price,
                        productWithExpiry.Price * 2,
                        productWithExpiry.Unit?.Name ?? Resources.Unit_Piece_Short
                    );
                }
            }

            var selectedProduct = (ProductComboViewModel)comboBoxProduct.SelectedItem;
            var productToShip = _shipmentService.GetProductForShipmentByName(selectedProduct.Name);

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
            comboBoxExpiry.Text = Resources.NoExpiryDateWatermark;
            comboBoxExpiry.ForeColor = Color.Gray;
        }

        /// <summary>
        /// Добавляет или обновляет позицию в списке отгрузки
        /// </summary>
        private bool AddOrUpdateShipmentItem(Guid productId, string productName, string article, int quantity, decimal price, decimal purchasePrice, string unitName)
        {
            bool isMetal = _shipmentService.IsProductMetal(productId);

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
                    existingItem.IsMetal = isMetal;
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
                PurchasePrice = purchasePrice,
                IsMetal = isMetal,
                ScrapMetal = false
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

            if (currentShipmentType != ShipmentTypeEnum.Shipment)
            {
                comboBoxRecipient1.SelectedIndex = -1;
                comboBoxRecipient1.Text = Resources.ShipmentRecipientWatermark;
                comboBoxRecipient1.ForeColor = Color.Gray;
            }

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
                    recipientName = Resources.ShipmentType_WriteOff;
                    break;
                case ShipmentTypeEnum.Defect:
                    recipientName = Resources.ShipmentType_Defect;
                    break;
                default:
                    if (comboBoxRecipient1.SelectedItem != null)
                    {
                        var selectedRecipient = (ComboItemDto)comboBoxRecipient1.SelectedItem;
                        recipientName = selectedRecipient.Text;
                    }
                    else
                    {
                        recipientName = Resources.NotListed;
                    }
                    break;
            }

            var displayList = shipmentItems.Select(item =>
            {
                var itemTotal = item.Quantity * item.PurchasePrice;
                var itemCost = item.Quantity * item.Price;
                decimal profit;

                if (currentShipmentType == ShipmentTypeEnum.Shipment)
                {
                    profit = itemTotal - itemCost;
                }
                else if (currentShipmentType == ShipmentTypeEnum.Defect)
                {
                    decimal scrapReturn = (item.IsMetal && item.ScrapMetal)
                        ? itemTotal * 0.5m
                        : 0;

                    profit = -(item.Quantity * item.PurchasePrice) + scrapReturn;

                    item.ScrapReturn = scrapReturn;
                }
                else
                {
                    profit = -(item.Quantity * item.Price);
                }

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
                    RecipientName = recipientName,
                    ProductId = item.ProductId ?? Guid.Empty,
                    IsMetal = item.IsMetal,
                    IsScrapped = (currentShipmentType == ShipmentTypeEnum.Defect) && item.IsMetal && item.ScrapMetal,
                    ScrapMetal = item.ScrapMetal
                };
            }).ToList();

            dataGridViewShipment.DataSource = displayList;

            if (dataGridViewShipment.Columns["ScrapMetal"] != null)
            {
                dataGridViewShipment.Columns["ScrapMetal"].Visible = (currentShipmentType == ShipmentTypeEnum.Defect);
            }

            if (currentShipmentType == ShipmentTypeEnum.Defect)
            {
                foreach (DataGridViewRow row in dataGridViewShipment.Rows)
                {
                    if (row.DataBoundItem is ShipmentViewModel vm && !vm.IsMetal)
                    {
                        row.Cells["ScrapMetal"].ReadOnly = true;
                        row.Cells["ScrapMetal"].Style.BackColor = System.Drawing.Color.LightGray;
                        row.Cells["ScrapMetal"].Value = false;
                    }
                }
            }

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
            Text = string.Format(Resources.ShipmentForm_TitleFormat, totalAmount, totalProfit);
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
                    recipientName = Resources.ShipmentType_WriteOff;
                    recipientId = null;
                    displayTotal = -totalAmount;
                    break;
                case ShipmentTypeEnum.Defect:
                    recipientName = Resources.ShipmentType_Defect;
                    recipientId = null;
                    displayTotal = -totalAmount;
                    break;
                default:
                    {
                        var selectedRecipient = comboBoxRecipient1.SelectedItem as ComboItemDto;

                        if (selectedRecipient == null)
                        {
                            MessageBox.Show(Resources.ErrorSelectRecipient,
                                Resources.TitleWarning,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                            return;
                        }

                        recipientName = selectedRecipient.Text;
                        recipientId = selectedRecipient.Id;
                        displayTotal = totalAmount;
                        break;
                    }
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
                if (_currentUserService.CurrentUser == null)
                {
                    MessageBox.Show("Текущий пользователь не найден.");
                    return;
                }
                _shipmentService.CreateShipment(
                shipmentItems,
                recipientId,
                _currentUserService.CurrentUser.Id,
                totalAmount,
                currentShipmentType
);

                if (currentShipmentType == ShipmentTypeEnum.Defect)
                {
                    decimal totalScrapReturn = shipmentItems
                        .Where(i => i.IsMetal && i.ScrapMetal)
                        .Sum(i => i.PurchasePrice * i.Quantity * 0.5m);

                    int scrapItemsCount = shipmentItems.Count(i => i.IsMetal && i.ScrapMetal);
                    int metalItemsNotScrapped = shipmentItems.Count(i => i.IsMetal && !i.ScrapMetal);

                    if (totalScrapReturn > 0)
                    {
                        MessageBox.Show(
                            string.Format(Resources.ScrapMetal_ReturnMessage, scrapItemsCount, totalScrapReturn),
                            Resources.ScrapMetal_Title,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else if (metalItemsNotScrapped > 0)
                    {
                        MessageBox.Show(
                            string.Format(Resources.ScrapMetal_NoReturnMessage, metalItemsNotScrapped),
                            Resources.ScrapMetal_Title,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }

                logger.Info($"Отгрузка успешно оформлена! Получатель: {recipientName}, Количество позиций: {shipmentItems.Count}, Общая сумма: {totalAmount:C2}");

                MessageBox.Show(string.Format(Resources.SuccessShipmentCreatedWithDetails, recipientName, shipmentItems.Count, totalAmount),
                    Resources.TitleSuccess, MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Ошибка при создании отгрузки");

                MessageBox.Show(Resources.ErrorCreateShipment,
                    Resources.TitleError,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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

        /// <summary>
        /// Обработчик изменения чекбокса для металлолома 
        /// </summary>
        private void DataGridViewShipment_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridViewShipment.CurrentCell is DataGridViewCheckBoxCell &&
                dataGridViewShipment.IsCurrentCellDirty)
            {
                dataGridViewShipment.CommitEdit(DataGridViewDataErrorContexts.Commit);

                if (dataGridViewShipment.CurrentRow?.DataBoundItem is ShipmentViewModel viewModel)
                {
                    var item = shipmentItems.FirstOrDefault(i => i.ProductId == viewModel.ProductId);
                    if (item != null)
                    {
                        item.ScrapMetal = viewModel.ScrapMetal;
                        RefreshShipmentList();
                    }
                }
            }
        }
    }
}