using BL.DTOs.Branch;
using BL.DTOs.Reservation;
using BL.Services.IServices;
using BL.Services.Services;
using DAL.Entities;
using FE.Models;
using Microsoft.AspNetCore.Mvc;

namespace FE.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public IActionResult Index()
        {
            int userId = 1;

             var model = new ReservationIndexViewModel()
            {
               reservations = _reservationService.GetReservationsByUserId(userId)
            };


            return View(model);
        }
        public IActionResult Edit(int id)
        {
            //var dto = repo.Get(id);

            var dto = new ReservationsDto
            {
                BookTitle = "title",
                Branch = new BranchDto { Name = "branch" }
                ,
                Id = 1,
                EndDate = DateTime.Now.AddDays(-10),
                StartDate = DateTime.Now.AddDays(4)
            };

            if (dto == null)
            {
                return NotFound();
            }

            var model = new ReservationEditViewModel
            {
                BookTitle = "title",
                Branch = new BranchDto { Name = "branch" }
                 ,
                Id = 1,
                EndDate = DateTime.Now.AddDays(-10),
                StartDate = DateTime.Now.AddDays(4)
            };

            //var model = dto.Adapt<ReservationEditViewModel>();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ReservationEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //var dto = model.ToDto();
            //_reservationService.Update(dto);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var dto = new ReservationsDto();
            //var dto =_reservationService.Get(id);
            if (dto == null)
            {
                return NotFound();
            }

            //_studentRepository.Delete(dto);

            return RedirectToAction(nameof(Index));
        }
    }
}
