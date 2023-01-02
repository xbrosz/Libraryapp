using BL.DTOs;
using BL.Facades.IFacades;
using BL.Services.IServices;
using BL.Services.Services;
using DAL.Entities;
using FE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

namespace FE.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminRatingController : Controller
    {
        private readonly IRatingService _ratingService;
        private readonly IRatingFacade _ratingFacade;

        public AdminRatingController(IRatingService ratingService, IRatingFacade ratingFacade)
        {
            _ratingService = ratingService;
            _ratingFacade = ratingFacade;
        }

        public IActionResult Index(string bookTitle, int bookId)
        {
            var ratings = _ratingService.GetRatingsByBook(bookId);

            var model = new RatingIndexViewModel()
            {
                bookTitle = bookTitle,
                ratings = ratings
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var dto = _ratingService.Find(id);

            if (dto == null)
            {
                return NotFound();
            }

            _ratingFacade.DeleteRating(dto);

            return RedirectToAction(nameof(Index));
        }
    }
}
