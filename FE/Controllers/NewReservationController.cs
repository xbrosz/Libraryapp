using BL.DTOs.Reservation;
using BL.Facades.Facades;
using BL.Facades.IFacades;
using FE.Models;
using Microsoft.AspNetCore.Mvc;

namespace FE.Controllers
{
    public class NewReservationController : Controller
    {
        private readonly IBookFacade _bookFacade;
        private readonly IReservationFacade _reservationFacade;

        public NewReservationController(IBookFacade bookFacade, IReservationFacade reservationFacade)
        {
            _bookFacade = bookFacade;
            _reservationFacade = reservationFacade;
        }

        public IActionResult Index(int Id)
        {
            int userId = getUserId();
            var dto = _bookFacade.GetBookDetailByID(Id);
            var model = new NewReservationModel
            {
                Id = Id,
                BookTitle = dto.Title,
                Branches = _reservationFacade.GetAllBranches().Select(r => r.Name).ToList(),
                UserId = userId,
            };
            return View(model);
        }
        public IActionResult Add(int Id, string branchName, DateTime start, DateTime end)
        {
            var dto = new ReservationCreateFormDto
            {
                BookId = Id,
                StartDate = start,
                EndDate = end,
                UserId = getUserId(),
                BranchId = _reservationFacade.GetBranchIDByName(branchName)
            };
            _reservationFacade.ReserveBook(dto);
            var model = new ReservationIndexViewModel
            {
                reservations = _reservationFacade.GetReservationsByUserId(getUserId())
            };
            return View(model);
        }

        private int getUserId()
        {
            return int.Parse(User.Identity.Name);
        }
    }
}
