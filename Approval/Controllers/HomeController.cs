using Approval.Models;
using Approval.Views.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Approval.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }







        public IActionResult Index()
        {
            return View("Privacy");
        }
        [HttpPost]
        public IActionResult Autorize(RegisterAtUser userdata)
        {
            UserData user = new UserData(Connect); // проверять userdata 


            if (userdata.Password != null)
            {
                return RedirectToAction("Privacy");
            }
            return View("Index");
        }






        public IActionResult Privacy()
        {
            return View();
        }









        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
