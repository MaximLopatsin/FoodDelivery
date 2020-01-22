using DAL.Domain;
using DAL.Interfaces;
using DAL.Orm;
using DAL.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject.Modules;

namespace DAL.Infrastructure
{
    public class DALModule : NinjectModule
    {
        private readonly string _connectionString;

        public DALModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        public override void Load()
        {
            Bind<DeliveryContext>().ToSelf().WithConstructorArgument(_connectionString);
            Bind<UserContext>().ToSelf().WithConstructorArgument(_connectionString);

            var service = (UserContext)Kernel.GetService(typeof(UserContext));
            Bind<Identity.ApplicationUserManager>().ToSelf().WithConstructorArgument("store",
                new UserStore<ApplicationUser>(service));
            Bind<Identity.ApplicationRoleManager>().ToSelf().WithConstructorArgument("store",
               new RoleStore<ApplicationRole>(service));

            Bind<IRepository<Ingredient>>().To<GenericRepository<Ingredient>>();
            Bind<IRepository<Institution>>().To<GenericRepository<Institution>>();
            Bind<IRepository<Meal>>().To<GenericRepository<Meal>>();
            Bind<IRepository<MealIngredient>>().To<GenericRepository<MealIngredient>>();
            Bind<IRepository<Menu>>().To<GenericRepository<Menu>>();
            Bind<IRepository<MenuMeal>>().To<GenericRepository<MenuMeal>>();
            Bind<IRepository<Order>>().To<GenericRepository<Order>>();
            Bind<IRepository<OrderMeal>>().To<GenericRepository<OrderMeal>>();
            Bind<IClientRepository>().To<ClientRepository>();
        }
    }
}
