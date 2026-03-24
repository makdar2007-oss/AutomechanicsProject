using AutomechanicsProject.Classes;
using AutomechanicsProject.Formes;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Properties;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

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
                MessageBox.Show("Пожалуйста, заполните все поля!",
                    Resources.TitleWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        MessageBox.Show("Неверный логин или пароль!",
                            Resources.TitleWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    var isValid = PasswordHelper.VerifyPassword(textBoxPassword.Text, user.Password);

                    if (!isValid && textBoxPassword.Text == user.Password)
                    {
                        user.Password = PasswordHelper.HashPassword(textBoxPassword.Text);
                        db.SaveChanges();
                        isValid = true;
                    }
                    if (!isValid)
                    {
                        textBoxPassword.BackColor = Color.LightPink;
                        MessageBox.Show("Неверный логин или пароль!",
                            Resources.TitleWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    Program.CurrentUser = user;
                    OpenMainForm(user);
                }
            }
            catch (Exception ex)
            {
                Program.LogError("Ошибка авторизации", ex);
                MessageBox.Show("Ошибка при входе в систему",
                    Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        Program.LogInfo($"Открыта форма администратора для {user.FullName}");
                        break;
                    case RoleType.Storekeeper:
                        nextForm = new StorekeeperForm();
                        Program.LogInfo($"Открыта форма кладовщика для {user.FullName}");
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
                MessageBox.Show("У вас нет доступа к системе! Обратитесь к администратору.",
                    Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.LogWarning($"Пользователь {user.FullName} не имеет назначенной роли");
            }
        }
    }
}