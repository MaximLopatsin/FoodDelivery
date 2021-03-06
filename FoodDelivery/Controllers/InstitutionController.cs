﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using BLL.Dto;
using BLL.Interfaces;
using FoodDelivery.Models;

namespace FoodDelivery.Controllers
{
    [Authorize]
    public class InstitutionController : Controller
    {
        private readonly IInstitutionService _institutionService;
        private readonly IMenuService _menuService;
        private readonly IMealService _mealService;

        public InstitutionController(IInstitutionService institutionService, IMenuService menuService, IMealService mealService)
        {
            _institutionService = institutionService;
            _menuService = menuService;
            _mealService = mealService;
        }

        public ActionResult Index()
        {
            var list = _institutionService.GetInstitutions();

            return View(list);
        }

        public ActionResult IndexMenu(int? id)
        {
            var list = new List<Menu>();
            var menu = Task.Run(() => _menuService.GetMenuByInstitutionIdAsync(id.Value)).Result;
            if (menu != null)
            {
                list.Add(menu);
            }
            TempData["institutionId"] = id.Value;
            return View(list);
        }

        public ActionResult IndexMeal(int? id)
        {
            var list = Task.Run(() => _mealService.GetMealsByMenuIdAsync(id.Value)).Result;
            TempData["menuId"] = id.Value;
            return View(list);
        }

        [HttpGet]
        public ActionResult Find(string content)
        {
            var list = _institutionService.FindByName(content);

            return View("Index", list);
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var institution = _institutionService.GetInstitutions().Find(a => a.Id == id);
            var menu = await _menuService.GetMenuByIdAsync(institution.MenuId);
            var model = new InstitutionDetailsViewModel();
            model.Address = institution.Address;
            model.Id = institution.Id;
            model.Name = institution.Name;
            model.ExpectedDeliveryTime = institution.ExpectedDeliveryTime;
            if (menu != null)
            {
                var meals = await _menuService.GetMealsByMenuIdAsync(menu.Id);
                foreach (var meal in meals)
                {
                    model.Meals.Add(meal.Name, meal.Price);
                }
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var institution = new Institution();
            return View(institution);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Institution venue)
        {
            await _institutionService.CreateInstitutionAsync(venue);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CreateMenu(int? id)
        {
            var institution = new Menu
            {
                InstitutionId = id.Value,
            };
            return View(institution);
        }

        [HttpPost]
        public async Task<ActionResult> CreateMenu(Menu venue)
        {
            var menu = await _menuService.CreateMenuAsync(venue);
            var institut = _institutionService.GetInstitutions().First(a => a.Id == venue.InstitutionId);
            institut.MenuId = menu.Id;
            await _institutionService.UpdateInstitutionAsync(institut);
            return RedirectToAction("Details", new { id = venue.InstitutionId });
        }

        [HttpGet]
        public ActionResult CreateMeal(int? id)
        {
            var institution = new Meal
            {
                MenuId = id.Value,
                Ingredients = new List<Ingredient>(),
            };
            return View(institution);
        }

        [HttpPost]
        public async Task<ActionResult> CreateMeal(Meal entity, List<Ingredient> ingredients)
        {
            if (ingredients is null || !ingredients.Any())
            {
                ModelState.AddModelError(string.Empty, "Должен быть хотя бы 1 ингредиент");
                entity.Ingredients = new List<Ingredient>();
                return View(entity);
            }

            var meal = await _mealService.CreateMealAsync(entity, ingredients);

            meal.MenuId = entity.MenuId;
            meal.Price = entity.Price;

            await _menuService.AddMealToMenuAsync(meal);

            return RedirectToAction("DetailsMenu", new { id = entity.MenuId });
        }

        public ActionResult AddIngredient()
        {
            SelectedProduct();
            var ingredient = new Ingredient();
            return PartialView(ingredient);
        }

        [HttpGet]
        public async Task<ActionResult> DetailsMenu(int? id)
        {
            var model = await _menuService.GetMenuByIdAsync(id.Value);

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> DetailsMeal(int? id)
        {
            var model = await _mealService.GetMealByIdAsync(id.GetValueOrDefault());

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction(nameof(Index));
            }

            var institution = _institutionService.GetInstitutions().FirstOrDefault(x => x.Id == id.Value);
            return View(institution);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Institution institution)
        {
            await _institutionService.UpdateInstitutionAsync(institution);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction(nameof(Index));
            }

            var institution = _institutionService.GetInstitutions().FirstOrDefault(x => x.Id == id.Value);
            return View(institution);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteCommand(int? id)
        {
            var institution = _institutionService.GetInstitutions().FirstOrDefault(x => x.Id == id.Value);
            await _institutionService.DeleteInstitutionAsync(institution);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<ActionResult> DeleteMenu(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction(nameof(Index));
            }

            var menu = await _menuService.GetMenuByIdAsync(id.GetValueOrDefault());
            return View(menu);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteMenuCommand(int? id)
        {
            var menu = await _menuService.GetMenuByIdAsync(id.GetValueOrDefault());
            await _menuService.DeleteMenuAsync(menu);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<ActionResult> DeleteMeal(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction(nameof(Index));
            }

            var meal = await _mealService.GetMealByIdAsync(id.GetValueOrDefault());
            return View(meal);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteMealCommand(int? id)
        {
            var meal = await _mealService.GetMealByIdAsync(id.GetValueOrDefault());
            await _mealService.DeleteMealAsync(meal);

            return RedirectToAction(nameof(Index));
        }

        private void SelectedProduct()
        {
            ViewBag.Id = _mealService.GetIngredients().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() });
        }
    }
}