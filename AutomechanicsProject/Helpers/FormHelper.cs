using AutomechanicsProject;
using AutomechanicsProject.Properties;
using System;
using System.Windows.Forms;

/// <summary>
/// Вспомогательный статический класс для работы с формами и диалоговыми окнами
/// Предоставляет методы для проверки подтверждения действий и обработки ошибок
/// </summary>
public static class FormHelper
{
    /// <summary>
    /// Проверяет, является ли текст водяным знаком
    /// </summary>
    public static bool IsWatermark(string text, string watermark) =>
        string.IsNullOrWhiteSpace(text) || text == watermark;
    /// <summary>
    /// Отображает диалоговое окно подтверждения действия с вопросом
    /// </summary>
    public static bool ShowCancelConfirmation(string message) =>
        MessageBox.Show(message, Resources.TitleConfirmation,
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
    /// <summary>
    /// Обрабатывает исключение: логирует ошибку и отображает сообщение пользователю
    /// </summary>
    public static void HandleException(string message, Exception ex, IWin32Window owner = null)
    {
        Program.LogError(message, ex);
        MessageBox.Show("Произошла ошибка. Попробуйте позже.",
            Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}