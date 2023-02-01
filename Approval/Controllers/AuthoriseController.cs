using Approval.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using Approval.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Data;

namespace Approval.Controllers
{
    public class AuthoriseController : Controller
    {
        private readonly ILogger<AuthoriseController> _logger;
        private readonly AuthoriseLogic _authorise;

        public AuthoriseController(ILogger<AuthoriseController> logger, AuthoriseLogic authorise)
        {
            _logger = logger;
            _authorise = authorise;
        }
 
        public IActionResult Index()
        {


            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identities.First().Claims.ToList();

                if (User.IsInRole("Admin"))
                {
                    ViewData["ValidationMessage"] = "Autorize Admin";
                }
                else if (User.IsInRole("User"))
                {
                    ViewData["ValidationMessage"] = "Autorize User";
                }
                else
                {
                    ViewData["ValidationMessage"] = "Autorize";
                }
            }
            else ViewData["ValidationMessage"] = "Not ";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Autorize(AutorizeUserModel userdata)
        {
            if (ModelState.IsValid)
            {

                var res = await _authorise.AutorizeAsync(userdata);
                ViewData["ValidationMessage"] = res.ErrorMessage;
                if (res.Status == Models.StatusCode.Ok)
                {

                    await AddSessionUser(res.Data);
                    return RedirectToAction("FormCreate", "Home");
                }
                return View("Index");
            }
            ViewData["ValidationMessage"] = "Data is not required";
            return View("Index");
        }

        async Task AddSessionUser(RegisterAtUserModel userdata)
        {
            List<Claim> claims = new List<Claim>()
            {
                 new Claim(ClaimTypes.Name, userdata.UserName),
                 new Claim(ClaimTypes.Hash, userdata.IdUser.ToString()),
                 new Claim(ClaimTypes.Role, userdata.Role),
                 new Claim(ClaimTypes.Email, userdata.Email),

            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            ClaimsPrincipal avtorizeHead = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(avtorizeHead);
        }

        public async Task<ActionResult> SignOut()                              //   != авторизация
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
        [Authorize]  //(Role = "Admin")
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
