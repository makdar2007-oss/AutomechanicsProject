using AutomechanicsProject.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutomechanicsProject.Formes
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void textBoxSurname_Enter(object sender, EventArgs e)
        {

            var tb = sender as TextBox;
            if (tb.Text == "Введите фамилию")
            {
                tb.Text = string.Empty;
                tb.ForeColor = Color.Black;
            }

        }

        private void textBoxName_Enter(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb.Text == "Введите имя")
            {
                tb.Text = string.Empty;
                tb.ForeColor = Color.Black;
            }
        }

        private void textBoxLastname_Enter(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb.Text == "Введите отчество")
            {
                tb.Text = string.Empty;
                tb.ForeColor = Color.Black;
            }
        }

        private void textBoxLogin_Enter(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb.Text == "Введите логин")
            {
                tb.Text = string.Empty;
                tb.ForeColor = Color.Black;
            }
        }

        private void textBoxPassword_Enter(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb.Text == "Введите пароль" || tb.Text == "Повторите пароль")
            {
                tb.Text = string.Empty;
                tb.ForeColor = Color.Black;
                tb.PasswordChar = '*';
            }
        }

        private void textBoxSurname_Leave(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "Введите фамилию";
                tb.ForeColor = Color.Gray;
            }
        }

        private void textBoxName_Leave(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "Введите имя";
                tb.ForeColor = Color.Gray;
            }
        }

        private void textBoxLastname_Leave(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "Введите отчество";
                tb.ForeColor = Color.Gray;
            }
        }

        private void textBoxLogin_Leave(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "Введите логин";
                tb.ForeColor = Color.Gray;
            }
        }

        private void textBoxPassword_Leave(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.PasswordChar = '\0';
                tb.Text = "Введите пароль";
                tb.ForeColor = Color.Gray;
            }
        }
        private void textBoxAgreePassword_Leave(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.PasswordChar = '\0';
                tb.Text = "Повторите пароль";
                tb.ForeColor = Color.Gray;
            }
        }

        private void buttonRegistration_Click(object sender, EventArgs e)
        {
            if (textBoxSurname.Text == "Введите фамилию" || string.IsNullOrWhiteSpace(textBoxSurname.Text) ||
                textBoxName.Text == "Введите имя" || string.IsNullOrWhiteSpace(textBoxName.Text) ||
                textBoxLogin.Text == "Введите логин" || string.IsNullOrWhiteSpace(textBoxLogin.Text) ||
                textBoxPassword.Text == "Введите пароль" || string.IsNullOrWhiteSpace(textBoxPassword.Text) ||
                textBoxAgreePassword.Text == "Повторите пароль" || string.IsNullOrWhiteSpace(textBoxAgreePassword.Text))
            {
                MessageBox.Show("Заполните все обязательные поля!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (textBoxPassword.Text != textBoxAgreePassword.Text)
            {
                MessageBox.Show("Пароли не совпадают!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using (var db = new DateBase())
                {
                    if (db.Users.Any(u => u.Login == textBoxLogin.Text))
                    {
                        MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var storekeeperRole = db.Roles.FirstOrDefault(r => r.Position == "Кладовщик");

                    if (storekeeperRole == null)
                    {
                        MessageBox.Show("Ошибка: роль 'Кладовщик' не найдена в базе данных!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var newUser = new User
                    {
                        Id = Guid.NewGuid(),
                        Surname = textBoxSurname.Text,
                        Name = textBoxName.Text,
                        Lastname = textBoxLastname.Text == "Введите отчество" ? null : textBoxLastname.Text,
                        Login = textBoxLogin.Text,
                        Password = textBoxPassword.Text,
                        RoleId = storekeeperRole.Id
                    };

                    db.Users.Add(newUser);
                    db.SaveChanges();

                    MessageBox.Show($"Регистрация успешна!\n\nВы зарегистрированы как кладовщик:\n" +
                                  $"{textBoxSurname.Text} {textBoxName.Text} {textBoxLastname.Text}\n" +
                                  $"Логин: {textBoxLogin.Text}",
                        "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    new Autorization().Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при регистрации: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }     
        private void ButtonEnter_Click(object sender, EventArgs e)
        {
            new Autorization().Show();
            this.Close();
        }
    }
}

