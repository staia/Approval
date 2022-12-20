using Approval.Models;
using Approval.Services;
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


namespace Approval.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, 
                                IConfiguration configuration)
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

        [Authorize(Roles ="Admin")]
        public IActionResult AllOrders()
        {
            PageData AllOrdersTable = new PageData(Conneсt);
            return View(AllOrdersTable);
        }

        public IActionResult FormCreate()
        {
            return View();
            void Hide_Click(Object Sender, EventArgs e)
            {
                
            }
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
                var res = userdata.UserModelRegister(Conneсt);
                ViewData["ValidationMessage"] = res.ErrorMessage;
                if (res.Status == Models.StatusCode.Ok)
                {
                    // ModelState.Clear();                                   //очистеть после заполнения
                    // return View();
                    AvtotizeUser(res.Data);
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

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Preview()
        {
            return View();
        }

        public IActionResult Edit(int idRequest)
        {
            var order = OrderServices.GetOrder(idRequest, Conneсt);     // завернуть в BaseResponce
            return View(order);
        }

        [HttpPost]
        public IActionResult Delete(int idRequest, string data)
        {
            var order = OrderServices.GetOrder(idRequest, Conneсt);      
            return View("Preview", order);
        }

        //[HttpGet]
        //public IActionResult Change(int id)
        //{
        //    return View();
        //}

        [HttpPost]
        public IActionResult Change(int idRequest, string data)
        {
            var order = OrderServices.GetOrder(idRequest, Conneсt);
            return View("Preview", order);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            throw new Exception();
            // return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
