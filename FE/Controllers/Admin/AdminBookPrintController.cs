using Microsoft.AspNetCore.Mvc;

namespace FE.Controllers.Admin
{
    public class AdminBookPrintController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
