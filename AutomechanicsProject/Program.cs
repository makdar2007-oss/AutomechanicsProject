using AutomechanicsProject.Classes;
using AutomechanicsProject.Formes;
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

        static Program()
        {
            LogDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            if (!Directory.Exists(LogDirectory))
            {
                Directory.CreateDirectory(LogDirectory);
            }
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                Application.Run(new Autorization());
            }
            catch (Exception ex)
            {
                Log("Критическая ошибка при запуске приложения", ex);
                MessageBox.Show("Произошла критическая ошибка. Приложение будет закрыто.",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void Log(string message, Exception ex = null, string level = "INFO")
        {
            try
            {
                lock (LockObject)
                {
                    var fileName = level == "ERROR" ? "error" : "log";
                    var logFile = Path.Combine(LogDirectory, $"{fileName}_{DateTime.Now:yyyy-MM-dd}.log");

                    var logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {level}: {message}\n";

                    if (ex != null)
                    {
                        logEntry += $"Exception: {ex.Message}\nStack Trace: {ex.StackTrace}\n";
                        if (ex.InnerException != null)
                        {
                            logEntry += $"Inner Exception: {ex.InnerException.Message}\n";
                        }
                        logEntry += new string('-', 80) + "\n";
                    }

                    File.AppendAllText(logFile, logEntry);
                }
            }
            catch { }
        }

        public static void LogInfo(string message) => Log(message, null, "INFO");
        public static void LogWarning(string message) => Log(message, null, "WARNING");
        public static void LogError(string message, Exception ex = null) => Log(message, ex, "ERROR");
    }
}