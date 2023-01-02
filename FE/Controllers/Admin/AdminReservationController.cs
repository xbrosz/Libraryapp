using BL.DTOs.Reservation;
using BL.Facades.IFacades;
using BL.Services.IServices;
using DAL.Entities;
using FE.Models;
using FE.Models.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace FE.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IReservationFacade _reservationFacade;
        private readonly IUserFacade _userFacade;

        public AdminReservationController(IReservationService reservationService, IReservationFacade reservationFacade, IUserFacade userFacade)
        {
            _reservationService = reservationService;
            _reservationFacade = reservationFacade;
            _userFacade = userFacade;
        }

        public IActionResult Index(int userId)
        {
            
            var reservations = _reservationService.GetReservationsByUserId(userId);

            var model = new AdminReservationsViewModel()
            {
                reservations = reservations,
                UserName = _userFacade.GetUserById(userId).UserName,
                UserId = userId
            };

            return View(model);
        }

        public IActionResult Edit(int resId, int userId)
        {
            var dto = _reservationService.Find(resId);

            if (dto == null)
            {
                return NotFound();
            }

            var model = new ReservationEditViewModel
            {
                BookTitle = dto.BookTitle,
                BranchId = dto.Branch.Id,
                BookPrintId = dto.BookPrintId,
                Id = dto.Id,
                EndDate = dto.EndDate,
                StartDate = dto.StartDate,
                UserId = userId
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ReservationEditViewModel model)
        {
            var dto = new ReservationUpdateFormDto
            {
                Id = model.Id,
                EndDate = model.EndDate,
                StartDate = model.StartDate,
                UserId = model.UserId,
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


            return RedirectToAction("Index", new {userId = model.UserId});
        }

        [ValidateAntiForgeryToken]
        public IActionResult Delete(int resId, int userId)
        {
            var dto = _reservationService.Find(resId);

            if (dto == null)
            {
                return NotFound();
            }

            _reservationService.Delete(dto.Id);

            return RedirectToAction("Index", new {userId = userId});
        }
    }
}
