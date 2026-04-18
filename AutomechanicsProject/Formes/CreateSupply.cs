using AutomechanicsProject.Classes;
using AutomechanicsProject.ViewModels;
using AutomechanicsProject.Dtos.UI;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Properties;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutomechanicsProject.Formes
{
    /// <summary>
    /// Форма создания поставки 
    /// </summary>
    public partial class CreateSupply : Form
    {
        private readonly DateBase _db;
        private List<SupplyPosition> positions = new List<SupplyPosition>();
        private string currentCurrency = CurrencyCodes.RUB;
        private decimal currentCurrencyRate = 1.0m;
        private bool isCurrencyFixed = false;
        private List<CurrencyInfo> currencies;
        private List<ProductDisplayItem> cachedProducts;
        private List<ComboItemDto> cachedSuppliers;
        private ToolTip productToolTip;
        private List<ProductComboViewModel> allProductsForSearch;
        private SearchableComboBoxHelper.ComboBoxState comboBoxState;

        public CreateSupply(DateBase database)
        {
            InitializeComponent();
            _db = database ?? throw new ArgumentNullException(nameof(database));
        }

        /// <summary>
        /// Обработчик загрузки формы
        /// </summary>
        private void CreateSupply_Load(object sender, EventArgs e)
        {
            LoadProductsFromDatabase();
            LoadCurrencies();
            LoadSuppliersFromDatabase();


            comboBoxProduct.Text = "";
            comboBoxProduct.SelectedIndex = -1;
        }

        /// <summary>
        /// Настраивает выпадающий список для поиска по товарам
        /// </summary>
        private void SetupSearchableComboBox()
        {
            comboBoxState = new SearchableComboBoxHelper.ComboBoxState();

            SearchableComboBoxHelper.SetupProductSearchComboBox(
                comboBoxProduct,
                comboBoxState,
                allProductsForSearch,
                OnProductSelected
            );
        }

        private void OnProductSelected(ProductComboViewModel selectedProduct)
        {
            var product = cachedProducts.FirstOrDefault(p => p.Id == selectedProduct.Id);
            bool hasExpiryDate = product?.HasExpiryDate == true;

            dateTimePickerExpiry.Enabled = hasExpiryDate;
            dateTimePickerExpiry.Checked = hasExpiryDate;
        }

        /// <summary>
        /// Загружает список валют в выпадающий список
        /// </summary>
        private void LoadCurrencies()
        {
            currencies = CurrencyHelper.GetCurrencies();

            comboBoxCurrency.DataSource = currencies;
            comboBoxCurrency.DisplayMember = "DisplayText";
            comboBoxCurrency.ValueMember = "Code";

            comboBoxCurrency.SelectedIndex = 0;
            currentCurrency = CurrencyCodes.RUB;

            comboBoxCurrency.SelectedIndexChanged += ComboBoxCurrency_SelectedIndexChanged;
        }

        /// <summary>
        /// Обработчик изменения выбранной валюты
        /// </summary>
        private void ComboBoxCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isCurrencyFixed && positions.Count > 0)
            {
                var previousCurrency = currencies.FirstOrDefault(c => c.Code == currentCurrency);
                if (previousCurrency != null)
                {
                    comboBoxCurrency.SelectedItem = previousCurrency;
                }
                MessageBox.Show(Resources.ErrorCurrencyFixed, Resources.TitleWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedCurrency = comboBoxCurrency.SelectedItem as CurrencyInfo;
            if (selectedCurrency == null)
            {
                return;
            }

            try
            {
                if (positions.Count == 0)
                {
                    currentCurrency = selectedCurrency.Code;
                    currentCurrencyRate = selectedCurrency.Rate;
                }

                UpdatePricesInGrid();
                UpdateTotalAmount();
            }
            catch (Exception ex)
            {
                Program.LogError("Ошибка при смене валюты", ex);
            }
        }
        
        /// <summary>
        /// Проверяет, что ввод в поле количества содержит только цифры
        /// </summary>
        private void ValidateNumberInput(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Проверяет, что ввод в поле цены содержит только цифры и разделители
        /// </summary>
        private void ValidateDecimalInput(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',')
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.' || e.KeyChar == ',') && textBox.Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Загружает список товаров из базы данных для выбора в комбобоксе
        /// </summary>
        private void LoadProductsFromDatabase()
        {
            try
            {
                cachedProducts = _db.Products
                    .OrderBy(p => p.Name)
                    .Select(p => new ProductDisplayItem
                    {
                        Id = p.Id,
                        Article = p.Article,
                        Name = p.Name,
                        Balance = p.Balance,
                        IsActive = p.Balance > 0,
                        ProductExpiryDate = p.ExpiryDate
                    })
                    .AsNoTracking()
                    .ToList();

                allProductsForSearch = cachedProducts
                    .Select(p => new ProductComboViewModel
                    {
                        Id = p.Id,
                        Article = p.Article,
                        Name = p.Name,
                        Text = $"{p.Article} - {p.Name} (остаток: {p.Balance} шт.)",
                        Balance = p.Balance
                    })
                    .ToList();

                if (allProductsForSearch != null && allProductsForSearch.Count > 0)
                {
                    SetupSearchableComboBox();
                }
                else
                {
                    MessageBox.Show(Resources.ErrorNoProducts, Resources.TitleWarning,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Program.LogError("Ошибка при загрузке товаров из БД", ex);
                MessageBox.Show(Resources.ErrorLoadProducts, Resources.TitleError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Загружает список поставщиков из базы данных для выбора в комбобоксе
        /// </summary>
        private void LoadSuppliersFromDatabase()
        {
            try
            {
                var suppliers = _db.Suppliers
             .OrderBy(s => s.Name)
             .Select(s => new ComboItemDto
             {
                 Id = s.Id,
                 Text = s.Name
             })
             .AsNoTracking()
             .ToList();

                cachedSuppliers = suppliers;

                if (cachedSuppliers != null && cachedSuppliers.Count > 0)
                {
                    comboBoxSupplier.DisplayMember = "Text";
                    comboBoxSupplier.ValueMember = "Id";
                    comboBoxSupplier.DataSource = cachedSuppliers;
                    comboBoxSupplier.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show(Resources.ErrorNoSuppliers, Resources.TitleWarning,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Program.LogError("Ошибка при загрузке поставщиков из БД", ex);
                MessageBox.Show(Resources.ErrorLoadSuppliers, Resources.TitleError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Проверяет корректность заполнения всех обязательных полей
        /// </summary>
        private bool ValidateInputs()
        {
            if (comboBoxProduct.SelectedItem == null)
            {
                MessageBox.Show(Resources.ErrorSelectProduct, Resources.TitleWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxProduct.Focus();
                return false;
            }

            var selectedProductDto = (ProductComboViewModel)comboBoxProduct.SelectedItem;
            var selectedProduct = cachedProducts.FirstOrDefault(p => p.Id == selectedProductDto.Id);

            if (!int.TryParse(textBoxQuantity.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show(Resources.ErrorEnterValidQuantity, Resources.TitleWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxQuantity.Focus();
                return false;
            }

            if (!decimal.TryParse(textBoxPrice.Text.Replace('.', ','), out decimal price) || price <= 0)
            {
                MessageBox.Show(Resources.ErrorEnterValidPrice, Resources.TitleWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxPrice.Focus();
                return false;
            }

            if (comboBoxSupplier.SelectedItem == null)
            {
                MessageBox.Show(Resources.ErrorSelectSupplier, Resources.TitleWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxSupplier.Focus();
                return false;
            }

            if (selectedProduct.HasExpiryDate)
            {
                if (!dateTimePickerExpiry.Checked || dateTimePickerExpiry.Value < DateTime.Now.Date)
                {
                    MessageBox.Show(Resources.ErrorExpiryDateRequired, Resources.TitleWarning,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dateTimePickerExpiry.Focus();
                    return false;
                }

                if (dateTimePickerExpiry.Value > selectedProduct.ProductExpiryDate.Value)
                {
                    MessageBox.Show(string.Format(Resources.ErrorExpiryDateExceedsProduct,
                        selectedProduct.ProductExpiryDate.Value.ToShortDateString()),
                        Resources.TitleWarning,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dateTimePickerExpiry.Focus();
                    return false;
                }
            }
            else
            {
                if (dateTimePickerExpiry.Checked)
                {
                    MessageBox.Show(Resources.ErrorNoExpiryDateAllowed, Resources.TitleWarning,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Очищает поля ввода после добавления позиции
        /// </summary>
        private void ClearInputFields()
        {
            textBoxQuantity.Clear();
            textBoxPrice.Clear();

            if (comboBoxState != null)
            {
                SearchableComboBoxHelper.ClearAndReloadProducts(comboBoxProduct, comboBoxState);
            }
        }

        /// <summary>
        /// Обновляет общую сумму поставки
        /// </summary>
        private void UpdateTotalAmount()
        {
            decimal totalInRUB = positions.Sum(p => p.Quantity * p.Price);

            if (comboBoxCurrency.SelectedItem != null)
            {
                var selectedCurrency = comboBoxCurrency.SelectedItem as CurrencyInfo;
                if (selectedCurrency != null)
                {
                    decimal displayTotal = CurrencyHelper.ConvertFromRUB(totalInRUB, selectedCurrency.Rate);
                    labelTotalValue.Text = $"{displayTotal:N2} {selectedCurrency.Code}";
                    return;
                }
            }

            labelTotalValue.Text = $"{totalInRUB:N2} RUB";
        }

        /// <summary>
        /// Обновляет отображение цен в таблице при смене валюты
        /// </summary>
        private void UpdatePricesInGrid()
        {
            if (comboBoxCurrency.SelectedItem == null || dataGridViewSupply.Rows.Count == 0 || positions.Count == 0)
            {
                return;
            }

            var selectedCurrency = comboBoxCurrency.SelectedItem as CurrencyInfo;
            if (selectedCurrency == null)
            {
                return;
            }

            decimal rate = selectedCurrency.Rate;
            string currencyCode = selectedCurrency.Code;

            int maxIndex = Math.Min(dataGridViewSupply.Rows.Count, positions.Count);

            for (int i = 0; i < maxIndex; i++)
            {
                try
                {
                    var row = dataGridViewSupply.Rows[i];
                    if (row.IsNewRow)
                    {
                        continue;
                    }

                    var position = positions[i];
                    if (position == null)
                    {
                        continue;
                    }

                    decimal displayPrice = CurrencyHelper.ConvertFromRUB(position.Price, rate);

                    if (row.Cells["colPrice"] != null)
                    {
                        row.Cells["colPrice"].Value = $"{displayPrice:F2} {currencyCode}";

                    }
                    if (row.Cells["colTotal"] != null)
                    {
                        row.Cells["colTotal"].Value = $"{displayPrice * position.Quantity:F2} {currencyCode}";

                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Ошибка обновления строки {i}: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Добавляет выбранную позицию в список поставки
        /// </summary>
        private void ButtonAddToList_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
            {
                return;
            }

            if (positions.Count == 0 && !isCurrencyFixed)
            {
                var firstCurrency = comboBoxCurrency.SelectedItem as CurrencyInfo;
                if (firstCurrency != null)
                {
                    currentCurrency = firstCurrency.Code;
                    currentCurrencyRate = firstCurrency.Rate;
                    isCurrencyFixed = true;

                    comboBoxCurrency.Enabled = false;
                    comboBoxCurrency.BackColor = System.Drawing.SystemColors.ControlLight;
                }
            }

            var selectedProductDto = (ProductComboViewModel)comboBoxProduct.SelectedItem;
            var selectedItem = cachedProducts.FirstOrDefault(p => p.Id == selectedProductDto.Id);
            var selectedSupplier = (ComboItemDto)comboBoxSupplier.SelectedItem;
            int quantity = int.Parse(textBoxQuantity.Text);
            var priceInSelectedCurrency = decimal.Parse(textBoxPrice.Text.Replace('.', ','));

            var selectedCurrency = (CurrencyInfo)comboBoxCurrency.SelectedItem;
            decimal rate = selectedCurrency.Rate;
            string currencyCode = selectedCurrency.Code;

            decimal priceInRUB = CurrencyHelper.ConvertToRUB(priceInSelectedCurrency, rate);

            DateTime? supplyExpiryDate = null;

            if (selectedItem.HasExpiryDate)
            {
                supplyExpiryDate = dateTimePickerExpiry.Value;
            }

            var position = new SupplyPosition
            {
                Id = Guid.NewGuid(),
                ProductId = selectedItem.Id,
                ProductName = selectedItem.Name,
                Article = selectedItem.Article,
                Quantity = quantity,
                Price = priceInRUB,  
                SupplierId = selectedSupplier.Id,
                SupplierName = selectedSupplier.Text,
                ExpiryDate = selectedItem.HasExpiryDate ? dateTimePickerExpiry.Value : (DateTime?)null
            };

            positions.Add(position);

            decimal displayPrice = CurrencyHelper.ConvertFromRUB(priceInRUB, rate);

            dataGridViewSupply.Rows.Add(
                position.Article,
                position.ProductName,
                position.Quantity,
                $"{displayPrice:F2} {currencyCode}",
                $"{displayPrice * position.Quantity:F2} {currencyCode}",
                selectedSupplier.Text,
                position.ExpiryDate?.ToShortDateString() ?? "");

            UpdateTotalAmount();
            ClearInputFields();
        }
        
        /// <summary>
        /// Импортирует данные поставки из JSON файла
        /// </summary>
        private async void ButtonImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = Resources.JsonFileFilter;
                openFileDialog.Title = Resources.ImportSupplyTitle;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string jsonContent = System.IO.File.ReadAllText(openFileDialog.FileName);
                        var importData = JsonSerializer.Deserialize<ImportFileData>(jsonContent);

                        if (importData == null || importData.Products == null || importData.Products.Count == 0)
                        {
                            MessageBox.Show(Resources.ErrorImportNoData, Resources.TitleWarning,
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        positions.Clear();
                        dataGridViewSupply.Rows.Clear();

                        if (!string.IsNullOrEmpty(importData.Currency))
                        {
                            int index = comboBoxCurrency.FindStringExact(importData.Currency);
                            if (index >= 0)
                            {
                                comboBoxCurrency.SelectedIndex = index;
                            }
                        }

                        var importedCount = 0;
                        var notFoundCount = 0;
                        List<string> notFoundArticles = new List<string>();

                        foreach (var item in importData.Products)
                        {
                            var product = cachedProducts
                                .FirstOrDefault(p => p.Article.Equals(item.Article, StringComparison.OrdinalIgnoreCase));

                            if (product == null)
                            {
                                notFoundCount++;
                                notFoundArticles.Add(item.Article);
                                continue;
                            }

                            var supplier = cachedSuppliers
                                .FirstOrDefault(s => s.Text.Equals(item.SupplierName, StringComparison.OrdinalIgnoreCase));

                            if (supplier == null && cachedSuppliers.Count > 0)
                            {
                                supplier = cachedSuppliers[0];
                            }
                            DateTime? expiryDate = null;
                            if (product.HasExpiryDate)  
                            {
                                if (!string.IsNullOrEmpty(item.ExpiryDate))
                                {
                                    if (DateTime.TryParse(item.ExpiryDate, out DateTime parsedDate))
                                    {
                                        expiryDate = parsedDate;
                                    }
                                }
                                if (expiryDate == null)
                                {
                                    expiryDate = dateTimePickerExpiry.Value;
                                }
                            }

                            SupplyPosition position = new SupplyPosition
                            {
                                Id = Guid.NewGuid(),
                                ProductId = product.Id,
                                ProductName = product.Name,
                                Article = product.Article,
                                Quantity = item.Quantity,
                                Price = item.Price,
                                SupplyId = Guid.Empty,
                                SupplierId = supplier?.Id,
                                SupplierName = supplier?.Text,
                                ExpiryDate = expiryDate
                            };

                            positions.Add(position);

                            dataGridViewSupply.Rows.Add(
                                position.Article,
                                position.ProductName,
                                position.Quantity,
                                $"{position.Price:N2} {currentCurrency}",
                                $"{position.Quantity * position.Price:N2} {currentCurrency}",
                                supplier?.Text ?? Resources.UnknownSupplier,
                                position.ExpiryDate?.ToShortDateString() ?? ""
                            );

                            importedCount++;
                        }

                        if (importedCount > 0 && !isCurrencyFixed)
                        {
                            var importedCurrency = comboBoxCurrency.SelectedItem as CurrencyInfo;
                            if (importedCurrency != null)
                            {
                                currentCurrency = importedCurrency.Code;
                                currentCurrencyRate = importedCurrency.Rate;
                                isCurrencyFixed = true;

                                comboBoxCurrency.Enabled = false;
                                comboBoxCurrency.BackColor = System.Drawing.SystemColors.ControlLight;
                            }
                        }

                        UpdateTotalAmount();

                        string warningMessage = notFoundCount > 0
                            ? $"\n\n{Resources.ErrorImportNotFound}: {notFoundCount}\n{string.Join(", ", notFoundArticles)}"
                            : "";

                        MessageBox.Show(string.Format(Resources.SuccessImportFormat, importedCount) + warningMessage,
                            Resources.TitleSuccess,
                            MessageBoxButtons.OK,
                            notFoundCount > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        Program.LogError("Ошибка при импорте", ex);
                        MessageBox.Show(string.Format(Resources.ErrorImportFailed, ex.Message), Resources.TitleError,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Сброс состояния
        /// </summary>
        private void ResetSupplyState()
        {
            positions.Clear();
            dataGridViewSupply.Rows.Clear();
            isCurrencyFixed = false;
            currentCurrency = CurrencyCodes.RUB;
            currentCurrencyRate = 1.0m;

            comboBoxCurrency.Enabled = true;
            comboBoxCurrency.BackColor = System.Drawing.SystemColors.Window;

            var defaultCurrency = currencies.FirstOrDefault(c => c.Code == CurrencyCodes.RUB);
            if (defaultCurrency != null)
            {
                comboBoxCurrency.SelectedItem = defaultCurrency;
            }
        }

        /// <summary>         
        /// Отменяет создание поставки и закрывает форму         
        /// </summary>
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            if (positions.Count > 0)
            {
                DialogResult result = MessageBox.Show(Resources.ConfirmCancelSupply, Resources.TitleConfirmation,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    ResetSupplyState();
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
            }
            else
            {
                ResetSupplyState();
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        /// <summary>
        /// Подтверждает поставку и сохраняет данные в базу
        /// </summary>
        private async void ButtonConfirmSupply_Click(object sender, EventArgs e)
        {
            if (positions.Count == 0)
            {
                MessageBox.Show(Resources.ErrorNoPositions, Resources.TitleWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal totalInRUB = positions.Sum(p => p.Quantity * p.Price);
            string displayTotalText;

            if (currentCurrency != "RUB" && currentCurrencyRate != 1.0m)
            {
                decimal displayTotal = CurrencyHelper.ConvertFromRUB(totalInRUB, currentCurrencyRate);
                displayTotalText = $"{displayTotal:N2} {currentCurrency}";
            }
            else
            {
                displayTotalText = $"{totalInRUB:N2} RUB";
            }

            DialogResult confirmResult = MessageBox.Show(
                string.Format(Resources.ConfirmSupplyFormat, displayTotalText),
                Resources.TitleConfirmation,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                buttonConfirmSupply.Enabled = false;
                buttonConfirmSupply.Text = Resources.StatusSaving;

                try
                {
                    Supply supply = new Supply
                    {
                        Id = Guid.NewGuid(),
                        OrderNumber = GenerateOrderNumber(),
                        DateCreated = DateTime.Now,
                        UserId = GetCurrentUserId(),
                        Status = Resources.SupplyStatusCompleted,
                        TotalAmount = totalInRUB
                    };

                    using (var transaction = await _db.Database.BeginTransactionAsync())
                    {
                        try
                        {
                            _db.Supplies.Add(supply);
                            await _db.SaveChangesAsync();

                            foreach (var pos in positions)
                            {
                                var supplyPosition = new SupplyPosition
                                {
                                    Id = Guid.NewGuid(),
                                    SupplyId = supply.Id,
                                    ProductId = pos.ProductId,
                                    Quantity = pos.Quantity,
                                    Price = pos.Price,
                                    ProductName = pos.ProductName,
                                    Article = pos.Article,
                                    SupplierId = pos.SupplierId,
                                    SupplierName = pos.SupplierName,
                                    ExpiryDate = pos.ExpiryDate
                                };
                                _db.SupplyPositions.Add(supplyPosition);
                            }
                            await _db.SaveChangesAsync();

                            foreach (var pos in positions)
                            {
                                var product = await _db.Products.FindAsync(pos.ProductId);
                                if (product != null)
                                {
                                    product.Balance += pos.Quantity;

                                    product.Price = pos.Price;

                                    if (pos.ExpiryDate.HasValue)
                                    {
                                        product.ExpiryDate = pos.ExpiryDate;
                                    }

                                    product.BatchNumber = $"Партия_{DateTime.Now:yyyyMM}";
                                }
                            }
                            await _db.SaveChangesAsync();

                            await transaction.CommitAsync();
                        }
                        catch (Exception ex)
                        {
                            await transaction.RollbackAsync();
                            throw;
                        }
                    }

                    MessageBox.Show(
                        string.Format(Resources.SuccessSupplyFormat, supply.OrderNumber, supply.DateCreated.ToString("dd.MM.yyyy HH:mm"), positions.Count, displayTotalText),
                        Resources.TitleSuccess,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    Program.LogError("Ошибка при подтверждении поставки", ex);
                    MessageBox.Show(string.Format(Resources.ErrorSupplyFailed, ex.Message), Resources.TitleError,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    buttonConfirmSupply.Enabled = true;
                    buttonConfirmSupply.Text = Resources.ButtonConfirmSupply;
                }
            }
        }        
        
        /// <summary>
        /// Генерирует уникальный номер заказа
        /// </summary>
        private string GenerateOrderNumber()
        {
            return $"PO-{DateTime.Now:yyyyMMddHHmmss}-{new Random().Next(1000, 9999)}";
        }

        /// <summary>
        /// Возвращает идентификатор текущего авторизованного пользователя
        /// </summary>
        private Guid GetCurrentUserId()
        {
            if (Program.CurrentUser != null && Program.CurrentUser.Id != Guid.Empty)
            {
                return Program.CurrentUser.Id;
            }

            MessageBox.Show(Resources.ErrorUserNotAuthorized, Resources.TitleError,
                MessageBoxButtons.OK, MessageBoxIcon.Error);

            return Guid.Empty;
        }
    }
}