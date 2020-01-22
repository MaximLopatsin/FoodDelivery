using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Dto;

namespace BLL.Interfaces
{
    public interface IMealService
    {
        Task<List<Meal>> GetMealsByMenuIdAsync(int menuId);

        Task<List<Ingredient>> GetIngredientsByMealIdAsync(int mealId);

        List<Ingredient> GetIngredients();

        Task<Meal> GetMealByIdAsync(int mealId);

        Task<Meal> CreateMealAsync(Meal meal, List<Ingredient> ingredients);

        Task UpdateMealAsync(Meal meal, List<Ingredient> ingredients);

        Task DeleteMealAsync(Meal meal);
    }
}
