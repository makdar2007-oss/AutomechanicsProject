using AutomechanicsProject.Classes;
using AutomechanicsProject.Formes;
using System;
using System.Windows.Forms;
using Npgsql;

namespace AutomechanicsProject
{
    internal static class Program
    {
        public static Users CurrentUser { get; set; }
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Autorization());
        }
    }
}
