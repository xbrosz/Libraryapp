using BL.DTOs.User;
using BL.Facades.Facades;
using BL.Facades.IFacades;
using FE.Models.Admin;
using FE.Models;
using Microsoft.AspNetCore.Mvc;
using BL.DTOs;
using BL.Services.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

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
                CanBeDeleted = !_reservationFacade.GetActiveReservationsByBookId(bookId).Any()
            };

            return View(model);
        }

        public IActionResult Delete(int bookId)
        {
            if (_reservationFacade.GetActiveReservationsByBookId(bookId).Any())
            {
                throw new Exception("Book cannot be deleted due still active reservations");
            }

            _bookFacade.DeleteBook(bookId);
            _reservationFacade.DeleteReservationsForBookId(bookId);
            
            return RedirectToAction("Index", "AdminBook");
        }


        public IActionResult EditTitle(int bookId)
        {
            var book = _bookFacade.GetBookDetailByID(bookId);

            return View(new BookEditVIewModel()
            {
                Id = bookId,
                Title = book.Title,
            });
        }

        public IActionResult ChangeGenresList(int bookId)
        {
            var model = new ChangeBookGenresViewModel()
            {
                BookId = bookId,
                Genres = _bookFacade.GetAllGenres().ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeGenresList(ChangeBookGenresViewModel model)
        {
            _bookFacade.DeleteBookGenreForBookId(model.BookId);

            foreach(var genre in model.CheckedGenres)
            {
                _bookFacade.InsertBookGenre(genre, model.BookId);
            }

            return RedirectToAction("Detail", "AdminBook", new { bookId = model.BookId });
        }

        public IActionResult ChangeGenres(int bookId, int authorId)
        {
            _bookFacade.UpdateBook(new BookUpdateDto() { Id = bookId, AuthorId = authorId });

            return RedirectToAction("Detail", "AdminBook", new { bookId = bookId });
        }

        public IActionResult ChangeAuthorList(int bookId)
        {
            var model = new ChangeBookAuthorViewModel()
            {
                BookId = bookId,
                Authors = _bookFacade.GetAllAuthors()
            };

            return View(model);
        }

        public IActionResult ChangeAuthor(int bookId, int authorId)
        {
            _bookFacade.UpdateBook(new BookUpdateDto() { Id = bookId, AuthorId = authorId});

            return RedirectToAction("Detail", "AdminBook", new {bookId = bookId});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditTitle(BookEditVIewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _bookFacade.UpdateBook(new BookUpdateDto()
            {
                Title = model.Title,
                Id= model.Id
            });

            return RedirectToAction("Detail", "AdminBook", new {bookId = model.Id});
        }

        public IActionResult EditRelease(int bookId)
        {
            var book = _bookFacade.GetBookDetailByID(bookId);

            return View(new BookEditVIewModel()
            {
                Id = bookId,
                Release = book.Release,
                Title = book.Title
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditRelease(BookEditVIewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _bookFacade.UpdateBook(new BookUpdateDto()
            {
                Release = model.Release,
                Id = model.Id
            });

            return RedirectToAction("Detail", "AdminBook", new { bookId = model.Id });
        }
    }
}
