using AutomechanicsProject.Classes;
using AutomechanicsProject.Properties;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutomechanicsProject.Formes
{
    /// <summary>
    /// Форма выбора валюты для отображения цен
    /// </summary>
    public partial class ChoosingCurrency : Form
    {
        private static string CacheFilePath => Path.Combine(
       Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
       "AutomechanicsProject",
       "currency_cache.json");
        private List<CurrencyInfo> currencies;
        private Dictionary<string, decimal> exchangeRates;
        public static string SelectedCurrencyCode = CurrencyCodes.RUB;
        public static decimal CurrentExchangeRate = 1m;
        public static string SelectedCurrencyName = "Российский рубль";
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public ChoosingCurrency()
        {
            InitializeComponent();
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

                bool apiSuccess = await TryLoadFromApi();

                if (!apiSuccess)
                {
                    if (LoadFromCache())
                    {
                        logger.Info("Загружены сохраненные курсы валют");
                        MessageBox.Show(Resources.InfoUsingCachedCurrencies, Resources.TitleInformation,
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        LoadFallbackRates();
                        logger.Warn("Нет сохраненныйх курсов, используются резервные");
                    }
                }

                PopulateCurrencyComboBox();
            }
            catch (Exception)
            {
                logger.Error("Ошибка при загрузке курсов валют");

                if (!LoadFromCache())
                {
                    LoadFallbackRates();
                }

                PopulateCurrencyComboBox();
            }
        }

        /// <summary>
        /// Асинхронно пытается загрузить курсы валют из внешнего API
        /// </summary>
        private async Task<bool> TryLoadFromApi()
        {
            try
            {
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
                            SaveToCache(exchangeRates);
                            return true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                logger.Error("Ошибка API при загрузке курсов");
            }
            return false;
        }

        /// <summary>
        /// Сохраняет курсы валют в файл кэша
        /// </summary>
        private void SaveToCache(Dictionary<string, decimal> rates)
        {
            try
            {
                var directory = Path.GetDirectoryName(CacheFilePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var cache = new
                {
                    ExchangeRates = rates,
                    SelectedCurrency = SelectedCurrencyCode,
                    LastUpdate = DateTime.Now
                };

                var json = JsonSerializer.Serialize(cache);
                File.WriteAllText(CacheFilePath, json);
            }
            catch (Exception)
            {
                logger.Error("Ошибка сохранения валют в кэш");
            }
        }

        /// <summary>
        /// Работа с кэшем
        /// </summary>
        private bool LoadFromCache()
        {
            try
            {
                if (File.Exists(CacheFilePath))
                {
                    var json = File.ReadAllText(CacheFilePath);
                    var cache = JsonSerializer.Deserialize<Dictionary<string, object>>(json);

                    if (cache != null && cache.ContainsKey("ExchangeRates"))
                    {
                        var ratesJson = JsonSerializer.Serialize(cache["ExchangeRates"]);
                        exchangeRates = JsonSerializer.Deserialize<Dictionary<string, decimal>>(ratesJson);
                        return exchangeRates != null && exchangeRates.Count > 0;
                    }
                }
            }
            catch (Exception)
            {
                logger.Error("Ошибка загрузки кэша курсов валют");
            }
            return false;
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
            {
                comboBoxCurrency.SelectedIndex = 0;
            }
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

                if (exchangeRates != null)
                {
                    SaveToCache(exchangeRates);
                }

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
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    SelectedCurrencyCode = oldCurrency;
                    CurrentExchangeRate = oldRate;
                    SelectedCurrencyName = GetCurrencyName(oldCurrency);
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
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Конвертирует цену из рублей в выбранную валюту
        /// </summary>
        public static decimal ConvertPrice(decimal priceInRub)
        {
            if (SelectedCurrencyCode == CurrencyCodes.RUB)
            {
                return priceInRub;
            }

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