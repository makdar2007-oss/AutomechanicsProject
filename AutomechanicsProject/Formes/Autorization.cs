using AutomechanicsProject.Classes;
using AutomechanicsProject.Formes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AutomechanicsProject
{
    public partial class Autorization : Form
    {
        public Autorization()
        {
            InitializeComponent();
        }
        private void linkRegisterClick(object sender, EventArgs e)
        {
            new Registration().Show();
            this.Hide();
        }
        private void BtnLoginClick(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLogin.Text) ||
                textBoxLogin.Text == "Введите имя" ||
                string.IsNullOrWhiteSpace(textBoxPassword.Text) ||
                textBoxPassword.Text == "Введите пароль")
            {
                MessageBox.Show("Заполните все поля!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var bd = new DateBase())
                {
                    var user = bd.Users
                        .Include(u => u.Role)
                        .FirstOrDefault(u => u.Login == textBoxLogin.Text
                                           && u.Password == textBoxPassword.Text);

                    if (user == null)
                    {
                        MessageBox.Show("Неверный логин или пароль!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    Program.CurrentUser = user;
                    if (user.Role?.Position == "Администратор")
                    {
                        var adminForm = new AdminForm();
                        adminForm.Show();
                        this.Hide();
                    }
                    else if (user.Role?.Position == "Кладовщик")
                    {
                        var storekeeperForm = new StorekeeperForm();
                        storekeeperForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("У вас нет доступа к системе!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка авторизации: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void textBoxLogin_Enter(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb.Text == "Введите имя")
            {
                tb.Text = string.Empty;
                tb.ForeColor = Color.Black;
            }
        }
        private void textBoxLogin_Leave(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = "Введите имя";
                tb.ForeColor = Color.Gray;
            }
        }
        private void textBoxPassword_Enter(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb.Text == "Введите пароль")
            {
                tb.Text = string.Empty;
                tb.ForeColor = Color.Black;
                tb.PasswordChar = '*';
            }
        }
        private void textBoxPassword_Leave(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb.Text == "Введите пароль")
            {
                tb.Text = string.Empty;
                tb.ForeColor = Color.Black;
                tb.PasswordChar = '*';
            }

        }
    }
}
