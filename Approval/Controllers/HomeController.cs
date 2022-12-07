using Approval.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
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

        public IDbConnection Connect
        {
            get
            {
                return new SqlConnection(_configuration.GetConnectionString("ConnectToMyDB"));
            }
        }

        public IDbConnection ConnectUser
        {
            get
            {
                return new SqlConnection(_configuration.GetConnectionString("Connect"));
            }
        }

        public IActionResult Index()
        {
            return View(new UserModel());
        }

        [HttpPost]
        public IActionResult Index(UserModel model)
        {
            if(ModelState.IsValid)
            {                
                var responce = model.Autorize(ConnectUser);
                if (responce.Status == Models.StatusCode.Ok)
                {
                    AutorizeUser(responce.Data);
                    return RedirectToAction("AllOrders");
                }                 
                //ConnectUser
            }
            return View(model);
        }

        private void AutorizeUser(UserModel model)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, model.Username),
                new Claim(ClaimTypes.Role, model.Role)
            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            ClaimsPrincipal autorizeHead = new ClaimsPrincipal(identity);
            HttpContext.SignInAsync(autorizeHead);
        }

        public IActionResult SignOut()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }




        //[Authorize]
        public IActionResult AllOrders()
        {
            PageData AllOrdersTable = new PageData(Connect);
            return View(AllOrdersTable);
        }



        public IActionResult FormCreate()
        {
            return View();
        }
        //public IActionResult FormCreate(ModelAttributes OrderCreate)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        return RedirectToAction("AllOrders", "Home");
        //    }
            
        //    return View();
        //}


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
