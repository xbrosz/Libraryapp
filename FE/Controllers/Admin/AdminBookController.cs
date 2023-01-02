using BL.DTOs.User;
using BL.Facades.Facades;
using BL.Facades.IFacades;
using FE.Models.Admin;
using FE.Models;
using Microsoft.AspNetCore.Mvc;
using BL.DTOs;
using BL.Services.Services;
using Microsoft.AspNetCore.Authorization;
using BL.Services.IServices;
using BL.DTOs.Genre;
using BL.DTOs.Book;

namespace FE.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminBookController : Controller
    {
        private readonly IBookFacade _bookFacade;
        private readonly IReservationFacade _reservationFacade;
        private readonly IRatingService _ratingService;
        private readonly IBookService _bookService;

        public AdminBookController(IBookFacade bookFacade, IReservationFacade reservationFacade, IRatingService ratingService, IBookService bookService)
        {
            _bookFacade = bookFacade;
            _reservationFacade = reservationFacade;
            _ratingService = ratingService;
            _bookService = bookService;
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

            foreach(var rating in _ratingService.GetRatingsByBook(bookId))
            {
                _ratingService.Delete(rating.Id);
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

        public IActionResult Add()
        {
            return View(new AdminAddBookViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAuthor(AdminAddBookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return View(new AdminAddAuthorToBookViewModel()
            {
                Title = model.Title,
                Release = model.Release,
                Authors = _bookFacade.GetAllAuthors().ToList()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(string title, DateTime release, int authorId)
        {
            _bookService.Insert(new BookInsertDto()
            {
                Title = title,
                AuthorId = authorId,
                Release = release
            });

            return RedirectToAction("Index", "AdminBook");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeGenresList(ChangeBookGenresViewModel model)
        {
            _bookFacade.DeleteBookGenreForBookId(model.BookId);

            if (model.CheckedGenres != null)
            {
                foreach (var genre in model.CheckedGenres)
                {
                    _bookFacade.InsertBookGenre(genre, model.BookId);
                }
            }

            return RedirectToAction("Detail", "AdminBook", new { bookId = model.BookId });
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

        [ValidateAntiForgeryToken]
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
