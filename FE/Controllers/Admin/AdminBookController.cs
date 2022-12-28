using BL.DTOs.User;
using BL.Facades.Facades;
using BL.Facades.IFacades;
using FE.Models.Admin;
using FE.Models;
using Microsoft.AspNetCore.Mvc;
using BL.DTOs;

namespace FE.Controllers.Admin
{
    public class AdminBookController : BaseController
    {
        private IBookFacade _bookFacade;

        public AdminBookController(IBookFacade bookFacade)
        {
            _bookFacade = bookFacade;
        }

        public IActionResult Index(int page = 1, string? searchString = null)
        {
            List<BookGridDto> books;

            if (!string.IsNullOrEmpty(searchString))
            {
                books = _bookFacade.GetBooksByTitle(searchString, page, PageSize).ToList();
            }
            else
            {
                books = _bookFacade.GetAllBooks(page, PageSize).ToList();
            }

            var model = new AdminBookViewModel()
            {
                Books = books,
                Pagination = new PaginationViewModel(page, books.Count(), PageSize)
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

        public IActionResult ChangeAuthor(int bookId, int authorId)
        {
            
        }
    }
}
