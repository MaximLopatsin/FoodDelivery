using AutoMapper;
using BLL.MapperProfile;
using Ninject.Modules;

namespace FoodDelivery.Infrastructure
{
    public class UserModule : NinjectModule
    {
        public override void Load()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new BLLMapperProfile());
            });

            Bind<IMapper>().ToConstant(config.CreateMapper());
        }
    }
}