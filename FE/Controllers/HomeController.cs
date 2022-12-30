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
    public class HomeController : Controller
    {
        private readonly IBookFacade _bookFacade;

        public HomeController(IBookFacade bookFacade)
        {
            _bookFacade = bookFacade;
        }

        public IActionResult Index(string? searchString = null, int? authorId = null, string? genre = null, int? rating = null)
        {
            List<BookGridDto> books;

            if (authorId.HasValue)
            {
                books = _bookFacade.GetBooksForAuthorId(authorId).ToList();
            }
            else
            {
                books = _bookFacade.GetBooksBySearchFilter(searchString, rating, genre).ToList();
            }

            var model = new BookListViewModel()
            {
                Books = books,
                Genres = new SelectList(_bookFacade.GetAllGenres().Select(x => x.Name).ToList())
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
