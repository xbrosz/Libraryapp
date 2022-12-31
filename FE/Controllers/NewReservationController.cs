using BL.DTOs.Reservation;
using BL.Facades.Facades;
using Microsoft.AspNetCore.Mvc;

namespace FE.Controllers
{
    public class NewReservationController : Controller
    {
        private readonly ReservationFacade _reservationFacade;

        public NewReservationController(ReservationFacade reservationFacade)
        {
            _reservationFacade = reservationFacade;
        }

        [ValidateAntiForgeryToken]
        public IActionResult AddReservation(int userID, int bookID, int branchID, DateTime startDate, DateTime endDate)
        {
            _reservationFacade.ReserveBook(new ReservationCreateFormDto
            {
                BookId = bookID,
                BranchId = branchID,
                UserId = userID,
                StartDate = startDate,
                EndDate = endDate
            });
            return View();
        }
    }
}
