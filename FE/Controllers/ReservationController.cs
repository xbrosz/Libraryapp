using BL.DTOs.Branch;
using BL.DTOs.Reservation;
using BL.Facades;
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
        private readonly IReservationFacade _reservationFacade;

        public ReservationController(IReservationService reservationService, IReservationFacade reservationFacade)
        {
            _reservationService = reservationService;
            _reservationFacade = reservationFacade;
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
            var dto = _reservationService.Find(id);

            if (dto == null)
            {
                return NotFound();
            }

            var model = new ReservationEditViewModel
            {
                BookTitle = dto.BookTitle,
                Branch = dto.Branch
                 ,
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
            //if (!ModelState.IsValid)
            //{
             //   return View(model);
            //}

            var dto = new ReservationUpdateFormDto
            {   
                Id = model.Id,
                EndDate = model.EndDate,
                StartDate = model.StartDate,
                UserId = 1,
                BranchId = 1,
                BookPrintId = model.BookPrintId
            };

            _reservationFacade.UpdateReservationDate(dto);

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

            _reservationService.Delete(dto.Id);

            return RedirectToAction(nameof(Index));
        }
    }
}
