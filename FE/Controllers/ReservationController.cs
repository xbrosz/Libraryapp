using BL.Services.IServices;
using FE.Models;
using Microsoft.AspNetCore.Mvc;

namespace FE.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService _service;

        public ReservationController(IReservationService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            int userId = 1;

            var model = new ReservationIndexViewModel()
            {
                reservations = _service.GetReservationsByUserId(userId)
            };

            return View(model);
        }
    }
}
