using AutomechanicsProject.Classes;
using AutomechanicsProject.Helpers;
using AutomechanicsProject.Properties;
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
        private readonly DateBase _db;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Инициализирует новый экземпляр формы регистрации
        /// </summary>
        public Registration(DateBase database)
        {
            InitializeComponent();
            _db = database ?? throw new ArgumentNullException(nameof(database));
            TextBoxHelper.SetupWatermarkTextBox(textBoxSurname, Resources.RegSurnameWatermark);
            TextBoxHelper.SetupWatermarkTextBox(textBoxName, Resources.RegNameWatermark);
            TextBoxHelper.SetupWatermarkTextBox(textBoxLastname, Resources.RegLastnameWatermark);
            TextBoxHelper.SetupWatermarkTextBox(textBoxLogin, Resources.RegLoginWatermark);
            TextBoxHelper.SetupPasswordTextBox(textBoxPassword, Resources.RegPasswordWatermark);
            TextBoxHelper.SetupPasswordTextBox(textBoxAgreePassword, Resources.RegConfirmPasswordWatermark);
        }

        /// <summary>
        /// Выполняет валидацию всех полей формы регистрации
        /// </summary>
        private bool ValidateAllFields()
        {
            var isValid = true;
            if (Validation.IsWatermark(textBoxSurname.Text, Resources.RegSurnameWatermark))
            {
                textBoxSurname.BackColor = Color.LightPink;
                isValid = false;
            }
            else if (!Validation.IsValidRussianName(textBoxSurname.Text))
            {
                textBoxSurname.BackColor = Color.LightPink;
                isValid = false;
            }
            else if (!Validation.IsValidNameLength(textBoxSurname.Text))
            {
                textBoxSurname.BackColor = Color.LightPink;
                isValid = false;
            }
            else
            {
                textBoxSurname.BackColor = Color.White;
            }
            if (Validation.IsWatermark(textBoxName.Text, Resources.RegNameWatermark))
            {
                textBoxName.BackColor = Color.LightPink;
                isValid = false;
            }
            else if (!Validation.IsValidRussianName(textBoxName.Text))
            {
                textBoxName.BackColor = Color.LightPink;
                isValid = false;
            }
            else if (!Validation.IsValidNameLength(textBoxName.Text))
            {
                textBoxName.BackColor = Color.LightPink;
                isValid = false;
            }
            else
            {
                textBoxName.BackColor = Color.White;
            }
            if (!Validation.IsWatermark(textBoxLastname.Text, Resources.RegLastnameWatermark) &&
                !string.IsNullOrWhiteSpace(textBoxLastname.Text))
            {
                if (!Validation.IsValidRussianName(textBoxLastname.Text))
                {
                    textBoxLastname.BackColor = Color.LightPink;
                    isValid = false;
                }
                else if (!Validation.IsValidNameLength(textBoxLastname.Text))
                {
                    textBoxLastname.BackColor = Color.LightPink;
                    isValid = false;
                }
                else
                {
                    textBoxLastname.BackColor = Color.White;
                }
            }
            else
            {
                textBoxLastname.BackColor = Color.White;
            }
            if (Validation.IsWatermark(textBoxLogin.Text, Resources.RegLoginWatermark))
            {
                textBoxLogin.BackColor = Color.LightPink;
                isValid = false;
            }
            else if (!Validation.IsValidLogin(textBoxLogin.Text))
            {
                textBoxLogin.BackColor = Color.LightPink;
                isValid = false;
            }
            else if (!Validation.IsValidLoginLength(textBoxLogin.Text))
            {
                textBoxLogin.BackColor = Color.LightPink;
                isValid = false;
            }
            else
            {
                textBoxLogin.BackColor = Color.White;
            }
            if (Validation.IsWatermark(textBoxPassword.Text, Resources.RegPasswordWatermark))
            {
                textBoxPassword.BackColor = Color.LightPink;
                isValid = false;
            }
            else if (!Validation.IsValidPassword(textBoxPassword.Text))
            {
                textBoxPassword.BackColor = Color.LightPink;
                isValid = false;
            }
            else
            {
                textBoxPassword.BackColor = Color.White;
            }
            if (Validation.IsWatermark(textBoxAgreePassword.Text, Resources.RegConfirmPasswordWatermark))
            {
                textBoxAgreePassword.BackColor = Color.LightPink;
                isValid = false;
            }
            else if (textBoxPassword.Text != textBoxAgreePassword.Text)
            {
                textBoxPassword.BackColor = Color.LightPink;
                textBoxAgreePassword.BackColor = Color.LightPink;
                isValid = false;
            }
            else
            {
                textBoxAgreePassword.BackColor = Color.White;
            }

            return isValid;
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
        /// Отображает сообщение об ошибке пользователю
        /// </summary>
        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, Resources.TitleWarning,
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Обработчик нажатия кнопки регистрации
        /// Выполняет регистрацию нового пользователя в системе
        /// </summary>
        private void buttonRegistration_Click(object sender, EventArgs e)
        {
            ResetAllHighlights();

            if (!ValidateAllFields())
            {
                ShowErrorMessage(Resources.ErrorFixFormErrors);
                return;
            }
            try
            {
                var login = textBoxLogin.Text.Trim();

                var storekeeperRole = _db.Roles
                    .FirstOrDefault(r => r.Position == "Кладовщик");
                if (storekeeperRole == null)
                {
                    MessageBox.Show(Resources.ErrorRoleNotFound,
                        Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (_db.Users.Any(u => u.Login == login))
                {
                    textBoxLogin.BackColor = Color.LightPink;
                    ShowErrorMessage(Resources.ErrorUserExists);
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
                _db.Users.Add(newUser);
                _db.SaveChanges();

                logger.Info($"Зарегистрирован новый пользователь: {newUser.Login} - {newUser.FullName}");
                MessageBox.Show(string.Format(Resources.SuccessRegistrationWithDetails, login, newUser.FullName),
                    Resources.TitleSuccess, MessageBoxButtons.OK, MessageBoxIcon.Information);

                OpenAuthorization();
            }
            catch (Exception)
            {
                logger.Error("Ошибка регистрации");
                MessageBox.Show(Resources.ErrorRegistration, Resources.TitleError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        
        
        /// <summary>
        /// Открывает форму авторизации и закрывает текущую форму
        /// </summary>
        private void OpenAuthorization()
        {
            var authForm = new Autorization(_db);
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
            if (!Validation.IsWatermark(textBoxSurname.Text, Resources.RegSurnameWatermark))
            {
                if (!Validation.IsValidRussianName(textBoxSurname.Text) ||
                    !Validation.IsValidNameLength(textBoxSurname.Text))
                {
                    textBoxSurname.BackColor = Color.LightPink;
                }
                else
                {
                    textBoxSurname.BackColor = Color.White;
                }
            }
            else
            {
                textBoxSurname.BackColor = Color.White;
            }
        }

        /// <summary>
        /// Обработчик изменения текста в поле имени
        /// </summary>
        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            if (!Validation.IsWatermark(textBoxName.Text, Resources.RegNameWatermark))
            {
                if (!Validation.IsValidRussianName(textBoxName.Text) ||
                    !Validation.IsValidNameLength(textBoxName.Text))
                {
                    textBoxName.BackColor = Color.LightPink;
                }
                else
                {
                    textBoxName.BackColor = Color.White;
                }
            }
            else
            {
                textBoxName.BackColor = Color.White;
            }
        }

        /// <summary>
        /// Обработчик изменения текста в поле отчества
        /// </summary>
        private void textBoxLastname_TextChanged(object sender, EventArgs e)
        {
            if (!Validation.IsWatermark(textBoxLastname.Text, Resources.RegLastnameWatermark) &&
                !string.IsNullOrWhiteSpace(textBoxLastname.Text))
            {
                if (!Validation.IsValidRussianName(textBoxLastname.Text) ||
                    !Validation.IsValidNameLength(textBoxLastname.Text))
                {
                    textBoxLastname.BackColor = Color.LightPink;
                }
                else
                {
                    textBoxLastname.BackColor = Color.White;
                }
            }
            else
            {
                textBoxLastname.BackColor = Color.White;
            }
        }

        /// <summary>
        /// Обработчик изменения текста в поле логина
        /// </summary>
        private void textBoxLogin_TextChanged(object sender, EventArgs e)
        {
            if (!Validation.IsWatermark(textBoxLogin.Text, Resources.RegLoginWatermark))
            {
                if (!Validation.IsValidLogin(textBoxLogin.Text) ||
                    !Validation.IsValidLoginLength(textBoxLogin.Text))
                {
                    textBoxLogin.BackColor = Color.LightPink;
                }
                else
                {
                    textBoxLogin.BackColor = Color.White;
                }
            }
            else
            {
                textBoxLogin.BackColor = Color.White;
            }
        }

        /// <summary>
        /// Обработчик изменения текста в поле пароля
        /// </summary>
        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {
            if (!Validation.IsWatermark(textBoxPassword.Text, Resources.RegPasswordWatermark))
            {
                if (!Validation.IsValidPassword(textBoxPassword.Text))
                {
                    textBoxPassword.BackColor = Color.LightPink;
                }
                else
                {
                    textBoxPassword.BackColor = Color.White;
                }
            }
            else
            {
                textBoxPassword.BackColor = Color.White;
            }
            if (!Validation.IsWatermark(textBoxAgreePassword.Text, Resources.RegConfirmPasswordWatermark))
            {
                if (textBoxPassword.Text != textBoxAgreePassword.Text)
                {
                    textBoxAgreePassword.BackColor = Color.LightPink;
                }
                else if (!Validation.IsWatermark(textBoxAgreePassword.Text, Resources.RegConfirmPasswordWatermark))
                {
                    textBoxAgreePassword.BackColor = Color.White;
                }
            }
        }
        /// <summary>
        /// Обработчик изменения текста в поле подтверждения пароля
        /// Выполняет проверку совпадения паролей в реальном времени
        /// </summary>
        private void textBoxAgreePassword_TextChanged(object sender, EventArgs e)
        {
            if (!Validation.IsWatermark(textBoxAgreePassword.Text, Resources.RegConfirmPasswordWatermark))
            {
                if (textBoxPassword.Text != textBoxAgreePassword.Text)
                {
                    textBoxAgreePassword.BackColor = Color.LightPink;
                    textBoxPassword.BackColor = Color.LightPink;
                }
                else if (!Validation.IsWatermark(textBoxPassword.Text, Resources.RegPasswordWatermark))
                {
                    textBoxAgreePassword.BackColor = Color.White;
                    if (Validation.IsValidPassword(textBoxPassword.Text))
                    {
                        textBoxPassword.BackColor = Color.White;
                    }
                }
            }
            else
            {
                textBoxAgreePassword.BackColor = Color.White;
            }
        }
    }
}