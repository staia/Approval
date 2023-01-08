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
        private readonly OrderServices _order;

        public HomeController(ILogger<HomeController> logger, OrderServices order)
        {
            _logger = logger;
            _order = order;
        }

        public IActionResult AllOrders([FromServices] PageData page)
        {
          
            return View(page);
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




















        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            throw new Exception();
            // return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
