using AutomechanicsProject.Classes;
using AutomechanicsProject.Dtos.UI;
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
        private List<SupplyPosition> positions = new List<SupplyPosition>();
        private string currentCurrency = "RUB";
        private List<ProductDisplayItem> cachedProducts;
        private List<ComboItemDto> cachedSuppliers;
        private ToolTip productToolTip;
        private List<ProductComboDto> allProductsForSearch;
        private bool isClearingText = false;
        private bool isUpdatingText = false;

        public CreateSupply()
        {
            InitializeComponent();
            dateTimePickerExpiry.ShowCheckBox = true;
            dateTimePickerExpiry.Checked = false;
            dateTimePickerExpiry.MinDate = DateTime.Now.Date;
            DbContextManager.AddReference();
            LoadProductsFromDatabase();
            LoadSuppliersFromDatabase();
            SetupEvents();

        }

        /// <summary>
        /// Настраивает обработчики событий для элементов управления
        /// </summary>
        private void SetupEvents()
        {
            textBoxQuantity.KeyPress += ValidateNumberInput;
            textBoxPrice.KeyPress += ValidateDecimalInput;
            comboBoxProduct.TextUpdate += ComboBoxProduct_TextUpdate;
            comboBoxProduct.DropDown += ComboBoxProduct_DropDown;
            comboBoxProduct.KeyDown += ComboBoxProduct_KeyDown;
            comboBoxProduct.SelectionChangeCommitted += ComboBoxProduct_SelectionChangeCommitted;

            comboBoxProduct.SelectedIndexChanged += OnProductSelectedChanged;

            productToolTip = new ToolTip();  

            comboBoxProduct.MouseHover += (s, e) =>
            {
                if (comboBoxProduct.SelectedItem is ProductComboDto selectedProductDto)
                {
                    var product = cachedProducts.FirstOrDefault(p => p.Id == selectedProductDto.Id);
                    if (product != null)
                    {
                        var tooltipText = $"Артикул: {product.Article}\nНаименование: {product.Name}\nОстаток: {product.Balance} шт.";
                        if (!product.HasExpiryDate)
                        {
                            tooltipText += $" {Resources.WarningNoProductExpiryDate}";
                        }
                        else
                        {
                            tooltipText += $"\nСрок годности товара: {product.ProductExpiryDate.Value.ToShortDateString()}";
                        }
                        productToolTip.Show(tooltipText, comboBoxProduct, 5, comboBoxProduct.Height, 3000);
                    }
                }
            };

            comboBoxProduct.DropDownClosed += (s, e) => productToolTip.Hide(comboBoxProduct);
        }
        /// <summary>
        /// Обработчик изменения выбранного товара
        /// </summary>
        private void OnProductSelectedChanged(object sender, EventArgs e)
        {
            if (comboBoxProduct.SelectedItem != null)
            {
                var selectedProductDto = (ProductComboDto)comboBoxProduct.SelectedItem;
                var selectedProduct = cachedProducts.FirstOrDefault(p => p.Id == selectedProductDto.Id);

                if (selectedProduct != null)
                {
                    if (!selectedProduct.HasExpiryDate)
                    {
                        dateTimePickerExpiry.Enabled = false;
                        dateTimePickerExpiry.Checked = false;
                    }
                    else
                    {
                        dateTimePickerExpiry.Enabled = true;
                        dateTimePickerExpiry.Checked = true;
                    }
                }
            }
            else
            {
                dateTimePickerExpiry.Enabled = true;
                dateTimePickerExpiry.Checked = true;
            }
        }
        /// <summary>
        /// Обработчик выбора товара из списка
        /// </summary>
        private void ComboBoxProduct_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBoxProduct.SelectedItem != null)
            {
                isUpdatingText = true;
                var selectedProduct = (ProductComboDto)comboBoxProduct.SelectedItem;
                comboBoxProduct.Text = $"{selectedProduct.Article} - {selectedProduct.Name}";
                comboBoxProduct.SelectionStart = 0;
                comboBoxProduct.SelectionLength = 0;
                isUpdatingText = false;
            }
        }

        /// <summary>
        /// Обработчик нажатия клавиш в выпадающем списке
        /// </summary>
        private void ComboBoxProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete) && string.IsNullOrEmpty(comboBoxProduct.Text))
            {
                isClearingText = true;
                LoadAllProductsToComboBox();
                comboBoxProduct.Text = "";
                comboBoxProduct.SelectedIndex = -1;
                isClearingText = false;
            }
            else if (e.KeyCode == Keys.Enter && comboBoxProduct.SelectedItem != null)
            {
                e.SuppressKeyPress = true;
                isUpdatingText = true;
                var selectedProduct = (ProductComboDto)comboBoxProduct.SelectedItem;
                comboBoxProduct.Text = $"{selectedProduct.Article} - {selectedProduct.Name}";
                comboBoxProduct.SelectionStart = 0;
                comboBoxProduct.SelectionLength = 0;
                isUpdatingText = false;
            }
        }

        /// <summary>
        /// Обработчик раскрытия выпадающего списка
        /// </summary>
        private void ComboBoxProduct_DropDown(object sender, EventArgs e)
        {
            string searchText = comboBoxProduct.Text;
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                FilterProducts(searchText);
            }
        }

        /// <summary>
        /// Обработчик изменения текста в выпадающем списке
        /// </summary>
        private void ComboBoxProduct_TextUpdate(object sender, EventArgs e)
        {
            if (isClearingText || isUpdatingText)
                return;

            string searchText = comboBoxProduct.Text;

            if (string.IsNullOrWhiteSpace(searchText))
            {
                LoadAllProductsToComboBox();
                comboBoxProduct.Text = "";
                return;
            }

            FilterProducts(searchText);
            comboBoxProduct.DroppedDown = true;
        }

        /// <summary>
        /// Фильтрует товары и показывает все совпадения
        /// </summary>
        private void FilterProducts(string searchText)
        {
            if (isClearingText || isUpdatingText)
                return;

            var filteredProducts = allProductsForSearch
                .Where(p => p.Name.ToLower().Contains(searchText.ToLower()) ||
                           p.Article.ToLower().Contains(searchText.ToLower()))
                .ToList();

            string currentText = comboBoxProduct.Text;

            comboBoxProduct.DataSource = null;
            comboBoxProduct.DisplayMember = "Text";
            comboBoxProduct.ValueMember = "Id";
            comboBoxProduct.DataSource = filteredProducts;

            comboBoxProduct.Text = currentText;
            comboBoxProduct.SelectionStart = comboBoxProduct.Text.Length;
        }

        /// <summary>
        /// Загружает все товары в комбобокс
        /// </summary>
        private void LoadAllProductsToComboBox()
        {
            if (allProductsForSearch != null && allProductsForSearch.Any())
            {
                comboBoxProduct.DataSource = null;
                comboBoxProduct.DisplayMember = "Text";
                comboBoxProduct.ValueMember = "Id";
                comboBoxProduct.DataSource = allProductsForSearch;
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
                var db = DbContextManager.GetContext();

                cachedProducts = db.Products
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
                    .Select(p => new ProductComboDto
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
                    comboBoxProduct.DisplayMember = "Text";
                    comboBoxProduct.ValueMember = "Id";
                    comboBoxProduct.DataSource = allProductsForSearch;
                    comboBoxProduct.SelectedIndex = -1;
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
                var db = DbContextManager.GetContext();

                var suppliers = db.Suppliers
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

            var selectedProductDto = (ProductComboDto)comboBoxProduct.SelectedItem;
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
            comboBoxProduct.SelectedIndex = -1;
        }

        /// <summary>
        /// Обновляет общую сумму поставки
        /// </summary>
        private void UpdateTotalAmount()
        {
            decimal total = positions.Sum(p => p.Quantity * p.Price);
            labelTotalValue.Text = $"{total:N2} RUB";
        }

        /// <summary>
        /// Обновляет отображение цен в таблице при смене валюты
        /// </summary>
        private void UpdatePricesInGrid()
        {
            for (int i = 0; i < dataGridViewSupply.Rows.Count; i++)
            {
                if (dataGridViewSupply.Rows[i].Cells["colPrice"].Value != null)
                {
                    string priceStr = dataGridViewSupply.Rows[i].Cells["colPrice"].Value.ToString().Split(' ')[0];
                    dataGridViewSupply.Rows[i].Cells["colPrice"].Value = $"{priceStr} {currentCurrency}";

                    if (dataGridViewSupply.Rows[i].Cells["colTotal"].Value != null)
                    {
                        string totalStr = dataGridViewSupply.Rows[i].Cells["colTotal"].Value.ToString().Split(' ')[0];
                        dataGridViewSupply.Rows[i].Cells["colTotal"].Value = $"{totalStr} {currentCurrency}";
                    }
                }
            }
        }

        /// <summary>
        /// Добавляет выбранную позицию в список поставки
        /// </summary>
        private void ButtonAddToList_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            var selectedProductDto = (ProductComboDto)comboBoxProduct.SelectedItem;
            var selectedItem = cachedProducts.FirstOrDefault(p => p.Id == selectedProductDto.Id);
            var selectedSupplier = (ComboItemDto)comboBoxSupplier.SelectedItem;
            int quantity = int.Parse(textBoxQuantity.Text);
            var price = decimal.Parse(textBoxPrice.Text.Replace('.', ','));

            DateTime? supplyExpiryDate = null;
            if (selectedItem.HasExpiryDate)
            {
                supplyExpiryDate = dateTimePickerExpiry.Value;
            }

            SupplyPosition position = new SupplyPosition
            {
                Id = Guid.NewGuid(),
                ProductId = selectedItem.Id,
                ProductName = selectedItem.Name,
                Article = selectedItem.Article,
                Quantity = quantity,
                Price = price,
                SupplyId = Guid.Empty,
                SupplierId = selectedSupplier.Id,
                SupplierName = selectedSupplier.Text,
                ExpiryDate = supplyExpiryDate
            };

            positions.Add(position);

            dataGridViewSupply.Rows.Add(
                position.Article,
                position.ProductName,
                position.Quantity,
                $"{position.Price:N2} RUB",
                $"{position.Quantity * position.Price:N2} RUB",
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
                                comboBoxCurrency.SelectedIndex = index;
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
                                supplier = cachedSuppliers[0];
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
                                    expiryDate = dateTimePickerExpiry.Value;
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

            DialogResult confirmResult = MessageBox.Show(
                string.Format(Resources.ConfirmSupplyFormat, labelTotalValue.Text),
                Resources.TitleConfirmation,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                buttonConfirmSupply.Enabled = false;
                buttonConfirmSupply.Text = Resources.StatusSaving;

                try
                {
                    var db = DbContextManager.GetContext();

                    using (var transaction = await db.Database.BeginTransactionAsync())
                    {
                        try
                        {
                            Guid currentUserId = GetCurrentUserId();

                            Supply supply = new Supply
                            {
                                Id = Guid.NewGuid(),
                                OrderNumber = GenerateOrderNumber(),
                                DateCreated = DateTime.Now,
                                UserId = currentUserId,
                                Status = Resources.SupplyStatusCompleted,
                                TotalAmount = positions.Sum(p => p.Quantity * p.Price)
                            };
                            db.Supplies.Add(supply);
                            await db.SaveChangesAsync();

                            foreach (var pos in positions)
                            {
                                db.SupplyPositions.Add(new SupplyPosition
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
                                });

                                var product = await db.Products.FindAsync(pos.ProductId);
                                if (product != null)
                                {
                                    product.Balance += pos.Quantity;

                                    if (product.Price == 0 || pos.Price > 0)
                                    {
                                        product.Price = pos.Price;
                                    }


                                }
                            }

                            await db.SaveChangesAsync();
                            await transaction.CommitAsync();

                            MessageBox.Show(
                                string.Format(Resources.SuccessSupplyFormat, supply.OrderNumber, supply.DateCreated.ToString("dd.MM.yyyy HH:mm"), positions.Count, labelTotalValue.Text),
                                Resources.TitleSuccess,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            await transaction.RollbackAsync();
                            throw;
                        }
                    }
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

        /// <summary>
        /// Обработчик закрытия формы - освобождает ресурсы контекста БД
        /// </summary>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            DbContextManager.ReleaseReference();
            base.OnFormClosed(e);
        }

        
    }
}