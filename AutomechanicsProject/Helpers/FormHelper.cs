using AutomechanicsProject;
using AutomechanicsProject.Properties;
using System;
using System.Windows.Forms;

namespace AutomechanicsProject.Helpers
{
    /// <summary>
    /// Вспомогательный статический класс для работы с формами и диалоговыми окнами
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
        /// Открыть форму как диалог
        /// </summary>
        public static void OpenDialog(Form form)
        {
            if (form == null) return;
            form.ShowDialog();
        }

        /// <summary>
        /// Открыть форму с владельцем
        /// </summary>
        public static void OpenDialog(Form form, IWin32Window owner)
        {
            if (form == null) return;
            form.ShowDialog(owner);
        }

        /// <summary>
        /// Открыть форму и вернуть результат
        /// </summary>
        public static DialogResult OpenDialogWithResult(Form form)
        {
            if (form == null) return DialogResult.None;
            return form.ShowDialog();
        }
    }
}