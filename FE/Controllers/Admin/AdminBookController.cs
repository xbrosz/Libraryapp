using BL.DTOs.User;
using BL.Facades.Facades;
using BL.Facades.IFacades;
using FE.Models.Admin;
using FE.Models;
using Microsoft.AspNetCore.Mvc;
using BL.DTOs;

namespace FE.Controllers.Admin
{
    public class AdminBookController : Controller
    {
        private IBookFacade _bookFacade;

        public AdminBookController(IBookFacade bookFacade)
        {
            _bookFacade = bookFacade;
        }

        public IActionResult Index(string? searchString = null)
        {
            List<BookGridDto> books;

            if (!string.IsNullOrEmpty(searchString))
            {
                books = _bookFacade.GetBooksByTitle(searchString).ToList();
            }
            else
            {
                books = _bookFacade.GetAllBooks().ToList();
            }

            var model = new AdminBookViewModel()
            {
                Books = books
            };

            return View(model);
        }

        public IActionResult Detail(int bookId)
        {
            var book = _bookFacade.GetBookDetailByID(bookId);

            var model = new BookDetailModel() 
            {
                AuthorName = book.AuthorName,
                BookTitle = book.Title,
                ReleaseDate = book.Release,
                Genres = book.BookGenres,
                RatingNumber = book.RatingNumber,
                Id = book.Id
            };

            return View(model);
        }

        //public IActionResult ChangeAuthor(int bookId, int authorId)
        //{
            
        //}
    }
}
