using Approval.Models;
using Approval.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

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
       

        //[Authorize(Roles ="Admin")]
        public IActionResult AllOrders([FromServices] PageData page)
        {            
            return View(page);
        }

  
        public IActionResult FormCreate()
        {
            return View();
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
                    return RedirectToAction("Saved", "Home");                     
                }
                return View(orderCreate);
            }
            ViewData["ValidationMessage"] = "Data is not required";
            return View(orderCreate);
        }      

        public IActionResult Edit(int idRequest)
        {
            var order = _order.GetOrder(idRequest);     // завернуть в BaseResponce
            return View(order);
        }
        [HttpPost]
        public IActionResult Edit(ListOrder orderCreate)
        {
            string Titel = orderCreate.Title;

            return RedirectToAction("Saved", "Home");

            //if (ModelState.IsValid)
            //{
            //    var result = _order.CreateOrder(orderCreate);
            //    ViewData["ValidationMessage"] = result.ErrorMessage;
            //    if (result.Status == Models.StatusCode.Ok)
            //    {
            //        ModelState.Clear();
            //        return RedirectToAction("Saved", "Home");
            //    }
            //    return View(orderCreate);
            //}
            //ViewData["ValidationMessage"] = "Data is not required";
            //return View(orderCreate);
        }


        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult Saved()
        {
            return View();
        }
        public ActionResult Delete(int idRequest)
        {
            ListOrder orders = new ListOrder();
            _order.DeleteItem(idRequest);
            return RedirectToAction("AllOrders");
        }

        public IActionResult Preview(int idRequest)
        {
            var order = _order.GetOrder(idRequest);
            return View("Preview", order);
        }

        public IActionResult MessageApprove(int idRequest)
        {
            var order = _order.OrderApprove(idRequest);
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
