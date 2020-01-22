using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Dto;
using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL.Services
{
    internal class MenuService : IMenuService
    {
        private readonly IRepository<DAL.Domain.Menu> _menuRepository;
        private readonly IRepository<DAL.Domain.Meal> _mealRepository;
        private readonly IRepository<DAL.Domain.Institution> _institutionRepository;
        private readonly IRepository<DAL.Domain.MenuMeal> _menuMealRepository;
        private readonly IMapper _mapper;

        public MenuService(IRepository<DAL.Domain.Menu> menuRepository, 
            IRepository<DAL.Domain.Meal> mealRepository, 
            IRepository<DAL.Domain.Institution> institutionRepository,
            IRepository<DAL.Domain.MenuMeal> menuMealRepository, IMapper mapper)
        {
            _menuRepository = menuRepository;
            _mealRepository = mealRepository;
            _institutionRepository = institutionRepository;
            _menuMealRepository = menuMealRepository;
            _mapper = mapper;
        }

        public async Task<Menu> GetMenuByIdAsync(int menuId)
        {
            var menu = await _menuRepository.GetByIdAsync(menuId);
            return _mapper.Map<Menu>(menu);
        }

        public async Task<Institution> GetInstitutionByMenuIdAsync(int menuId)
        {
            var menu = await _menuRepository.GetByIdAsync(menuId);
            var institution = await _institutionRepository.GetByIdAsync(menu.InstitutionId);

            return _mapper.Map<Institution>(institution);
        }

        public async Task<List<Meal>> GetMealsByMenuIdAsync(int menuId)
        {
            var menu = await _menuRepository.GetByIdAsync(menuId);
            var menuMeals = _menuMealRepository.GetAll()
                .Where(menuMeal => menuMeal.MenuId == menu.Id).ToList();
            var meals = new List<Meal>();
            foreach (var item in menuMeals)
            {
                var meal = await _mealRepository.GetByIdAsync(item.MealId);
                meals.Add(new Meal
                {
                    Id = meal.Id,
                    Name = meal.Name,
                    Price = item.Price,
                });
            }

            return _mapper.Map<List<Meal>>(meals);
        }

        public async Task UpdateMenuAsync(Menu menu)
        {
            var oldMenu = await _menuRepository.GetByIdAsync(menu.Id);
            await _menuRepository.UpdateAsync(oldMenu);
        }

        public async Task<Menu> CreateMenuAsync(Menu menu)
        {
            var newMenu = await _menuRepository.CreateAsync(_mapper.Map<DAL.Domain.Menu>(menu));

            return _mapper.Map<Menu>(newMenu);
        }

        public async Task DeleteMenuAsync(Menu menu)
        {
            await _menuRepository.DeleteAsync(_mapper.Map<DAL.Domain.Menu>(menu).Id);
            var menuMeals = _menuMealRepository.GetAll().Where(menuMeal => menuMeal.MenuId == menu.Id);
            foreach (var menuMeal in menuMeals)
            {
                await _menuMealRepository.DeleteAsync(menuMeal.Id);
            }
        }

        public async Task<List<MenuMeal>> FindByNameAsync(string name)
        {
            var result = new List<MenuMeal>();
            var menuMeals = _menuMealRepository.GetAll();

            foreach (var item in menuMeals)
            {
                var meal = await _mealRepository.GetByIdAsync(item.MealId);
                if (meal.Name.ToLower().Contains(name.ToLower()))
                {
                    result.Add(_mapper.Map<MenuMeal>(item));
                }
            }

            foreach (var item in result)
            {
                var meal = await _mealRepository.GetByIdAsync(item.MealId);
                item.MealName = meal.Name;
                var menu = await _menuRepository.GetByIdAsync(item.MenuId);
                var institution = await _institutionRepository.GetByIdAsync(menu.InstitutionId);
                item.InstitutionName = institution.Name;
                item.InstitutionCity = institution.City;
                item.InstitutionAddress = institution.Address;
                item.ExpectedDeliveryTime = institution.ExpectedDeliveryTime;
                item.InstitutionId = institution.Id;
            }

            return result;
        }

        public async Task<List<MenuMeal>> GetMenuMealsAsync()
        {
            var menuMeals = _menuMealRepository.GetAll().ToList();

            var result = _mapper.Map<List<MenuMeal>>(menuMeals);

            foreach (var item in result)
            {
                var meal = await _mealRepository.GetByIdAsync(item.MealId);
                item.MealName = meal.Name;
                var menu = await _menuRepository.GetByIdAsync(item.MenuId);
                var institution = await _institutionRepository.GetByIdAsync(menu.InstitutionId);
                item.InstitutionName = institution.Name;
                item.InstitutionCity = institution.City;
                item.InstitutionAddress = institution.Address;
                item.ExpectedDeliveryTime = institution.ExpectedDeliveryTime;
                item.InstitutionId = institution.Id;
            }

            return result;
        }

        public async Task<Menu> GetMenuByInstitutionIdAsync(int institutionId)
        {
            var institution = await _institutionRepository.GetByIdAsync(institutionId);
            var menu = await _menuRepository.GetByIdAsync(institution.MenuId);

            return _mapper.Map<Menu>(menu);
        }

        public async Task AddMealToMenuAsync(Meal meal)
        {
            var menu = await _menuRepository.GetByIdAsync(meal.MenuId);
            var menuMeal = new DAL.Domain.MenuMeal
            {
                MealId = meal.Id,
                MenuId = menu.Id,
                Price = meal.Price,
            };

            await _menuMealRepository.CreateAsync(menuMeal);
        }
    }
}
