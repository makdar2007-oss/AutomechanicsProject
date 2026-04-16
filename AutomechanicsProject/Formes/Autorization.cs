using AutomechanicsProject.Classes;
using AutomechanicsProject.Formes;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Properties;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using AutomechanicsProject.Dtos;

namespace AutomechanicsProject
{
    /// <summary>
    /// Форма авторизации пользователя в системе
    /// </summary>
    public partial class Autorization : Form
    {
        /// <summary>
        /// Инициализирует новый экземпляр формы авторизации
        /// </summary>
        public Autorization()
        {
            InitializeComponent();
            TextBoxHelper.SetupWatermarkTextBox(textBoxLogin, Resources.AuthLoginWatermark);
            TextBoxHelper.SetupPasswordTextBox(textBoxPassword, Resources.AuthPasswordWatermark);

            textBoxLogin.KeyDown += TextBox_KeyDown;
            textBoxPassword.KeyDown += TextBox_KeyDown;
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
        /// Сбрасывает подсветку полей ввода
        /// </summary>
        private void ResetHighlights()
        {
            textBoxLogin.BackColor = Color.White;
            textBoxPassword.BackColor = Color.White;
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
            new Registration().Show();
            Hide();
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Войти"
        /// Выполняет аутентификацию пользователя и открывает соответствующую главную форму
        /// </summary>
        private void BtnLoginClick(object sender, EventArgs e)
        {
            ResetHighlights();
            if (!ValidateFields())
            {
                MessageBox.Show(Resources.ErrorFillFields, Resources.TitleWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using (var db = new DateBase())
                {
                    var user = db.Users
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
                            db.SaveChanges();
                            Program.LogInfo($"Пароль для пользователя {user.Login} был хеширован");
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
            }
            catch (Exception ex)
            {
                Program.LogError("Ошибка авторизации", ex);
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
                        nextForm = new AdminForm();
                        Program.LogInfo(string.Format("Открыта форма администратора для {0}", user.FullName));
                        break;
                    case RoleType.Storekeeper:
                        nextForm = new StorekeeperForm();
                        Program.LogInfo(string.Format("Открыта форма кладовщика для {0}", user.FullName));
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
                Program.LogWarning(string.Format($"Пользователь {user.FullName} не имеет назначенной роли"));
            }
        }
    }
}