using BL.Facades.Facades;
using BL.Services.Services;
using FE.Models;
using Microsoft.AspNetCore.Mvc;

namespace FE.Controllers
{
    public class RatingListController : Controller
    {
        private readonly RatingService _ratingService;

        public RatingListController(RatingService ratingService)
        {
            _ratingService = ratingService;
        }

        public IActionResult Index(int bookID)
        {
            RatingListIndexViewModel model = new RatingListIndexViewModel
            {
                Ratings = _ratingService.GetRatingsByBook(bookID)
            };
            return View(model);
        }
    }
}
