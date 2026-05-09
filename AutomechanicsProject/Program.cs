using AutomechanicsProject.Classes;
using AutomechanicsProject.Formes;
using NLog;
using System;
using System.IO;
using System.Windows.Forms;
using AutomechanicsProject.Config;
using Castle.Windsor;

namespace AutomechanicsProject
{
    internal static class Program
    {
        public static Users CurrentUser { get; set; }
        private static readonly NLog.Logger logger = LogManager.GetCurrentClassLogger();
        public static IWindsorContainer Container { get; set; }



        

        [STAThread]

        
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                var container = WindsorConfig.Register();
                Program.Container = container;

                var form = container.Resolve<Autorization>();

                Application.Run(form);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "CRITICAL ERROR",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}