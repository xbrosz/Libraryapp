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
        private readonly ILogger<HomeController> _logger;


        private readonly IBookFacade _bookFacade;

        public IEnumerable<BookGridDto> Books { get; set; }

        public SelectList? Genres { get; set; }


        [BindProperty(SupportsGet = true)]
        public string? BookGenre { get; set; }

        public HomeController(ILogger<HomeController> logger, IBookFacade bookFacade)
        {
            _logger = logger;
            _bookFacade = bookFacade;
        }

        public IActionResult Index(int page = 1, string? searchString = null)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
               var books = _bookFacade.GetBooksBySubstring(searchString);

                var model = new BookListViewModel()
                {
                    Books = books,
                    Pagination = new PaginationViewModel(page, books.Count(), PageSize)
                };

                return View(model);
            } else
            {

           

            var books = _bookFacade.GetAllBooksSortedByRating(page, PageSize);

            var model = new BookListViewModel()
            {
                Books = books,
                Pagination = new PaginationViewModel(page, books.Count(), PageSize)
            };

            return View(model);

            }
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

       
        //public IActionResult Search(BookListViewModel model)
        //{
        //    if (string.IsNullOrWhiteSpace(model.SearchString))
        //    {
        //        return View(model);
        //    }

        //    model.Books = _bookFacade.GetBooksBySubstring(model.SearchString);

        //    return View(model);
        //}
    }
}
