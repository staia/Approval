using Microsoft.AspNetCore.Mvc;

namespace Approval.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult AccessDenied()
        {
            return RedirectToAction("AccessDenied", "Home");
        }
         
    }
}
