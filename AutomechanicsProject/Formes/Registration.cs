using AutomechanicsProject.Classes;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Properties;
using AutomechanicsProject.Services;
using AutomechanicsProject.Services.Interfaces;
using NLog;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace AutomechanicsProject.Formes
{
    /// <summary>
    /// Форма регистрации нового кладовщика в системе
    /// </summary>
    public partial class Registration : Form
    {
        private readonly IAuthService _authService;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ISupplyService _supplyService;
        private readonly IReportService _reportService;
        private readonly IShipmentService _shipmentService;
        private readonly IExpiredProductsService _expiredProductsService;
        private readonly ISupplyCurrencyService _supplyCurrencyService;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICurrencySettingsService _currencySettingsService;
        private readonly IWarehouseHeatmapService _warehouseHeatmapService;
        private readonly IDaDataService _daDataService;
        private readonly IWeatherService _weatherService;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// Применяет текст из ресурсов к элементам формы
        /// </summary>
        private void ApplyLocalization()
        {
            Text = Resources.Registration_Form_Title;

            labelRegistration.Text = Resources.Registration_LabelTitle_Text;
            labelSurname.Text = Resources.Registration_LabelSurname_Text;
            labelName.Text = Resources.Registration_LabelName_Text;
            labelLastname.Text = Resources.Registration_LabelLastname_Text;
            labelLogin.Text = Resources.Registration_LabelLogin_Text;
            labelPassword.Text = Resources.Registration_LabelPassword_Text;
            labelAgreePassword.Text = Resources.Registration_LabelConfirmPassword_Text;

            buttonRegistration.Text = Resources.Registration_ButtonRegister_Text;
            buttonEnter.Text = Resources.Registration_ButtonEnter_Text;
        }

        /// <summary>
        /// Инициализирует новый экземпляр формы регистрации
        /// </summary>
        public Registration(
            IAuthService authService,
            IProductService productService,
            ICategoryService categoryService,
            ISupplyService supplyService,
            IReportService reportService,
            IShipmentService shipmentService,
            IExpiredProductsService expiredProductsService,
            ISupplyCurrencyService supplyCurrencyService,
            ICurrentUserService currentUserService,
            ICurrencySettingsService currencySettingsService,
            IWarehouseHeatmapService warehouseHeatmapService,
            IDaDataService daDataService,
            IWeatherService weatherService)
        {
            InitializeComponent();
            ApplyLocalization();

            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _supplyService = supplyService ?? throw new ArgumentNullException(nameof(supplyService));
            _reportService = reportService ?? throw new ArgumentNullException(nameof(reportService));
            _shipmentService = shipmentService ?? throw new ArgumentNullException(nameof(shipmentService));
            _expiredProductsService = expiredProductsService ?? throw new ArgumentNullException(nameof(expiredProductsService));
            _supplyCurrencyService = supplyCurrencyService ?? throw new ArgumentNullException(nameof(supplyCurrencyService));
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
            _currencySettingsService = currencySettingsService ?? throw new ArgumentNullException(nameof(currencySettingsService));
            _warehouseHeatmapService = warehouseHeatmapService ?? throw new ArgumentNullException(nameof(warehouseHeatmapService));
            _daDataService = daDataService ?? throw new ArgumentNullException(nameof(daDataService));
            _weatherService = weatherService ?? throw new ArgumentNullException(nameof(weatherService));

            TextBoxHelper.SetupWatermarkTextBox(textBoxSurname, Resources.RegSurnameWatermark);
            TextBoxHelper.SetupWatermarkTextBox(textBoxName, Resources.RegNameWatermark);
            TextBoxHelper.SetupWatermarkTextBox(textBoxLastname, Resources.RegLastnameWatermark);
            TextBoxHelper.SetupWatermarkTextBox(textBoxLogin, Resources.RegLoginWatermark);
            TextBoxHelper.SetupPasswordTextBox(textBoxPassword, Resources.RegPasswordWatermark);
            TextBoxHelper.SetupPasswordTextBox(textBoxAgreePassword, Resources.RegConfirmPasswordWatermark);
        }

        /// <summary>
        /// Обработчик нажатия кнопки регистрации
        /// Выполняет регистрацию нового пользователя в системе
        /// </summary>
        private void buttonRegistration_Click(object sender, EventArgs e)
        {
            try
            {
                string errorMessage;

                var isValid = Validation.ValidateRegistrationFields(
                    textBoxSurname.Text, Resources.RegSurnameWatermark,
                    textBoxName.Text, Resources.RegNameWatermark,
                    textBoxLogin.Text, Resources.RegLoginWatermark,
                    textBoxPassword.Text, Resources.RegPasswordWatermark,
                    textBoxAgreePassword.Text, Resources.RegConfirmPasswordWatermark,
                    out errorMessage,
                    textBoxLastname.Text, Resources.RegLastnameWatermark
                );

                if (!isValid)
                {
                    MessageBox.Show(errorMessage, Resources.TitleWarning,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var login = textBoxLogin.Text.Trim();

                var lastname = Validation.IsWatermark(textBoxLastname.Text, Resources.RegLastnameWatermark)
                    ? string.Empty
                    : textBoxLastname.Text.Trim();

                _authService.Register(
                    textBoxSurname.Text.Trim(),
                    textBoxName.Text.Trim(),
                    lastname,
                    login,
                    textBoxPassword.Text);

                MessageBox.Show(
                    string.Format(Resources.SuccessRegistrationWithDetails, login,
                        $"{textBoxSurname.Text.Trim()} {textBoxName.Text.Trim()} {lastname}".Trim()),
                    Resources.TitleSuccess,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                OpenAuthorization();
            }
            catch (Exception ex)
            {
                FormHelper.HandleException(Resources.ErrorRegistration, ex, this);
            }
        }
        /// <summary>
        /// Открывает форму авторизации и закрывает текущую форму
        /// </summary>
        private void OpenAuthorization()
        {
            var authForm = new Autorization(
                _authService,
                _productService,
                _categoryService,
                _supplyService,
                _reportService,
                _shipmentService,
                _expiredProductsService,
                _supplyCurrencyService,
                _currentUserService,
                _currencySettingsService,
                _warehouseHeatmapService,
                _daDataService,
                _weatherService);
            authForm.Show();
            Close();
        }

        /// <summary>
        /// Обработчик нажатия кнопки Уже есть аккаунт? Войти
        /// </summary>
        private void ButtonEnter_Click(object sender, EventArgs e)
        {
            OpenAuthorization();
        }

        /// <summary>
        /// Обработчик изменения текста в поле фамилии
        /// </summary>
        private void textBoxSurname_TextChanged(object sender, EventArgs e)
        {
            Validation.HighlightError(
                textBoxSurname,
                !Validation.IsWatermark(textBoxSurname.Text, Resources.RegSurnameWatermark) &&
                (!Validation.IsValidRussianName(textBoxSurname.Text) ||
                 !Validation.IsValidNameLength(textBoxSurname.Text))
            );
        }

        /// <summary>
        /// Обработчик изменения текста в поле имени
        /// </summary>
        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            Validation.HighlightError(
                textBoxName,
                !Validation.IsWatermark(textBoxName.Text, Resources.RegNameWatermark) &&
                (!Validation.IsValidRussianName(textBoxName.Text) ||
                 !Validation.IsValidNameLength(textBoxName.Text))
            );
        }

        /// <summary>
        /// Обработчик изменения текста в поле отчества
        /// </summary>
        private void textBoxLastname_TextChanged(object sender, EventArgs e)
        {
            Validation.HighlightError(
                textBoxLastname,
                !Validation.IsWatermark(textBoxLastname.Text, Resources.RegLastnameWatermark) &&
                !string.IsNullOrWhiteSpace(textBoxLastname.Text) &&
                (!Validation.IsValidRussianName(textBoxLastname.Text) ||
                 !Validation.IsValidNameLength(textBoxLastname.Text))
            );
        }

        /// <summary>
        /// Обработчик изменения текста в поле логина
        /// </summary>
        private void textBoxLogin_TextChanged(object sender, EventArgs e)
        {
            Validation.HighlightError(
                textBoxLogin,
                !Validation.IsWatermark(textBoxLogin.Text, Resources.RegLoginWatermark) &&
                (!Validation.IsValidLogin(textBoxLogin.Text) ||
                 !Validation.IsValidLoginLength(textBoxLogin.Text))
            );
        }

        /// <summary>
        /// Обработчик изменения текста в поле пароля
        /// </summary>
        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {
            Validation.HighlightError(
                textBoxPassword,
                !Validation.IsWatermark(textBoxPassword.Text, Resources.RegPasswordWatermark) &&
                !Validation.IsValidPassword(textBoxPassword.Text)
            );

            if (!Validation.IsWatermark(textBoxAgreePassword.Text, Resources.RegConfirmPasswordWatermark))
            {
                Validation.HighlightError(
                    textBoxAgreePassword,
                    textBoxPassword.Text != textBoxAgreePassword.Text
                );
            }
        }

        /// <summary>
        /// Обработчик изменения текста в поле подтверждения пароля
        /// Выполняет проверку совпадения паролей в реальном времени
        /// </summary>
        private void textBoxAgreePassword_TextChanged(object sender, EventArgs e)
        {
            Validation.HighlightError(
                textBoxAgreePassword,
                !Validation.IsWatermark(textBoxAgreePassword.Text, Resources.RegConfirmPasswordWatermark) &&
                textBoxPassword.Text != textBoxAgreePassword.Text
            );

            if (!Validation.IsWatermark(textBoxPassword.Text, Resources.RegPasswordWatermark))
            {
                Validation.HighlightError(
                    textBoxPassword,
                    textBoxPassword.Text != textBoxAgreePassword.Text &&
                    !Validation.IsWatermark(textBoxAgreePassword.Text, Resources.RegConfirmPasswordWatermark)
                );
            }
        }
    }
}