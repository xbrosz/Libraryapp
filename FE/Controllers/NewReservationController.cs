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
            try
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
            catch (Exception ex)
            {
                return RedirectToAction("Login", "User");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(NewReservationModel newModel)
        {
            if (newModel.ToDate <= newModel.FromDate)
            {
                ModelState.AddModelError(nameof(NewReservationModel.ToDate), "Invalid date range");
                newModel.Branches = _reservationFacade.GetAllBranches().Select(r => r.Name).ToList();
                return View("Index", newModel);
            }
            try {
            var dto = new ReservationCreateFormDto
            {
                BookId = newModel.Id,
                StartDate = newModel.FromDate,
                EndDate = newModel.ToDate,
                UserId = getUserId(),
                BranchId = _reservationFacade.GetBranchIDByName(newModel.SelectedBranch)
            };
            
                _reservationFacade.ReserveBook(dto);
            }
            catch (IndexOutOfRangeException ex)
            {
                ModelState.AddModelError(nameof(NewReservationModel.ToDate), "Please select a valid branch");

                newModel.Branches = _reservationFacade.GetAllBranches().Select(r => r.Name).ToList();
                return View("Index", newModel);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(nameof(NewReservationModel.ToDate), "No prints available for selected date range");
                //return RedirectToAction("Index", new { Id = newModel.Id});
                newModel.Branches =_reservationFacade.GetAllBranches().Select(r => r.Name).ToList();
                return View("Index", newModel);
            } 
            
            var model = new ReservationIndexViewModel
            {
                reservations = _reservationFacade.GetReservationsByUserId(getUserId())
            };
            return RedirectToAction("Index", "Reservation");
        }

        private int getUserId()
        {
            
            return int.Parse(User.Identity.Name);
           
        }
    }
}
