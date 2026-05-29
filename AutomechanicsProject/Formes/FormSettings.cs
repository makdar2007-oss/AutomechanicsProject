
using AutomechanicsProject.Classes;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Properties;
using AutomechanicsProject.Services.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace AutomechanicsProject.Formes
{
    /// <summary>
    /// Форма выбора валюты для отображения цен
    /// </summary>
    public partial class FormSettings : Form
    {
        private static string CacheFilePath => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "AutomechanicsProject",
        "currency_cache.json");
        private List<CurrencyInfo> currencies;
        private Dictionary<string, decimal> exchangeRates;
        private readonly ICurrencySettingsService _currencySettingsService;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Применяет текст из ресурсов к элементам формы
        /// </summary>
        private void ApplyLocalization()
        {
            Text = Resources.Settings_Form_Title;

            
            buttonChoose.Text = Resources.Settings_ButtonChoose_Text;
            buttonCancel.Text = Resources.Settings_ButtonCancel_Text;
        }

        public FormSettings(ICurrencySettingsService currencySettingsService)
        {
            InitializeComponent();
            ApplyLocalization();

            _currencySettingsService = currencySettingsService ?? throw new ArgumentNullException(nameof(currencySettingsService));



            TextBoxHelper.SetupWatermarkComboBox(comboBoxCurrency, Resources.Settings_CurrencyWatermark);
            TextBoxHelper.SetupWatermarkComboBox(comboBoxLanguage, Resources.Settings_LanguageWatermark);

            LoadLanguages();

            Load += FormSettings_Load;
        }

        /// <summary>
        /// Загружает данные при открытии формы
        /// </summary>
        private async void FormSettings_Load(object sender, EventArgs e)
        {
            await LoadCurrenciesAsync();
        }
        /// <summary>
        /// Асинхронно загружает курсы валют из API
        /// </summary>
        private async Task LoadCurrenciesAsync()
        {
            try
            {
                comboBoxCurrency.Items.Clear();
                comboBoxCurrency.Items.Add(Resources.LoadingCurrencies);
                comboBoxCurrency.Enabled = false;
                buttonChoose.Enabled = false;

                exchangeRates = await CurrencyHelper.GetExchangeRatesAsync();
                currencies = CreateCurrenciesFromRates(exchangeRates);

                PopulateCurrencyComboBox();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Ошибка при загрузке курсов валют");

                if (!LoadFromCache())
                {
                    LoadFallbackRates();
                }

                currencies = CreateCurrenciesFromRates(exchangeRates);

                PopulateCurrencyComboBox();

                MessageBox.Show(Resources.WarningCurrencyRatesFallback,
                    Resources.TitleWarning,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Загружает список языков
        /// </summary>
        private void LoadLanguages()
        {
            comboBoxLanguage.Items.Clear();

            comboBoxLanguage.Items.Add(Resources.Language_Russian);
            comboBoxLanguage.Items.Add(Resources.Language_English);
            comboBoxLanguage.Items.Add(Resources.Language_Chuvash);
            comboBoxLanguage.Items.Add(Resources.Language_Tatar);

            if (Settings.Default.SelectedLanguage == "en")
            {
                comboBoxLanguage.SelectedItem = Resources.Language_English;
            }
            else if (Settings.Default.SelectedLanguage == "cv")
            {
                comboBoxLanguage.SelectedItem = Resources.Language_Chuvash;
            }
            else if (Settings.Default.SelectedLanguage == "tt")
            {
                comboBoxLanguage.SelectedItem = Resources.Language_Tatar;
            }
            else
            {
                comboBoxLanguage.SelectedItem = Resources.Language_Russian;
            }
        }

        /// <summary>
        /// Сохраняет курсы валют в файл кэша
        /// </summary>
        private void SaveToCache(Dictionary<string, decimal> rates)
        {
            CurrencyStorage.SaveRates(rates, _currencySettingsService.SelectedCurrencyCode);
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
                logger.Error(ex, "Ошибка загрузки кэша курсов валют");
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
        /// Создает список валют из курсов
        /// </summary>
        private List<CurrencyInfo> CreateCurrenciesFromRates(Dictionary<string, decimal> rates)
        {
            var result = new List<CurrencyInfo>();

            if (rates == null)
            {
                return result;
            }

            foreach (var rate in rates)
            {
                result.Add(new CurrencyInfo
                {
                    Code = rate.Key,
                    Rate = rate.Value,
                    DisplayText = $"{rate.Key} - {CurrencyHelper.GetCurrencyName(rate.Key)} (1 RUB = {rate.Value:F4} {rate.Key})"
                });
            }

            return result;
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

            for (var i = 0; i < currencies.Count; i++)
            {
                if (currencies[i].Code == _currencySettingsService.SelectedCurrencyCode)
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
        /// Обработчик нажатия кнопки выбора валюты
        /// </summary>
        private void buttonChoose_Click(object sender, EventArgs e)
        {
            if (comboBoxCurrency.SelectedIndex < 0 || comboBoxCurrency.SelectedIndex >= currencies.Count)
            {
                MessageBox.Show(Resources.PleaseSelectCurrency, Resources.TitleWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            var selected = currencies[comboBoxCurrency.SelectedIndex];
            var selectedLanguageName = comboBoxLanguage.SelectedItem?.ToString();
            var selectedCurrencyName = CurrencyHelper.GetCurrencyName(selected.Code);

            _currencySettingsService.SetCurrency(
                selected.Code,
                selectedCurrencyName,
                selected.Rate);

            if (exchangeRates != null)
            {
                SaveToCache(exchangeRates);
            }

            Settings.Default.SelectedCurrency = selected.Code;
            Settings.Default.ExchangeRate = selected.Rate;
            SaveLanguage();
            Settings.Default.Save();

            MessageBox.Show(
                string.Format(Resources.CurrencyChangeConfirm,
                    selectedLanguageName,
                    selectedCurrencyName,
                    selected.Rate,
                    selected.Code),
                Resources.CurrencyChangeTitle,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            ApplySelectedLanguage();
            DialogResult = DialogResult.OK;
            Close();
        }
        /// <summary>
        /// Сохраняет выбранный язык
        /// </summary>
        private void SaveLanguage()
        {
            var selectedLanguage = comboBoxLanguage.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(selectedLanguage) ||
                selectedLanguage == Resources.Settings_LanguageWatermark)
            {
                return;
            }

            if (selectedLanguage == Resources.Language_English)
            {
                Settings.Default.SelectedLanguage = "en";
            }
            else if (selectedLanguage == Resources.Language_Chuvash)
            {
                Settings.Default.SelectedLanguage = "cv";
            }
            else if (selectedLanguage == Resources.Language_Tatar)
            {
                Settings.Default.SelectedLanguage = "tt";
            }
            else
            {
                Settings.Default.SelectedLanguage = "ru";
            }
        }
        /// <summary>
        /// Применяет сохраненный язык интерфейса
        /// </summary>
        private void ApplySelectedLanguage()
        {
            var culture = new CultureInfo(Settings.Default.SelectedLanguage);

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }
        /// <summary>
        /// Обработчик нажатия кнопки отмены
        /// </summary>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

    }
}