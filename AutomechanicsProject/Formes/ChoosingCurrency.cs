
using AutomechanicsProject.Classes;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Properties;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
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
            LoadCurrencies();
        }

        /// <summary>
        /// Загружает курсы валют из API 
        /// </summary>
        private void LoadCurrencies()
        {
            try
            {
                comboBoxCurrency.Items.Clear();
                comboBoxCurrency.Items.Add(Resources.LoadingCurrencies);
                comboBoxCurrency.Enabled = false;
                buttonChoose.Enabled = false;

                currencies = CurrencyHelper.GetCurrenciesFromApi();

                if (currencies == null || currencies.Count == 0)
                {
                    currencies = CurrencyHelper.GetFallbackCurrencies();
                    logger.Warn("Используются резервные курсы валют");
                    MessageBox.Show(Resources.WarningCurrencyRatesFallback,
                        Resources.TitleWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                PopulateCurrencyComboBox();
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка при загрузке курсов валют", ex);

                if (!LoadFromCache())
                {
                    LoadFallbackRates();
                }

                PopulateCurrencyComboBox();
            }
        }

        /// <summary>
        /// Сохраняет курсы валют в файл кэша
        /// </summary>
        private void SaveToCache(Dictionary<string, decimal> rates)
        {
            CurrencyStorage.SaveRates(rates, SelectedCurrencyCode);
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
            catch (Exception ex)
            {
                logger.Error("Ошибка загрузки кэша курсов валют", ex);
            }
            return false;
        }

        /// <summary>
        /// Загружает резервные курсы валют при недоступности API
        /// </summary>
        private void LoadFallbackRates()
        {
            exchangeRates = CurrencyHelper.GetFallbackRates();
        }

        /// <summary>
        /// Заполняет выпадающий список доступными валютами
        /// </summary>
        private void PopulateCurrencyComboBox()
        {
            comboBoxCurrency.Items.Clear();
            comboBoxCurrency.Enabled = true;
            buttonChoose.Enabled = true;

            if (currencies == null || currencies.Count == 0)
            {
                comboBoxCurrency.Items.Add(Resources.NoCurrenciesAvailable);
                return;
            }

            foreach (var currency in currencies)
            {
                comboBoxCurrency.Items.Add(currency.DisplayText);
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
            var name = CurrencyHelper.GetCurrencyName(code);
            return $"{code} - {name} (1 RUB = {rate:F4} {code})";
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
                SelectedCurrencyName = CurrencyHelper.GetCurrencyName(selected.Code);

                if (exchangeRates != null)
                {
                    SaveToCache(exchangeRates);
                }

                DialogResult result = MessageBox.Show(
                    string.Format(Resources.CurrencyChangeConfirm,
                        CurrencyHelper.GetCurrencyName(oldCurrency),
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
                    SelectedCurrencyName = CurrencyHelper.GetCurrencyName(oldCurrency);
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