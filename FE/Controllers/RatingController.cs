using BL.DTOs;
using BL.Facades.IFacades;
using BL.Services.IServices;
using DAL.Entities;
using FE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FE.Controllers
{
    [Authorize(Roles = "User, Admin")]
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
            int userId = getUserId();

            var model = new RatingIndexViewModel() 
            {
                ratings = _ratingService.GetRatingsByUser(userId),
                awaitingRatings = _ratingFacade.GetAwaitingRatingsByUser(userId)
            };

            return View(model);
        }

        public IActionResult Add(int bookId, string bookTitle)
        {
            var userId = getUserId();

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
            _ratingFacade.InsertRating(dto);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var dto = _ratingService.Find(id);

            if (dto == null)
            {
                return NotFound();
            }

            if (dto.UserId != getUserId())
            {
                return Unauthorized();
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
            var userId = getUserId();

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

            _ratingFacade.UpdateRating(dto);

            return RedirectToAction(nameof(Index));
        }

        private int getUserId()
        {
            return int.Parse(User.Identity.Name);
        }
    }
}
