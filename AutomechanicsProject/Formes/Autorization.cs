using AutomechanicsProject.Classes;
using AutomechanicsProject.Formes;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Properties;
using AutomechanicsProject.Services;
using AutomechanicsProject.Services.Interfaces;
using NLog;
using System;
using System.Windows.Forms;

namespace AutomechanicsProject
{
    /// <summary>
    /// Форма авторизации пользователя в системе
    /// </summary>
    public partial class Autorization : Form
    {
        /// <summary>
        /// Сервис авторизации
        /// </summary>
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

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Конструктор
        /// </summary>
        public Autorization(
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
             IWarehouseHeatmapService warehouseHeatmapService)
        {
            InitializeComponent();

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

            TextBoxHelper.SetupWatermarkTextBox(textBoxLogin, Resources.AuthLoginWatermark);
            TextBoxHelper.SetupPasswordTextBox(textBoxPassword, Resources.AuthPasswordWatermark);
        }

        /// <summary>
        /// Нажатие Enter
        /// </summary>
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                BtnLoginClick(sender, e);
            }
        }

        /// <summary>
        /// Переход на регистрацию
        /// </summary>
        private void linkRegisterClick(object sender, EventArgs e)
        {
            var form = new Registration(
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
                _warehouseHeatmapService);
            form.Show();
            Hide();
        }

        /// <summary>
        /// Нажатие кнопки "Войти"
        /// </summary>
        private void BtnLoginClick(object sender, EventArgs e)
        {
            Validation.ResetHighlights(textBoxLogin, textBoxPassword);

            if (!Validation.ValidateRequiredFields(
                (textBoxLogin.Text, Resources.AuthLoginWatermark),
                (textBoxPassword.Text, Resources.AuthPasswordWatermark)
            ))
            {
                Validation.HighlightError(textBoxLogin,
                    Validation.IsWatermark(textBoxLogin.Text, Resources.AuthLoginWatermark));

                Validation.HighlightError(textBoxPassword,
                    Validation.IsWatermark(textBoxPassword.Text, Resources.AuthPasswordWatermark));

                MessageBox.Show(Resources.ErrorFillFields, Resources.TitleWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var user = _authService.Login(textBoxLogin.Text, textBoxPassword.Text);

                if (user == null)
                {
                    Validation.HighlightError(textBoxLogin, true);

                    MessageBox.Show(Resources.ErrorAuthInvalid, Resources.TitleWarning,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _currentUserService.SetCurrentUser(user);

                OpenMainForm(user);
            }
            catch (Exception ex)
            {
                FormHelper.HandleException("Ошибка авторизации", ex);
            }
        }

        /// <summary>
        /// Открывает главную форму
        /// </summary>
        private void OpenMainForm(Users user)
        {
            Form nextForm = null;

            if (user.Role != null)
            {
                switch (user.Role.Type)
                {
                    case RoleType.Administrator:
                        nextForm = new AdminForm(
                            _productService,
                            _categoryService,
                            _authService,
                            _supplyService,
                            _reportService,
                            _shipmentService,
                            _expiredProductsService,
                            _supplyCurrencyService,
                            _currentUserService,
                            _currencySettingsService,
                            _warehouseHeatmapService);
                        logger.Info("Открыта форма администратора для {0}", user.FullName);
                        break;

                    case RoleType.Storekeeper:
                        nextForm = new StorekeeperForm(
                            _productService,
                            _categoryService,
                            _authService,
                            _shipmentService,
                            _supplyService,
                            _reportService,
                            _expiredProductsService,
                            _supplyCurrencyService,
                            _currentUserService,
                            _currencySettingsService,
                            _warehouseHeatmapService);
                        logger.Info("Открыта форма кладовщика для {0}", user.FullName);
                        break;
                }
            }

            if (nextForm != null)
            {
                nextForm.Show();
                Hide();
            }
            else
            {
                MessageBox.Show(Resources.ErrorNoAccess, Resources.TitleError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                logger.Warn($"Пользователь {user.FullName} не имеет роли");
            }
        }

        
    }
}