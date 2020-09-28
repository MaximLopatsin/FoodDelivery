using System.Threading.Tasks;
using System.Web.Mvc;
using BLL.Interfaces;

namespace FoodDelivery.Controllers
{
    [Authorize]
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var list = await _menuService.GetMenuMealsAsync();

            return View(list);
        }

        [HttpGet]
        public async Task<ActionResult> Find(string content)
        {
            var list = await _menuService.FindByNameAsync(content);
            
            return View("Index", list);
        }

        public async Task<ActionResult> Details(int? id)
        {
            var meals = await _menuService.GetMenuMealsAsync();
            var meal = meals.Find(x => x.Id == id.Value);

            return View(meal);
        }
    }
}