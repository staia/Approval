using Approval.Models;
using Approval.Views.Home;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Approval.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;  
        }
        public IDbConnection Connext
        {
            get
            {
                return new SqlConnection(_configuration.GetConnectionString("Remote"));
            }
        }






        public IActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                var user = User.Identities.First().Claims.ToList();

                if (User.IsInRole("Admin"))
                {
                    ViewData["ValidationMessage"] = "Autorize Admin";
                }
                else
                {
                    ViewData["ValidationMessage"] = "Autorize User";
                }
            }
            else ViewData["ValidationMessage"] = "Not ";

            return View();
        }
        [HttpPost]   
        public IActionResult Autorize(RegisterAtUserModel userdata)
        {
            if (ModelState.IsValid)
            {
                //var responce = userdata.Autorize(Conneсt);
                //if(responce.Status = Models.StatusCode.Ok)
                //{
                //}

                var res = userdata.UserModelRegister(Connext);
                ViewData["ValidationMessage"] = res.ErrorMessage;
                if (res.Status == Models.StatusCode.Ok)
                {
                    // ModelState.Clear();                                   //очистеть после заполнения
                    // return View();
                    AvtotizeUser(res.Date);
                    return RedirectToAction("Index");
                }
                return View("Index", userdata);
            }
            ViewData["ValidationMessage"] = "Data is not required";
            return View("Index", userdata);
        }

         
        void AvtotizeUser(RegisterAtUserModel userdata)
        {
            List<Claim> claims = new List<Claim>()                 // личность на сервери 
            {
                 new Claim(ClaimTypes.Hash, userdata.IdUser.ToString()),
                 new Claim(ClaimTypes.Role, userdata.Role),          
                 new Claim(ClaimTypes.Email, userdata.Email),          
                
            }; 
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Hash, ClaimTypes.Role);
            ClaimsPrincipal avtorizeHead = new ClaimsPrincipal(identity);
            HttpContext.SignInAsync(avtorizeHead);
        }

        public ActionResult SignOut()                              //   != авторизация
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
        [Authorize]  //(Role = "Admin")
        public IActionResult Privacy()
        {
            return View();
        }









        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            throw new Exception();
            // return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
