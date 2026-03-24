using AutomechanicsProject.Classes;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Properties;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace AutomechanicsProject.Formes
{
    /// <summary>
    /// Форма регистрации нового кладовщика в системе
    public partial class Registration : Form
    {
        /// <summary>
        /// Инициализирует новый экземпляр формы регистрации
        /// </summary>
        public Registration()
        {
            InitializeComponent();

            TextBoxHelper.SetupWatermarkTextBox(textBoxSurname, Resources.RegSurnameWatermark);
            TextBoxHelper.SetupWatermarkTextBox(textBoxName, Resources.RegNameWatermark);
            TextBoxHelper.SetupWatermarkTextBox(textBoxLastname, Resources.RegLastnameWatermark);
            TextBoxHelper.SetupWatermarkTextBox(textBoxLogin, Resources.RegLoginWatermark);
            TextBoxHelper.SetupPasswordTextBox(textBoxPassword, Resources.RegPasswordWatermark);
            TextBoxHelper.SetupPasswordTextBox(textBoxAgreePassword, Resources.RegConfirmPasswordWatermark);
        }
        /// <summary>
        /// Сбрасывает подсветку всех полей ввода к белому цвету
        /// </summary>
        private void ResetAllHighlights()
        {
            textBoxSurname.BackColor = Color.White;
            textBoxName.BackColor = Color.White;
            textBoxLastname.BackColor = Color.White;
            textBoxLogin.BackColor = Color.White;
            textBoxPassword.BackColor = Color.White;
            textBoxAgreePassword.BackColor = Color.White;
        }
        /// <summary>
        /// Проверяет заполнение обязательных полей и совпадение паролей
        /// </summary>
        private bool ValidateAllFields()
        {
            var isValid = true;

            if (Validation.IsWatermark(textBoxSurname.Text, Resources.RegSurnameWatermark))
            {
                textBoxSurname.BackColor = Color.LightPink;
                isValid = false;
            }
            if (Validation.IsWatermark(textBoxName.Text, Resources.RegNameWatermark))
            {
                textBoxName.BackColor = Color.LightPink;
                isValid = false;
            }

            if (Validation.IsWatermark(textBoxLogin.Text, Resources.RegLoginWatermark))
            {
                textBoxLogin.BackColor = Color.LightPink;
                isValid = false;
            }

            if (Validation.IsWatermark(textBoxPassword.Text, Resources.RegPasswordWatermark))
            {
                textBoxPassword.BackColor = Color.LightPink;
                isValid = false;
            }

            if (Validation.IsWatermark(textBoxAgreePassword.Text, Resources.RegConfirmPasswordWatermark))
            {
                textBoxAgreePassword.BackColor = Color.LightPink;
                isValid = false;
            }

            if (!Validation.IsWatermark(textBoxPassword.Text, Resources.RegPasswordWatermark) &&
                !Validation.IsWatermark(textBoxAgreePassword.Text, Resources.RegConfirmPasswordWatermark))
            {
                if (textBoxPassword.Text != textBoxAgreePassword.Text)
                {
                    textBoxPassword.BackColor = Color.LightPink;
                    textBoxAgreePassword.BackColor = Color.LightPink;
                    isValid = false;
                }
            }

            return isValid;
        }
        /// <summary>
        /// Обработчик нажатия кнопки Зарегистрироваться
        /// Выполняет регистрацию нового пользователя в системе
        /// </summary>
        private void buttonRegistration_Click(object sender, EventArgs e)
        {
            ResetAllHighlights();
            if (!ValidateAllFields())
                return;

            try
            {
                using (var db = new DateBase())
                {
                    var login = textBoxLogin.Text.Trim();

                    var storekeeperRole = db.Roles
                        .FirstOrDefault(r => r.Position == "Кладовщик");

                    if (storekeeperRole == null)
                    {
                        MessageBox.Show("Ошибка: роль 'Кладовщик' не найдена!",
                            Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (db.Users.Any(u => u.Login == login))
                    {
                        textBoxLogin.BackColor = System.Drawing.Color.LightPink;
                        MessageBox.Show("Пользователь уже существует",
                            Resources.TitleWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var hashedPassword = PasswordHelper.HashPassword(textBoxPassword.Text);

                    var newUser = new Users
                    {
                        Id = Guid.NewGuid(),
                        Surname = textBoxSurname.Text.Trim(),
                        Name = textBoxName.Text.Trim(),
                        Lastname = Validation.IsWatermark(textBoxLastname.Text, Resources.RegLastnameWatermark)
                            ? null
                            : textBoxLastname.Text.Trim(),
                        Login = login,
                        Password = hashedPassword,
                        RoleId = storekeeperRole.Id
                    };
                    db.Users.Add(newUser);
                    db.SaveChanges();

                    Program.LogInfo($"Зарегистрирован: {newUser.Login}");

                    MessageBox.Show("Регистрация успешна!",
                        Resources.TitleSuccess, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    OpenAuthorization();
                }
            }
            catch (Exception ex)
            {
                Program.LogError("Ошибка регистрации", ex);
                MessageBox.Show("Ошибка. Попробуйте позже.",
                    Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Открывает форму авторизации и закрывает текущую форму
        /// Вызывается после успешной регистрации
        /// </summary>
        private void OpenAuthorization()
        {
            new Autorization().Show();
            Close();
        }
        /// <summary>
        /// Обработчик нажатия кнопки Уже есть аккаунт? Войти
        /// Открывает форму авторизации и закрывает текущую форму
        /// </summary>
        private void ButtonEnter_Click(object sender, EventArgs e)
        {
            new Autorization().Show();
            Close();
        }
    }
}