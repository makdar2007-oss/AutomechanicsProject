using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Linq;

/// <summary>
/// Вспомогательный класс для валидации данных в формах
/// Предоставляет методы для проверки обязательных полей, цен, количества товаров и других входных данных
/// </summary>
public static class Validation
{
    /// <summary>
    /// Проверяет, что все обязательные поля заполнены 
    /// </summary>
    public static bool ValidateRequiredFields(params (string text, string watermark)[] fields)
    {
        foreach (var (text, watermark) in fields)
        {
            if (IsWatermark(text, watermark))
                return false;
        }
        return true;
    }

    /// <summary>
    /// Проверяет, является ли поле подсказкой или пустым
    /// </summary>
    public static bool IsWatermark(string text, string watermark) =>
        string.IsNullOrWhiteSpace(text) || text == watermark;

    /// <summary>
    /// Проверяет корректность введенной цены и преобразует ее в десятичное число
    /// </summary>
    public static bool ValidatePrice(string priceText, out decimal price)
    {
        price = 0;
        return decimal.TryParse(priceText, out price) && price >= 0;
    }

    /// <summary>
    /// Проверяет корректность введенного количества и преобразует его в целое число
    /// </summary>
    public static bool ValidateQuantity(string quantityText, out int quantity)
    {
        quantity = 0;
        return int.TryParse(quantityText, out quantity) && quantity > 0;
    }

    /// <summary>
    /// Проверяет, что строка содержит только русские буквы, дефис и пробел
    /// </summary>
    public static bool IsValidRussianName(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return false;

        string pattern = @"^[а-яА-ЯёЁ\s\-]+$";
        return Regex.IsMatch(text, pattern);
    }

    /// <summary>
    /// Проверяет, что логин содержит только английские буквы, цифры и подчеркивание
    /// </summary>
    public static bool IsValidLogin(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return false;

        string pattern = @"^[a-zA-Z0-9_]+$";
        return Regex.IsMatch(text, pattern);
    }

    /// <summary>
    /// Проверяет длину логина 
    /// </summary>
    public static bool IsValidLoginLength(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return false;

        return text.Length >= 3 && text.Length <= 20;
    }

    /// <summary>
    /// Проверяет длину имени/фамилии/отчества 
    /// </summary>
    public static bool IsValidNameLength(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return false;

        return text.Length >= 2 && text.Length <= 100;
    }

    /// <summary>
    /// Проверяет сложность пароля 
    /// </summary>
    public static bool IsValidPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return false;

        if (password.Length < 6)
            return false;

        if (!password.Any(char.IsDigit))
            return false;

        if (!password.Any(char.IsLetter))
            return false;

        return true;
    }

    /// <summary>
    /// Проверяет совпадение паролей
    /// </summary>
    public static bool IsPasswordMatch(string password, string confirmPassword)
    {
        return password == confirmPassword;
    }

    /// <summary>
    /// Проверяет все поля регистрации
    /// </summary>
    public static bool ValidateRegistrationFields(
        string surname, string surnameWatermark,
        string name, string nameWatermark,
        string login, string loginWatermark,
        string password, string passwordWatermark,
        string confirmPassword, string confirmWatermark,
        out string errorMessage,
        string lastname = null, string lastnameWatermark = null)
    {
        errorMessage = string.Empty;

        if (IsWatermark(surname, surnameWatermark))
        {
            errorMessage = "Введите фамилию";
            return false;
        }
        if (!IsValidRussianName(surname))
        {
            errorMessage = "Фамилия должна содержать только русские буквы, дефис и пробел";
            return false;
        }
        if (!IsValidNameLength(surname))
        {
            errorMessage = "Фамилия должна быть от 2 до 50 символов";
            return false;
        }
        if (IsWatermark(name, nameWatermark))
        {
            errorMessage = "Введите имя";
            return false;
        }
        if (!IsValidRussianName(name))
        {
            errorMessage = "Имя должно содержать только русские буквы, дефис и пробел";
            return false;
        }
        if (!IsValidNameLength(name))
        {
            errorMessage = "Имя должно быть от 2 до 50 символов";
            return false;
        }
        if (!string.IsNullOrWhiteSpace(lastname) &&
            !IsWatermark(lastname, lastnameWatermark))
        {
            if (!IsValidRussianName(lastname))
            {
                errorMessage = "Отчество должно содержать только русские буквы, дефис и пробел";
                return false;
            }
            if (!IsValidNameLength(lastname))
            {
                errorMessage = "Отчество должно быть от 2 до 50 символов";
                return false;
            }
        }
        if (IsWatermark(login, loginWatermark))
        {
            errorMessage = "Введите логин";
            return false;
        }
        if (!IsValidLogin(login))
        {
            errorMessage = "Логин может содержать только английские буквы, цифры и подчеркивание";
            return false;
        }
        if (!IsValidLoginLength(login))
        {
            errorMessage = "Логин должен быть от 3 до 20 символов";
            return false;
        }
        if (IsWatermark(password, passwordWatermark))
        {
            errorMessage = "Введите пароль";
            return false;
        }
        if (!IsValidPassword(password))
        {
            errorMessage = "Пароль должен содержать минимум 6 символов, хотя бы одну цифру и одну букву";
            return false;
        }
        if (IsWatermark(confirmPassword, confirmWatermark))
        {
            errorMessage = "Подтвердите пароль";
            return false;
        }
        if (!IsPasswordMatch(password, confirmPassword))
        {
            errorMessage = "Пароли не совпадают";
            return false;
        }

        return true;
    }

    /// <summary>
    /// Подсвечивает поле красным цветом при ошибке
    /// </summary>
    public static void HighlightError(TextBox textBox, bool hasError)
    {
        if (hasError)
        {
            textBox.BackColor = Color.LightPink;
            textBox.BorderStyle = BorderStyle.FixedSingle;
        }
        else
        {
            textBox.BackColor = Color.White;
        }
    }

    /// <summary>
    /// Сбрасывает подсветку всех полей
    /// </summary>
    public static void ResetHighlights(params TextBox[] textBoxes)
    {
        foreach (var textBox in textBoxes)
        {
            if (textBox != null)
            {
                textBox.BackColor = Color.White;
            }
        }
    }
}