using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Dto;

namespace BLL.Interfaces
{
    public interface IMenuService
    {
        Task<Menu> GetMenuByIdAsync(int menuId);

        Task<Menu> GetMenuByInstitutionIdAsync(int institutionId);

        Task<List<MenuMeal>> GetMenuMealsAsync();

        Task<Institution> GetInstitutionByMenuIdAsync(int menuId);

        Task<List<Meal>> GetMealsByMenuIdAsync(int menuId);

        Task<List<MenuMeal>> FindByNameAsync(string name);

        Task UpdateMenuAsync(Menu menu);

        Task<Menu> CreateMenuAsync(Menu menu);

        Task DeleteMenuAsync(Menu menu);

        Task AddMealToMenuAsync(Meal meal);
    }
}
