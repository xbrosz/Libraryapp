using BL.Facades.Facades;
using BL.Facades.IFacades;
using BL.Services.IServices;
using FE.Models;
using Microsoft.AspNetCore.Mvc;

namespace FE.Controllers
{
    public class BookController : Controller
    {
        private  IBookFacade _bookFacade;
        private IRatingService _ratingService;

        public BookController(IBookFacade bookFacade, IRatingService ratingService)
        {
            _bookFacade = bookFacade;
            _ratingService = ratingService;
        }

        public IActionResult Index(int Id)
        {
            
            var dto = _bookFacade.GetBookDetailByID(Id);

            var model = new BookDetailModel()
            {
                Id = dto.Id,
                AuthorName = dto.AuthorName,
                BookTitle = dto.Title,
                ReleaseDate = dto.Release,
                Genres = dto.BookGenres,
                RatingNumber = dto.RatingNumber
            };

            return View(model);
        }

        public IActionResult Ratings(int id)
        {
            var bookTitle = _bookFacade.GetBookDetailByID(id).Title;
            var ratings = _ratingService.GetRatingsByBook(id);

            var model = new BookRatingsViewModel()
            {
                Title = bookTitle,
                Ratings = ratings
            };

            return View(model);
        }
    }
}
