using System;
using System.Drawing;
using System.Windows.Forms;
using AutomechanicsProject.Properties;

namespace AutomechanicsProject.Helpers
{
    /// <summary>
    /// Вспомогательный класс для настройки элементов управления с водяными знаками
    /// </summary>
    public static class TextBoxHelper
    {
        /// <summary>
        /// Настраивает водяной знак для текстового поля
        /// При фокусе на поле водяной знак исчезает, при потере фокуса и пустом поле - появляется снова
        /// </summary>
        public static void SetupWatermarkTextBox(TextBox textBox, string watermarkText)
        {
            if (string.IsNullOrEmpty(watermarkText))
            {
                return;
            }
            textBox.Text = watermarkText;
            textBox.ForeColor = Color.Gray;

            textBox.Enter += (sender, e) =>
            {
                var tb = sender as TextBox;
                if (tb.Text == watermarkText)
                {
                    tb.Text = string.Empty;
                    tb.ForeColor = Color.Black;
                }
            };
            textBox.Leave += (sender, e) =>
            {
                var tb = sender as TextBox;
                if (string.IsNullOrWhiteSpace(tb.Text))
                {
                    tb.Text = watermarkText;
                    tb.ForeColor = Color.Gray;
                }
            };
        }
        /// <summary>
        /// Настраивает водяной знак для поля пароля
        /// При фокусе на поле водяной знак исчезает, поле переключается в режим скрытия пароля,
        /// при потере фокуса и пустом поле - водяной знак появляется снова
        /// </summary>
        public static void SetupPasswordTextBox(TextBox textBox, string watermarkText)
        {
            if (string.IsNullOrEmpty(watermarkText))
            {
                return;
            }
            textBox.Text = watermarkText;
            textBox.ForeColor = Color.Gray;
            textBox.UseSystemPasswordChar = false;
            textBox.Enter += (sender, e) =>
            {
                var tb = sender as TextBox;
                if (tb.Text == watermarkText)
                {
                    tb.Text = string.Empty;
                    tb.ForeColor = Color.Black;
                    tb.UseSystemPasswordChar = true;
                }
            };
            textBox.Leave += (sender, e) =>
            {
                var tb = sender as TextBox;
                if (string.IsNullOrWhiteSpace(tb.Text))
                {
                    tb.UseSystemPasswordChar = false;
                    tb.Text = watermarkText;
                    tb.ForeColor = Color.Gray;
                }
            };
        }
        /// <summary>
        /// Настраивает водяной знак для выпадающего списка
        /// При фокусе на списке водяной знак исчезает, при потере фокуса и пустом выборе - появляется снова
        /// </summary>
        public static void SetupWatermarkComboBox(ComboBox comboBox, string watermarkText)
        {
            if (string.IsNullOrEmpty(watermarkText))
            {
                return;
            }

            comboBox.Text = watermarkText;
            comboBox.ForeColor = Color.Gray;

            comboBox.Enter += (sender, e) =>
            {
                var cb = sender as ComboBox;
                if (cb.Text == watermarkText)
                {
                    cb.Text = string.Empty;
                    cb.ForeColor = Color.Black;
                }
            };
            comboBox.Leave += (sender, e) =>
            {
                var cb = sender as ComboBox;
                if (string.IsNullOrWhiteSpace(cb.Text))
                {
                    cb.Text = watermarkText;
                    cb.ForeColor = Color.Gray;
                }
            };
        }
    }
}