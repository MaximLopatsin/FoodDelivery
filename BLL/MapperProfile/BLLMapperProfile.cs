using AutoMapper;
using BLL.Dto;

namespace BLL.MapperProfile
{
    public class BLLMapperProfile : Profile
    {
        public BLLMapperProfile()
        {
            CreateMap<Institution, DAL.Domain.Institution>();
            CreateMap<Ingredient, DAL.Domain.Ingredient>();
            CreateMap<Meal, DAL.Domain.Meal>();
            CreateMap<MealIngredient, DAL.Domain.MealIngredient>();
            CreateMap<Menu, DAL.Domain.Menu>();
            CreateMap<MenuMeal, DAL.Domain.MenuMeal>();
            CreateMap<Order, DAL.Domain.Order>();
            CreateMap<OrderMeal, DAL.Domain.OrderMeal>();

            CreateMap<Institution, DAL.Domain.Institution>().ReverseMap();
            CreateMap<Ingredient, DAL.Domain.Ingredient>().ReverseMap();
            CreateMap<Meal, DAL.Domain.Meal>().ReverseMap();
            CreateMap<MealIngredient, DAL.Domain.MealIngredient>().ReverseMap();
            CreateMap<Menu, DAL.Domain.Menu>().ReverseMap();
            CreateMap<MenuMeal, DAL.Domain.MenuMeal>().ReverseMap();
            CreateMap<Order, DAL.Domain.Order>().ReverseMap();
            CreateMap<OrderMeal, DAL.Domain.OrderMeal>().ReverseMap();
        }
    }
}
