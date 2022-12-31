using BL.DTOs.Reservation;
using BL.Facades.Facades;
using FE.Models;
using Microsoft.AspNetCore.Mvc;

namespace FE.Controllers
{
    public class NewReservationController : Controller
    {
        private readonly BookFacade _bookFacade;
        private readonly ReservationFacade _reservationFacade;

        public NewReservationController(BookFacade bookFacade, ReservationFacade reservationFacade)
        {
            _bookFacade = bookFacade;
            _reservationFacade = reservationFacade;
        }


        public IActionResult Index(int bookID)

        {
            var dto = _bookFacade.GetBookDetailByID(bookID);
            var model = new NewReservationModel
            {
                BookID = bookID,
                BookTitle = dto.Title,
                Branches = _reservationFacade.GetAllBranches().Select(r => r.Name).ToList()
            };
            return View(model);
        }
        public IActionResult AddReservation()
        {
            return View();
        }
    }
}
