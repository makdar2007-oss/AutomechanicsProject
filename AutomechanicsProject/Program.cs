using AutomechanicsProject.Classes;
using AutomechanicsProject.Formes;
using NLog;
using System;
using System.IO;
using System.Windows.Forms;

namespace AutomechanicsProject
{
    internal static class Program
    {
        public static Users CurrentUser { get; set; }
        private static readonly string LogDirectory;
        private static readonly object LockObject = new object();
        private static readonly NLog.Logger logger = LogManager.GetCurrentClassLogger();

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                logger.Info("Приложение запущено");
                var db = new DateBase();
                Application.Run(new Autorization(db));
            }
            catch (Exception ex)
            {
                logger.Error("Критическая ошибка при запуске приложения", ex);
                MessageBox.Show("Произошла критическая ошибка. Приложение будет закрыто.",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { LogManager.Shutdown(); }
        }
    }
}