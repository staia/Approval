using Approval.Models;
using Approval.Services;
using Dapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        private readonly OrderServices _order;

        public HomeController(ILogger<HomeController> logger, OrderServices order)
        {
            _logger = logger;
            _order = order;
        }
       

        [HttpPost]
        public async Task<IActionResult> Search(string searchString)
        {
            var result = _order.Search(searchString);
            PageData data = new PageData()
            {
                ListOrders = result
            };
            return View("AllOrders", data);           
        }

        //[Authorize(Roles ="Admin")]
        public IActionResult AllOrders([FromServices] PageData page)
        {            
            return View(page);
        }

  
        public IActionResult FormCreate()
        {
            return View();
            void Hide_Click(Object Sender, EventArgs e)
            {                
            }
        }

        [HttpPost]
        public IActionResult FormCreate(ListOrder orderCreate)
        {
            if (ModelState.IsValid)
            {
                var result = _order.CreateOrder(orderCreate);
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












        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult Saved()
        {
            return View();
        }

        public IActionResult Edit(int idRequest)
        {
            var order = OrderServices.GetOrder(idRequest, Conneсt);     // завернуть в BaseResponce
            return View(order);
        }

        public ActionResult Delete(int idRequest)
        {
            ListOrder orders = new ListOrder();
            orders.DeleteItem(idRequest, Conneсt);
            return RedirectToAction("AllOrders");
        }

        public IActionResult Preview(int idRequest, string data)
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
