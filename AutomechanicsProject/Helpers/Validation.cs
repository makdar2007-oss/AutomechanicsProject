using System.Drawing;
using System.Windows.Forms;

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
            textBox.BackColor = Color.White;
        }
    }
}