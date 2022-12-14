using BL.Facades.Facades;
using FE.Models;
using Microsoft.AspNetCore.Mvc;

namespace FE.Controllers
{
    public class BookDetailController : Controller
    {
        private  BookFacade _bookFacade;

        public BookDetailController(BookFacade bookFacade)
        {
            _bookFacade = bookFacade;
        }

        public IActionResult Index(int bookID)
        {
            var dto = _bookFacade.GetBookDetailByID(bookID);
            var model = new BookDetailModel
            {
                AuthorName = dto.AuthorName,
                BookTitle = dto.Title,
                ReleaseDate = dto.Release,
                Genres = dto.BookGenres
            };
            return View(model);
        }
    }
}
