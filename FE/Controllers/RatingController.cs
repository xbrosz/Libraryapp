using BL.DTOs;
using BL.DTOs.Reservation;
using BL.Facades;
using BL.Services.IServices;
using BL.Services.Services;
using FE.Models;
using Microsoft.AspNetCore.Mvc;

namespace FE.Controllers
{
    public class RatingController : Controller
    {
        private readonly IRatingService _ratingService;
        private readonly IRatingFacade _ratingFacade;

        public RatingController(IRatingService ratingService, IRatingFacade ratingFacade)
        {
            _ratingService = ratingService;
            _ratingFacade = ratingFacade;
        }

        public IActionResult Index()
        {
            // load user from auth
            int userId = 1;

            var model = new RatingIndexViewModel()
            {
                ratings = _ratingService.GetRatingsByUser(userId),
                awaitingRatings = _ratingFacade.GetAwaitingRatingsByUser(userId)
                
            };


            return View(model);
        }

        public IActionResult Add(int bookId, string bookTitle)
        {
            // change to user from auth
            var userId = 1;

            return View(new RatingInsertViewModel() { BookId = bookId, UserId = userId, BookTitle = bookTitle });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(RatingInsertViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dto = model.ToDto();
            _ratingService.Insert(dto);

            return RedirectToAction(nameof(Index));
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
                BookId = dto.BookId            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RatingEditViewModel model)
        {
            //load user from auth
            var userId = 1;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dto = new RatingDto
            {
                Id = model.Id,
                BookTitle= model.BookTitle,
                Comment = model.Comment,
                RatingNumber = model.RatingNumber,
                BookId= model.BookId,
                UserId = userId
            };

            _ratingService.Update(dto);

            return RedirectToAction(nameof(Index));
        }
    }
}
