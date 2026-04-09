using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Windows.Forms;
using AutomechanicsProject.Classes;
using AutomechanicsProject.Properties;

namespace AutomechanicsProject.Formes
{
    /// <summary>
    /// Форма выбора валюты для отображения цен
    /// </summary>
    public partial class ChoosingCurrency : Form
    {
        private List<CurrencyInfo> currencies;
        private Dictionary<string, decimal> exchangeRates;
        private string selectedCurrencyCode;

        /// <summary>
        /// Код выбранной валюты (по умолчанию RUB)
        /// </summary>
        public static string SelectedCurrencyCode = "RUB";

        /// <summary>
        /// Текущий курс обмена относительно рубля
        /// </summary>
        public static decimal CurrentExchangeRate = 1m;

        /// <summary>
        /// Название выбранной валюты
        /// </summary>
        public static string SelectedCurrencyName = "Российский рубль";

        /// <summary>
        /// Инициализирует новый экземпляр формы выбора валюты
        /// </summary>
        public ChoosingCurrency()
        {
            InitializeComponent();

            this.textBoxCurrency.ReadOnly = true;
            this.textBoxCurrency.TabStop = false;
            this.textBoxCurrency.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBoxCurrency.DropDownStyle = ComboBoxStyle.DropDownList;

            LoadCurrenciesAsync();
        }

        /// <summary>
        /// Асинхронно загружает курсы валют из API
        /// </summary>
        private async void LoadCurrenciesAsync()
        {
            try
            {
                comboBoxCurrency.Items.Clear();
                comboBoxCurrency.Items.Add(Resources.LoadingCurrencies);
                comboBoxCurrency.Enabled = false;
                buttonChoose.Enabled = false;

                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(10);
                    var apiUrl = "https://open.er-api.com/v6/latest/RUB";

                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var rateResponse = JsonSerializer.Deserialize<ExchangeRateResponse>(json);

                        if (rateResponse != null && rateResponse.Rates != null && rateResponse.Rates.Count > 0)
                        {
                            exchangeRates = rateResponse.Rates;
                            PopulateCurrencyComboBox();
                            return;
                        }
                    }

                    Program.LogWarning($"Не удалось загрузить курсы валют из API. Используются резервные курсы.");
                    LoadFallbackRates();
                    PopulateCurrencyComboBox();
                }
            }
            catch (Exception ex)
            {
                Program.LogError($"Ошибка при загрузке курсов валют.", ex);
                MessageBox.Show(string.Format(Resources.ErrorLoadCurrencies, ex.Message),
                    Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LoadFallbackRates();
                PopulateCurrencyComboBox();
            }
        }

        /// <summary>
        /// Загружает резервные курсы валют при недоступности API
        /// </summary>
        private void LoadFallbackRates()
        {
            exchangeRates = new Dictionary<string, decimal>
            {
                { "RUB", 1.00m },
                { "USD", 0.011m },
                { "EUR", 0.010m },
                { "CNY", 0.079m },
                { "KZT", 4.90m },
                { "BYN", 0.036m },
                { "GBP", 0.0087m },
                { "JPY", 1.63m }
            };
        }

        /// <summary>
        /// Заполняет выпадающий список доступными валютами
        /// </summary>
        private void PopulateCurrencyComboBox()
        {
            comboBoxCurrency.Items.Clear();
            comboBoxCurrency.Enabled = true;
            buttonChoose.Enabled = true;

            currencies = new List<CurrencyInfo>();

            if (exchangeRates == null || exchangeRates.Count == 0)
            {
                comboBoxCurrency.Items.Add(Resources.NoCurrenciesAvailable);
                return;
            }

            foreach (var rate in exchangeRates)
            {
                var displayText = GetCurrencyDisplayText(rate.Key, rate.Value);
                comboBoxCurrency.Items.Add(displayText);
                currencies.Add(new CurrencyInfo
                {
                    Code = rate.Key,
                    Rate = rate.Value,
                    DisplayText = displayText
                });
            }

            for (int i = 0; i < currencies.Count; i++)
            {
                if (currencies[i].Code == SelectedCurrencyCode)
                {
                    comboBoxCurrency.SelectedIndex = i;
                    break;
                }
            }

            if (comboBoxCurrency.SelectedIndex == -1 && comboBoxCurrency.Items.Count > 0)
                comboBoxCurrency.SelectedIndex = 0;
        }

        /// <summary>
        /// Возвращает отображаемый текст для валюты
        /// </summary>
        private string GetCurrencyDisplayText(string code, decimal rate)
        {
            var name = GetCurrencyName(code);
            return $"{code} - {name} (1 RUB = {rate:F4} {code})";
        }

        /// <summary>
        /// Возвращает название валюты по её коду
        /// </summary>
        private string GetCurrencyName(string code)
        {
            switch (code)
            {
                case "RUB": return Resources.CurrencyRUB;
                case "USD": return Resources.CurrencyUSD;
                case "EUR": return Resources.CurrencyEUR;
                case "CNY": return Resources.CurrencyCNY;
                case "KZT": return Resources.CurrencyKZT;
                case "BYN": return Resources.CurrencyBYN;
                case "GBP": return Resources.CurrencyGBP;
                case "JPY": return Resources.CurrencyJPY;
                default: return code;
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки выбора валюты
        /// </summary>
        private void buttonChoose_Click(object sender, EventArgs e)
        {
            if (comboBoxCurrency.SelectedIndex >= 0 && comboBoxCurrency.SelectedIndex < currencies.Count)
            {
                var selected = currencies[comboBoxCurrency.SelectedIndex];
                var oldCurrency = SelectedCurrencyCode;
                var oldRate = CurrentExchangeRate;

                SelectedCurrencyCode = selected.Code;
                CurrentExchangeRate = selected.Rate;
                SelectedCurrencyName = GetCurrencyName(selected.Code);

                DialogResult result = MessageBox.Show(
                    string.Format(Resources.CurrencyChangeConfirm,
                        GetCurrencyName(oldCurrency),
                        SelectedCurrencyName,
                        CurrentExchangeRate,
                        SelectedCurrencyCode,
                        SelectedCurrencyCode),
                    Resources.CurrencyChangeTitle,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show(Resources.PleaseSelectCurrency, Resources.TitleWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки отмены
        /// </summary>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Конвертирует цену из рублей в выбранную валюту
        /// </summary>
        public static decimal ConvertPrice(decimal priceInRub)
        {
            if (SelectedCurrencyCode == "RUB")
                return priceInRub;

            return priceInRub * CurrentExchangeRate;
        }

        /// <summary>
        /// Форматирует цену с символом валюты
        /// </summary>
        public static string FormatPrice(decimal price)
        {
            var currencySymbol = GetCurrencySymbol(SelectedCurrencyCode);
            return $"{price:F2} {currencySymbol}";
        }

        /// <summary>
        /// Возвращает символ валюты по её коду
        /// </summary>
        private static string GetCurrencySymbol(string code)
        {
            switch (code)
            {
                case "RUB": return "₽";
                case "USD": return "$";
                case "EUR": return "€";
                case "GBP": return "£";
                case "JPY": return "¥";
                case "CNY": return "¥";
                case "KZT": return "₸";
                case "BYN": return "Br";
                default: return code;
            }
        }
    }
}