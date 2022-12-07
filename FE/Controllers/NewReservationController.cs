using Microsoft.AspNetCore.Mvc;

namespace FE.Controllers
{
    public class NewReservationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
