using AutomechanicsProject.Classes;
using AutomechanicsProject.Formes;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Properties;
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

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Конструктор
        /// </summary>
        public Autorization(IAuthService authService)
        {
            InitializeComponent();

            _authService = authService ?? throw new ArgumentNullException(nameof(authService));

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
            var form = Program.Container.Resolve<Registration>();
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

                Program.CurrentUser = user;

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
                        nextForm = Program.Container.Resolve<AdminForm>();
                        logger.Info("Открыта форма администратора для {0}", user.FullName);
                        break;

                    case RoleType.Storekeeper:
                        nextForm = Program.Container.Resolve<StorekeeperForm>();
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