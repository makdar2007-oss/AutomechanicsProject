using AutomechanicsProject.Classes;
using AutomechanicsProject.Services;
using AutomechanicsProject.Services.Interfaces;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace AutomechanicsProject.Config
{
    /// <summary>
    /// Настройка Castle Windsor
    /// </summary>
    public static class WindsorConfig
    {
        /// <summary>
        /// Регистрирует зависимости
        /// </summary>
        public static IWindsorContainer Register()
        {
            var container = new WindsorContainer();

            container.Register(

                Component.For<DateBase>().LifestyleTransient(),


                Component.For<IAuthService>().ImplementedBy<AuthService>().LifestyleTransient(),
                Component.For<ICategoryService>().ImplementedBy<CategoryService>().LifestyleTransient(),
                Component.For<IProductService>().ImplementedBy<ProductService>().LifestyleTransient(),
                Component.For<IReportService>().ImplementedBy<ReportService>().LifestyleTransient(),
                Component.For<IShipmentService>().ImplementedBy<ShipmentService>().LifestyleTransient(),
                Component.For<ISupplyService>().ImplementedBy<SupplyService>().LifestyleTransient(),
                Component.For<IExpiredProductsService>().ImplementedBy<ExpiredProductsService>().LifestyleTransient(),
                Component.For<ISupplyCurrencyService>().ImplementedBy<SupplyCurrencyService>().LifestyleTransient(),
                Component.For<ICurrentUserService>().ImplementedBy<CurrentUserService>().LifestyleSingleton(),
                Component.For<ICurrencySettingsService>().ImplementedBy<CurrencySettingsService>().LifestyleSingleton(),
                Component.For<IWarehouseHeatmapService>().ImplementedBy<WarehouseHeatmapService>().LifestyleTransient()




                );
                
            return container;
        }
    }
}