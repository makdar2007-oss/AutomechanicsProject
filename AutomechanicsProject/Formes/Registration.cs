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

                var role = _db.Roles.FirstOrDefault(r => r.Position == Resources.StorekeeperRoleName);
                if (role == null)
                {
                    MessageBox.Show(Resources.ErrorRoleNotFound,
                        Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (_db.Users.Any(u => u.Login == login))
                {
                    Validation.HighlightError(textBoxLogin, true);
                    MessageBox.Show(Resources.ErrorUserExists,
                        Resources.TitleWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var newUser = new Users
                {
                    Id = Guid.NewGuid(),
                    Surname = textBoxSurname.Text.Trim(),
                    Name = textBoxName.Text.Trim(),
                    Lastname = Validation.IsWatermark(textBoxLastname.Text, Resources.RegLastnameWatermark)
                        ? null
                        : textBoxLastname.Text.Trim(),
                    Login = login,
                    Password = PasswordHelper.HashPassword(textBoxPassword.Text),
                    RoleId = role.Id
                };

                _db.Users.Add(newUser);
                _db.SaveChanges();

                MessageBox.Show(
                    string.Format(Resources.SuccessRegistrationWithDetails, login, newUser.FullName),
                    Resources.TitleSuccess,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                OpenAuthorization();
            }
            catch (Exception ex)
            {
                FormHelper.HandleException("Ошибка регистрации", ex, this);
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