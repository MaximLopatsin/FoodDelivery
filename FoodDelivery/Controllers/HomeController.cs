using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using FoodDelivery.Models;

namespace FoodDelivery.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(SearchContent searchContent)
        {
            if(searchContent == null)
            {
                return View();
            }

            var isNullOrEmpty = string.IsNullOrEmpty(searchContent.Content);
            switch (searchContent.SearchType)
            {
                case SearchType.Menu:
                    if (!isNullOrEmpty)
                    {
                        return RedirectToAction("Find", "Menu",
                            new RouteValueDictionary(
                                new Dictionary<string, object>
                                {
                                { "content", searchContent.Content }
                                }));
                    }
                    break;
                case SearchType.Institution:
                    if (!isNullOrEmpty)
                    {
                        return RedirectToAction("Find", "Institution",
                            new RouteValueDictionary(
                                new Dictionary<string, object>
                                {
                                { "content", searchContent.Content }
                                }));
                    }
                    break;
                default:
                    throw new System.Exception();
            }

            return RedirectToAction("Index", searchContent.SearchType.ToString());
        }
    }
}