using BL.DTOs;
using BL.Facades.IFacades;
using BL.Services.IServices;
using DAL.Entities;
using FE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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

        public IActionResult Index()
        {
            var model = new RatingIndexViewModel()
            {
                ratings = _ratingService.GetAll()
            };

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var dto = _ratingService.Find(id);

            if (dto == null)
            {
                return NotFound();
            }

            var model = new RatingEditViewModel
            {
                Id = dto.Id,
                BookTitle = dto.BookTitle,
                RatingNumber = dto.RatingNumber,
                Comment = dto.Comment,
                BookId = dto.BookId
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RatingEditViewModel model)
        {
            var rating = _ratingService.Find(model.Id);

            if(rating == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dto = new RatingDto
            {
                Id = model.Id,
                BookTitle = model.BookTitle,
                Comment = model.Comment,
                RatingNumber = model.RatingNumber,
                BookId = model.BookId,
                UserId = rating.UserId
            };

            _ratingFacade.UpdateRating(dto);

            return RedirectToAction(nameof(Index));
        }
    }
}
