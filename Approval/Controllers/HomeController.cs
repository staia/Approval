using Approval.Models;
using Approval.Services;
using Dapper;
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
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Approval.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;         // считыванье с файла

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;  
        }
        public IDbConnection Conneсt
        {
            get
            {
                return new SqlConnection(_configuration.GetConnectionString("Remote"));
            }
        }



        public IActionResult AllOrders()
        {
            PageData AllOrdersTable = new PageData(Conneсt);
            return View(AllOrdersTable);
        }

        public IActionResult FormCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FormCreate(OrderCreate orderCreate)
        {
            if (ModelState.IsValid)
            {
                var result = orderCreate.CreateOrder(Conneсt);
                ViewData["ValidationMessage"] = result.ErrorMessage;
                if (result.Status == Models.StatusCode.Ok)
                {
                    ModelState.Clear();
                    return View();
                }
                return View(orderCreate);

            }
            ViewData["ValidationMessage"] = "Data is not required";
            return View(orderCreate);
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
                else if(User.IsInRole("User"))
                {
                    ViewData["ValidationMessage"] = "Autorize User";
                }
                else
                {
                    ViewData["ValidationMessage"] = "Autorize Purchase";     
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

                var res = await userdata.AutorizeAsync(Conneсt);
                ViewData["ValidationMessage"] = res.ErrorMessage;
                if (res.Status == Models.StatusCode.Ok)
                {
 
                    await AvtotizeUser(res.Data);
                    return RedirectToAction("Index");
                }
                return View("Index");
            }
            ViewData["ValidationMessage"] = "Data is not required";
            return View("Index");
        }

        async Task AvtotizeUser(RegisterAtUserModel userdata)
        {
            List<Claim> claims = new List<Claim>()                
            {
                 new Claim(ClaimTypes.Hash, userdata.IdUser.ToString()),
                 new Claim(ClaimTypes.Role, userdata.Role),          
                 new Claim(ClaimTypes.Email, userdata.Email),          
                
            }; 
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Hash, ClaimTypes.Role);
            ClaimsPrincipal avtorizeHead = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(avtorizeHead);
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
