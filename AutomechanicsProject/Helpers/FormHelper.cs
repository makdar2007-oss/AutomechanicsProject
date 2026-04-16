using AutomechanicsProject;
using AutomechanicsProject.Properties;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// Вспомогательный статический класс для работы с формами и диалоговыми окнами
/// Предоставляет методы для проверки подтверждения действий и обработки ошибок
/// </summary>
public static class FormHelper
{
    /// <summary>
    /// Проверяет, является ли текст подсказкой
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
        MessageBox.Show(Resources.ErrorGeneric, Resources.TitleError,
            MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    /// <summary>
    /// Асинхронное отображение формы с загрузкой данных
    /// </summary>
    public static async Task<TResult> ShowFormAsync<TForm, TResult>(
        Func<TForm> formFactory,
        Func<TForm, Task> loadDataAsync,
        IWin32Window owner = null) where TForm : Form
    {
        using (var form = formFactory())
        {
            try
            {
                form.Cursor = Cursors.WaitCursor;
                await loadDataAsync(form);
                form.Cursor = Cursors.Default;

                return (TResult)(object)form.ShowDialog(owner);
            }
            catch (Exception ex)
            {
                Program.LogError($"Ошибка при открытии формы {typeof(TForm).Name}", ex);
                MessageBox.Show(Resources.ErrorGeneric, Resources.TitleError,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return default;
            }
            finally
            {
                form.Cursor = Cursors.Default;
            }
        }
    }

    /// <summary>
    /// Безопасное выполнение асинхронной операции с обработкой ошибок
    /// </summary>
    public static async Task<bool> ExecuteSafeAsync(
        Func<Task> action,
        string errorMessage,
        IWin32Window owner = null)
    {
        try
        {
            await action();
            return true;
        }
        catch (Exception ex)
        {
            Program.LogError(errorMessage, ex);
            MessageBox.Show(owner, $"{errorMessage}\n{ex.Message}",
                Resources.TitleError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
    }

    /// <summary>
    /// Универсальная загрузка ComboBox
    /// </summary>
    public static async Task LoadComboBoxAsync<T>(
        ComboBox comboBox,
        Func<Task<List<T>>> loadDataAsync,
        string displayMember = null,
        string valueMember = null)
    {
        comboBox.Cursor = Cursors.WaitCursor;
        comboBox.Enabled = false;

        try
        {
            var data = await loadDataAsync();
            comboBox.DataSource = data;

            if (!string.IsNullOrEmpty(displayMember))
                comboBox.DisplayMember = displayMember;
            if (!string.IsNullOrEmpty(valueMember))
                comboBox.ValueMember = valueMember;

            comboBox.SelectedIndex = -1;
        }
        finally
        {
            comboBox.Cursor = Cursors.Default;
            comboBox.Enabled = true;
        }
    }
}