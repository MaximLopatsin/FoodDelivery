using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BLL.Dto;
using BLL.Exceptions;
using BLL.Interfaces;
using FoodDelivery.Models;
using Microsoft.Owin.Security;

namespace WEBPresentationLayer.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            try
            {
                await SetInitialDataAsync();
            }
            catch
            {
            }

            if (ModelState.IsValid)
            {
                var userDto = new User { Password = model.Password, UserName = model.Login };
                ClaimsIdentity claim = null;
                try
                {
                    claim = await _userService.AuthenticateAsync(userDto);
                }
                catch (AuthenticateException e)
                {
                    ModelState.AddModelError("", "Wrong login or password");
                    return View(model);
                }

                AuthenticationManager.SignOut();
                AuthenticationManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(10),
                }, claim);

                return RedirectToAction("MiddleWare", "Account");
            }

            return View(model);
        }

        public ActionResult MiddleWare()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userDto = new User
                {
                    UserName = model.Login,
                    Email = model.Email,
                    Password = model.Password,
                    Address = model.Address,
                    Name = model.Name,
                    Roles = { "user" },
                };
                try
                {
                    await _userService.CreateAsync(userDto);
                    var claim = await _userService.AuthenticateAsync(userDto);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(10),
                    }, claim);

                    return RedirectToAction("Index", "Home");
                }
                catch (UserCreateException)
                {
                    ModelState.AddModelError("Create", "Error");
                }
                catch (UserExistException)
                {
                    ModelState.AddModelError("Exist", "Exist");
                }
            }

            return View(model);
        }

        private async Task SetInitialDataAsync()
        {
            await _userService.SetInitialDataAsync(new User
            {
                Email = "somemail@mail.ru",
                UserName = "qwerty",
                Password = "qwerty",
                Name = "Кличко",
                Address = "Address",
                Roles = { "user", "manager", "dispatcher", "admin" },
            }, new List<string> { "user", "manager", "dispatcher", "admin" });
        }
    }
}