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
        private readonly IBookFacade _bookFacade;
        private readonly IReservationFacade _reservationFacade;

        public AdminBookController(IBookFacade bookFacade, IReservationFacade reservationFacade)
        {
            _bookFacade = bookFacade;
            _reservationFacade = reservationFacade;
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
                Id = book.Id,
                CanBeDeleted = !_reservationFacade.GetReservationsByBookId(bookId).Any()
            };

            return View(model);
        }

        public IActionResult Delete(int bookId)
        {
            _bookFacade.DeleteBook(bookId);

            
            return RedirectToAction("Index", "AdminBook");
        }

        [HttpGet("Edit")]
        public IActionResult Edit(int bookId)
        {
            var book = _bookFacade.GetBookDetailByID(bookId);

            Console.WriteLine(bookId);

            return View(new BookEditVIewModel()
            {
                Id = bookId,
                Title = book.Title,
                AuthorName = book.AuthorName,
                Release = book.Release,
                BookGenres = book.BookGenres
            });
        }

        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BookEditVIewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Console.WriteLine(model.Id);

            _bookFacade.UpdateBook(new BookUpdateDto()
            {
                Title = model.Title,
                Release = model.Release,
                Id= model.Id
            });

            return RedirectToAction("Detail", "AdminBook", new {bookId = model.Id});
        }
    }
}
