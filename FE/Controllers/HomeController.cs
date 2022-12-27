using BL.DTOs;
using BL.Facades.IFacades;
using DAL.Entities;
using FE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using static System.Reflection.Metadata.BlobBuilder;

namespace FE.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IBookFacade _bookFacade;

        public HomeController(IBookFacade bookFacade)
        {
            _bookFacade = bookFacade;
        }

        public IActionResult Index(int page = 1, string? searchString = null, int? authorId = null, string? genre = null, int? rating = null)
        {
            List<BookGridDto> books;

            if (!string.IsNullOrWhiteSpace(genre))
            {
                Console.WriteLine(genre);
            }

            if (rating.HasValue)
            {
                Console.WriteLine(rating);
            }


            if (authorId.HasValue) 
            {
                books = _bookFacade.GetBooksForAuthorId(authorId, page, PageSize).ToList();
            } 
            else if (!string.IsNullOrEmpty(searchString))
            {
                books = _bookFacade.GetBooksByTitle(searchString, page, PageSize).ToList();

            } else
            {
                books = _bookFacade.GetAllBooksSortedByRating(page, PageSize).ToList();     // nesortuje
            }

            var model = new BookListViewModel()
            {
                Books = books,
                Genres = new SelectList(_bookFacade.GetAllGenres().Select(x => x.Name).ToList()),
                Pagination = new PaginationViewModel(page, books.Count(), PageSize)
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
