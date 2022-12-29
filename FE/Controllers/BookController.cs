using BL.Facades.Facades;
using BL.Facades.IFacades;
using FE.Models;
using Microsoft.AspNetCore.Mvc;

namespace FE.Controllers
{
    public class BookController : Controller
    {
        private  IBookFacade _bookFacade;

        public BookController(IBookFacade bookFacade)
        {
            _bookFacade = bookFacade;
        }

        public IActionResult Index(int Id)
        {
            
            var dto = _bookFacade.GetBookDetailByID(Id);

            var model = new BookDetailModel()
            {
                AuthorName = dto.AuthorName,
                BookTitle = dto.Title,
                ReleaseDate = dto.Release,
                Genres = dto.BookGenres,
                RatingNumber = dto.RatingNumber
            };

            return View(model);
        }
    }
}
