using AutomechanicsProject.Config;
using AutomechanicsProject.Formes;
using AutomechanicsProject.Properties;
using AutomechanicsProject.Services.Interfaces;
using NLog;
using System;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;

namespace AutomechanicsProject
{
    /// <summary>
    /// Главный класс точки входа в приложение
    /// </summary>
    internal static class Program
    {

        private static readonly NLog.Logger logger = LogManager.GetCurrentClassLogger();



        [STAThread]


        /// <summary>
        /// Главный метод приложения
        /// </summary>
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                var language = Settings.Default.SelectedLanguage;

                if (string.IsNullOrWhiteSpace(language))
                {
                    language = "ru";
                }

                Thread.CurrentThread.CurrentCulture = new CultureInfo(language);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
                Application.SetCompatibleTextRenderingDefault(false);

                var container = WindsorConfig.Register();


                var form = new Autorization(
                    container.Resolve<IAuthService>(),
                    container.Resolve<IProductService>(),
                    container.Resolve<ICategoryService>(),
                    container.Resolve<ISupplyService>(),
                    container.Resolve<IReportService>(),
                    container.Resolve<IShipmentService>(),
                    container.Resolve<IExpiredProductsService>(),
                    container.Resolve<ISupplyCurrencyService>(),
                    container.Resolve<ICurrentUserService>(),
                    container.Resolve<ICurrencySettingsService>(),
                    container.Resolve<IWarehouseHeatmapService>(),
                    container.Resolve<IDaDataService>(),
                    container.Resolve<IWeatherService>());

                Application.Run(form);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Критическая ошибка при запуске приложения");

                MessageBox.Show(Resources.CriticalApplicationError,
                    Resources.TitleError,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

    }
}