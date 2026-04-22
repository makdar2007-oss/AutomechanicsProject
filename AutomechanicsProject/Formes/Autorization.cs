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
        /// Проверяет заполнение обязательных полей
        /// </summary>
        private bool ValidateFields()
        {
            var isValid = true;

            if (Validation.IsWatermark(textBoxLogin.Text, Resources.AuthLoginWatermark))
            {
                textBoxLogin.BackColor = Color.LightPink;
                isValid = false;
            }
            if (Validation.IsWatermark(textBoxPassword.Text, Resources.AuthPasswordWatermark))
            {
                textBoxPassword.BackColor = Color.LightPink;
                isValid = false;
            }
            return isValid;
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
            textBoxLogin.BackColor = SystemColors.Window;
            textBoxPassword.BackColor = SystemColors.Window;

            if (!ValidateFields())
            {
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
                    textBoxLogin.BackColor = Color.LightPink;
                    MessageBox.Show(Resources.ErrorAuthInvalid, Resources.TitleWarning,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool isValid = false;
                if (user.Password.StartsWith("$2"))
                {
                    isValid = PasswordHelper.VerifyPassword(textBoxPassword.Text, user.Password);
                }
                else
                {
                    isValid = (textBoxPassword.Text == user.Password);
                    if (isValid)
                    {
                        user.Password = PasswordHelper.HashPassword(textBoxPassword.Text);
                        _db.SaveChanges();
                        logger.Info($"Пароль для польщователя {user.Login} был хеширован");
                    }
                }
                if (!isValid)
                {
                    textBoxPassword.BackColor = Color.LightPink;
                    MessageBox.Show(Resources.ErrorAuthInvalid, Resources.TitleWarning,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Program.CurrentUser = user;
                OpenMainForm(user);
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка авторизации", ex);
                MessageBox.Show(Resources.ErrorAuthFailed, Resources.TitleError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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