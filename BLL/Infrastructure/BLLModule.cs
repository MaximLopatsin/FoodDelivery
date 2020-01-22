using BLL.Interfaces;
using BLL.Services;
using DAL.Infrastructure;
using Ninject.Modules;

namespace BLL.Infrastructure
{
    public class BLLModule : NinjectModule
    {
        private readonly string _connectionString;

        public BLLModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        public override void Load()
        {
            Kernel.Load(new[] { new DALModule(_connectionString) });

            Bind<IInstitutionService>().To<InstitutionService>();
            Bind<IMealService>().To<MealService>();
            Bind<IMenuService>().To<MenuService>();
            Bind<IOrderService>().To<OrderService>();
            Bind<IUserService>().To<UserService>();
        }
    }
}
