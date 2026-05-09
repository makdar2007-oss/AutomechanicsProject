using AutomechanicsProject.Classes;
using AutomechanicsProject.Formes;
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
                

                Component.For<IAuthService>().ImplementedBy<AuthService>().LifestyleTransient(),
                Component.For<ICategoryService>().ImplementedBy<CategoryService>().LifestyleTransient(),
                Component.For<IProductService>().ImplementedBy<ProductService>().LifestyleTransient(),
                Component.For<IReportService>().ImplementedBy<ReportService>().LifestyleTransient(),
                Component.For<IShipmentService>().ImplementedBy<ShipmentService>().LifestyleTransient(),
                Component.For<ISupplyService>().ImplementedBy<SupplyService>().LifestyleTransient(),


                Component.For<ChoosingCurrency>().LifestyleTransient(),
                Component.For<Autorization>().LifestyleTransient(),
                Component.For<Registration>().LifestyleTransient(),
                Component.For<EditCategory>().LifestyleTransient(),
                Component.For<ShipmentHistoryForm>().LifestyleTransient(),
                Component.For<DeleteCategory>().LifestyleTransient(),
                Component.For<DeleteProduct>().LifestyleTransient(),
                Component.For<RedactProduct>().LifestyleTransient(),
                Component.For<AdminForm>().LifestyleTransient(),
                Component.For<StorekeeperForm>().LifestyleTransient(),
                Component.For<AddCategory>().LifestyleTransient(),
                Component.For<AddProduct>().LifestyleTransient(),
                Component.For<CreateShipment>().LifestyleTransient(),
                Component.For<CreateSupply>().LifestyleTransient(),
                Component.For<DateBase>().LifestyleTransient(),
                Component.For<ReportForm>().LifestyleTransient()
                );
                



            return container;
        }
    }
}