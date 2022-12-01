using Approval.Models;
using Approval.Views.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
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
                return new SqlConnection(_configuration.GetConnectionString("ConnectToMyDataBese"));
            }
        }






        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Autorize(RegisterAtUser userdata)
        {
            UserData user = new UserData(Connext); // проверять userdata 


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
