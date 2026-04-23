using AutomechanicsProject.Classes;
using AutomechanicsProject.Formes;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Properties;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AutomechanicsProject
{
    /// <summary>
    /// Форма авторизации пользователя в системе
    /// </summary>
    public partial class Autorization : Form
    {
        private readonly DateBase _db;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Инициализирует новый экземпляр формы авторизации
        /// </summary>
        public Autorization(DateBase database)
        {
            InitializeComponent();
            _db = database ?? throw new ArgumentNullException(nameof(database));
            TextBoxHelper.SetupWatermarkTextBox(textBoxLogin, Resources.AuthLoginWatermark);
            TextBoxHelper.SetupPasswordTextBox(textBoxPassword, Resources.AuthPasswordWatermark);
        }

        /// <summary>
        /// Обработчик нажатия клавиш в текстовых полях
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
        /// Обработчик перехода на форму регистрации
        /// </summary>
        private void linkRegisterClick(object sender, EventArgs e)
        {
            new Registration(_db).Show();
            Hide();
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Войти"
        /// Выполняет аутентификацию пользователя и открывает соответствующую главную форму
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
                var user = _db.Users
                    .Include(u => u.Role)
                    .FirstOrDefault(u => u.Login == textBoxLogin.Text);

                if (user == null)
                {
                    Validation.HighlightError(textBoxLogin, true);

                    MessageBox.Show(Resources.ErrorAuthInvalid, Resources.TitleWarning,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool isValid;

                if (user.Password.StartsWith("$2"))
                {
                    isValid = PasswordHelper.VerifyPassword(textBoxPassword.Text, user.Password);
                }
                else
                {
                    isValid = textBoxPassword.Text == user.Password;

                    if (isValid)
                    {
                        user.Password = PasswordHelper.HashPassword(textBoxPassword.Text);
                        _db.SaveChanges();
                        logger.Info($"Пароль для пользователя {user.Login} был хеширован");
                    }
                }

                if (!isValid)
                {
                    Validation.HighlightError(textBoxPassword, true);

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
        /// Открывает главную форму в зависимости от роли пользователя
        /// </summary>
        private void OpenMainForm(Users user)
        {
            Form nextForm = null;
            if (user.Role != null)
            {
                switch (user.Role.Type)
                {
                    case RoleType.Administrator:
                        nextForm = new AdminForm(_db);
                        logger.Info("Открыта форма администратора для {0}", user.FullName);
                        break;
                    case RoleType.Storekeeper:
                            nextForm = new StorekeeperForm(_db);
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
                logger.Warn($"Пользователь {user.FullName} не имеет назначенной роли");
            }
        }
    }
}