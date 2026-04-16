using System;
using System.Windows.Forms;

namespace AutomechanicsProject.Helpers
{
    /// <summary>
    /// Вспомогательные методы для работы с контролами
    /// </summary>
    public static class ControlHelper
    {
        /// <summary>
        /// Безопасный вызов действия на UI потоке
        /// </summary>
        public static void SafeInvoke(Control control, Action action)
        {
            if (control.InvokeRequired)
                control.Invoke(action);
            else
                action();
        }

        /// <summary>
        /// Безопасный вызов с возвратом значения
        /// </summary>
        public static T SafeInvoke<T>(Control control, Func<T> func)
        {
            if (control.InvokeRequired)
                return (T)control.Invoke(func);
            else
                return func();
        }

        /// <summary>
        /// Очищает все текстовые поля и сбрасывает ComboBox
        /// </summary>
        public static void ClearControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is TextBox textBox)
                    textBox.Clear();
                else if (control is ComboBox comboBox)
                    comboBox.SelectedIndex = -1;
                else if (control.HasChildren)
                    ClearControls(control.Controls);
            }
        }
    }
}