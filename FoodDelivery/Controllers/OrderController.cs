using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using BLL.Dto;
using BLL.Interfaces;
using Microsoft.AspNet.Identity;

namespace FoodDelivery.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IMenuService _menuService;

        public OrderController(IOrderService orderService, IMenuService menuService)
        {
            _orderService = orderService;
            _menuService = menuService;
        }

        public ActionResult Index()
        {
            var orderList = _orderService.GetOrders().Where(a => a.DeliveryType == DeliveryType.New &&
                a.ClientId == User.Identity.GetUserId()).ToList();

            return View(orderList);
        }

        [HttpGet]
        public ActionResult Cart()
        {
            var mealList = Session["meals"] as List<MenuMeal>;

            return View(mealList);
        }

        public void AddMealForOrder(int? menuMealId)
        {
            if (!menuMealId.HasValue)
                return;

            var meal = Task.Run(() => _menuService.GetMenuMealsAsync()).Result.First(a => a.Id == menuMealId);

            if (Session["meals"] == null)
            {
                Session["meals"] = new List<MenuMeal>();
            }

            var meals = Session["meals"] as List<MenuMeal>;
            if (meals.All(a => a.InstitutionId == meal.InstitutionId))
            {
                meals.Add(meal);
            }

            Session["meals"] = meals;
        }

        [HttpGet]
        public async Task<ActionResult> CreateOrder()
        {
            var order = new Order();
            var menuMeals = Session["meals"] as List<MenuMeal>;
            var orderMeals = new List<OrderMeal>();
            foreach (var item in menuMeals)
            {
                orderMeals.Add(new OrderMeal
                {
                    MealId = item.MealId,
                });
            }
            order.ClientId = User.Identity.GetUserId();
            order.InstitutionId = menuMeals.First().InstitutionId;
            order.Cost = menuMeals.Sum(a => a.Price);
            order.DeliveryCost = 2;
            if (order.Cost >= 20m)
            {
                order.DeliveryCost = 0;
            }
            order.DeliveryType = DeliveryType.New;
            order.CreationTime = DateTime.Now;

            await _orderService.CreateOrderAsync(order, orderMeals);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Delete(int id)
        {
            var menuMeals = Session["meals"] as List<MenuMeal>;
            var menuMeal = menuMeals.First(a => a.Id == id);
            menuMeals.Remove(menuMeal);

            Session["meals"] = menuMeals;

            return RedirectToAction("Cart");
        }

        public async Task<ActionResult> OrderDelete(int id)
        {
            var order = _orderService.GetOrders().First(a => a.Id == id);
            await _orderService.DeleteOrderAsync(order);

            return RedirectToAction("Index");
        }

        public ActionResult NewOrders()
        {
            var orderList = _orderService.GetOrders()
                .Where(a => a.DeliveryType == DeliveryType.New || a.DeliveryType == DeliveryType.Processing)
                .ToList();

            return View(orderList);
        }

        public ActionResult Details(int? id)
        {
            var order = _orderService.GetOrders().First(a => a.Id == id.Value);

            var meals = _orderService.GetMealsByOrderId(order.Id);
            ViewBag.Meals = meals;

            return View(order);
        }

        [HttpPost]
        public async Task<ActionResult> Details(Order order)
        {
            var model = _orderService.GetOrders().First(a => a.Id == order.Id);
            model.DeliveryType = order.DeliveryType;
            await _orderService.UpdateJustOrderAsync(model);

            return RedirectToAction("NewOrder");
        }
    }
}