using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Dto;
using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL.Services
{
    internal class MealService : IMealService
    {
        private readonly IRepository<DAL.Domain.Menu> _menuRepository;
        private readonly IRepository<DAL.Domain.Meal> _mealRepository;
        private readonly IRepository<DAL.Domain.MenuMeal> _menuMealRepository;
        private readonly IRepository<DAL.Domain.Ingredient> _ingredientRepository;
        private readonly IRepository<DAL.Domain.MealIngredient> _mealIngredientRepository;
        private readonly IMapper _mapper;

        public MealService(IRepository<DAL.Domain.Menu> menuRepository,
            IRepository<DAL.Domain.Meal> mealRepository,
            IRepository<DAL.Domain.MenuMeal> menuMealRepository,
            IRepository<DAL.Domain.Ingredient> ingredientRepository,
            IRepository<DAL.Domain.MealIngredient> mealIngredientRepository, IMapper mapper)
        {
            _menuRepository = menuRepository;
            _mealRepository = mealRepository;
            _menuMealRepository = menuMealRepository;
            _ingredientRepository = ingredientRepository;
            _mealIngredientRepository = mealIngredientRepository;
            _mapper = mapper;
        }

        public async Task<List<Meal>> GetMealsByMenuIdAsync(int menuId)
        {
            var menu = await _menuRepository.GetByIdAsync(menuId);
            var menuMeals = _menuMealRepository.GetAll()
                .Where(menuMeal => menuMeal.MenuId == menu.Id);
            var meals = new List<Meal>();

            foreach (var menuMeal in menuMeals)
            {
                var meal = await _mealRepository.GetByIdAsync(menuMeal.MealId);
                var dto = _mapper.Map<Meal>(meal);
                dto.Price = menuMeal.Price;
                dto.MenuId = menuMeal.MenuId;
                dto.Ingredients = await GetIngredientsByMealIdAsync(meal.Id);

                meals.Add(dto);
            }

            return meals;
        }

        public async Task<List<Ingredient>> GetIngredientsByMealIdAsync(int mealId)
        {
            var meal = await _mealRepository.GetByIdAsync(mealId);
            var mealIngredients = _mealIngredientRepository.GetAll()
                .Where(mealIngredient => mealIngredient.MealId == meal.Id);
            var ingredients = new List<Ingredient>();

            foreach (var mealIngredient in mealIngredients)
            {
                var ingredient = await _ingredientRepository.GetByIdAsync(mealIngredient.IngredientId);
                ingredients.Add(_mapper.Map<Ingredient>(ingredient));
            }

            return ingredients;
        }

        public async Task<Meal> GetMealByIdAsync(int mealId)
        {
            var meal = await _mealRepository.GetByIdAsync(mealId);
            var dto = _mapper.Map<Meal>(meal);
            dto.Ingredients = await GetIngredientsByMealIdAsync(mealId);

            return dto;
        }

        public async Task<Meal> CreateMealAsync(Meal meal, List<Ingredient> ingredients)
        {
            var model = _mapper.Map<DAL.Domain.Meal>(meal);
            var newMeal = await _mealRepository.CreateAsync(model);

            foreach (var ingredient in ingredients)
            {
                var mealIngredient = new DAL.Domain.MealIngredient();
                mealIngredient.MealId = newMeal.Id;
                mealIngredient.IngredientId = ingredient.Id;

                await _mealIngredientRepository.CreateAsync(mealIngredient);
            }

            return _mapper.Map<Meal>(newMeal);
        }

        public async Task UpdateMealAsync(Meal meal, List<Ingredient> ingredients)
        {
            var oldMeal = await _mealRepository.GetByIdAsync(meal.Id);
            await _mealRepository.UpdateAsync(oldMeal);
            var oldMealIngredient = _mealIngredientRepository.GetAll().Where(mealIngredient => mealIngredient.MealId == meal.Id);
            foreach (var mealIngredient in oldMealIngredient)
            {
                await _mealIngredientRepository.DeleteAsync(mealIngredient.Id);
            }

            foreach (var ingredient in ingredients)
            {
                var mealIngredient = new DAL.Domain.MealIngredient();
                mealIngredient.MealId = meal.Id;
                mealIngredient.IngredientId = ingredient.Id;

                await _mealIngredientRepository.CreateAsync(mealIngredient);
            }
        }

        public async Task DeleteMealAsync(Meal meal)
        {
            var oldMeal = await _mealRepository.GetByIdAsync(meal.Id);
            await _mealRepository.DeleteAsync(oldMeal.Id);
            var oldMealIngredient = _mealIngredientRepository.GetAll().Where(mealIngredient => mealIngredient.MealId == meal.Id).ToList();
            var oldMenuMeal = _menuMealRepository.GetAll().Where(menuMeal => menuMeal.MealId == meal.Id).ToList();

            foreach (var mealIngredient in oldMealIngredient)
            {
                await _mealIngredientRepository.DeleteAsync(mealIngredient.Id);
            }
            foreach (var menuMeal in oldMenuMeal)
            {
                await _menuMealRepository.DeleteAsync(menuMeal.Id);
            }
        }

        public List<Ingredient> GetIngredients()
        {
            var ingredients = _ingredientRepository.GetAll().ToList();

            return _mapper.Map<List<Ingredient>>(ingredients);
        }
    }
}
