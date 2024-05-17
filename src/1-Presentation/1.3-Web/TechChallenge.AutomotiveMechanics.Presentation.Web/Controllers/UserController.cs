using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Security.Claims;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Infrastructure.Data.Repositories;
using TechChallenge.AutomotiveMechanics.Presentation.Web.Models;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;
using TechChallenge.AutomotiveMechanics.Services.Business.Services;

namespace TechChallenge.AutomotiveMechanics.Presentation.Web.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IBaseNotification baseNotification, IUserService userService) : base(baseNotification)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }



        // GET: OrderController/Create
        public async Task<IActionResult> Login(string returnUrl)
        {
            UserLoginInput input = new UserLoginInput();
            input.Email = "isaias@test.com";
            input.Password = "123456";

            ViewBag.ReturnUrl = returnUrl;
            //return View();
            return View(input);
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginInput input, string returnUrl)
        {
            try
            {
                var result = await _userService.Login(input);
                //return OKOrBadRequest(result);

                if (result != null)
                {
                    var sessionUserResult = JsonConvert.SerializeObject(result);
                    HttpContext.Session.SetString("UserResult", sessionUserResult);

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, result.Email)
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    //string returnUrl = HttpContext.Request.Query["ReturnUrl"];
                    if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    else
                        return RedirectToAction("Index", "Home");
                }

                return View(input);

            }
            catch
            {
                return View();
            }
        }

    }
}
