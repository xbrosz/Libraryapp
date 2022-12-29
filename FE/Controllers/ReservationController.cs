using BL.DTOs.Branch;
using BL.DTOs.Reservation;
using BL.Facades.IFacades;
using BL.Services.IServices;
using FE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FE.Controllers
{
    [Authorize(Roles = "User, Admin")]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IReservationFacade _reservationFacade;

        public ReservationController(IReservationService reservationService, IReservationFacade reservationFacade)
        {
            _reservationService = reservationService;
            _reservationFacade = reservationFacade;
        }
        
        public IActionResult Index()
        {
            int userId = getUserId();
            IEnumerable<ReservationsDto> reservations = new List<ReservationsDto>();

            //if (isAdmin())
            //{
            //     reservations = _reservationService.GetAll();
            //}
            //else
            //{
                reservations = _reservationService.GetReservationsByUserId(userId);
            //}


             var model = new ReservationIndexViewModel()
            {
               reservations = reservations
            };

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var dto = _reservationService.Find(id);

            if (dto == null)
            {
                return NotFound();
            }

            int userId = getUserId();

            if (!isAdmin() && dto.UserId != userId)
            { 
                return Unauthorized();
            }
            
            var model = new ReservationEditViewModel
            {
                BookTitle = dto.BookTitle,
                BranchId = dto.Branch.Id,
                BookPrintId = dto.BookPrintId,
                Id = dto.Id,
                EndDate = dto.EndDate,
                StartDate = dto.StartDate
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ReservationEditViewModel model)
        {
            int userId = getUserId();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dto = new ReservationUpdateFormDto
            {   
                Id = model.Id,
                EndDate = model.EndDate,
                StartDate = model.StartDate,
                UserId = userId,
                BranchId = model.BranchId,
                BookPrintId = model.BookPrintId
            };

            try
            {
                _reservationFacade.UpdateReservationDate(dto);
            }
            catch (InvalidOperationException e) 
            {
                ModelState.AddModelError(nameof(ReservationEditViewModel.EndDate), "Book is not available in given date range.");
                return View(model);
            }
            

            return RedirectToAction(nameof(Index));
        }

        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var dto =_reservationService.Find(id);

            if (dto == null)
            {
                return NotFound();
            }

            if (!isAdmin() || dto.UserId != getUserId())
            {
                return Unauthorized();
            }

            _reservationService.Delete(dto.Id);

            return RedirectToAction(nameof(Index));
        }

        private bool isAdmin()
        {
            return HttpContext.User.IsInRole("Admin");
        }

        private bool isUser()
        {
            return HttpContext.User.IsInRole("User");
        }

        private int getUserId()
        {
            return int.Parse(User.Identity.Name);
        }
    }
}
