using System;

namespace AutomechanicsProject.Classes
{
    /// <summary>
    /// Уведомляет формы об изменении остатков склада
    /// </summary>
    public static class WarehouseRefreshNotifier
    {
        /// <summary>
        /// Событие изменения остатков склада
        /// </summary>
        public static event Action WarehouseChanged;

        /// <summary>
        /// Вызывает обновление открытых форм склада
        /// </summary>
        public static void NotifyWarehouseChanged()
        {
            WarehouseChanged?.Invoke();
        }
    }
}