using BL.DTOs;
using BL.Facades.IFacades;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FE.Controllers
{
    public class BookListContoller : Controller
    {
        private readonly IBookFacade _bookFacade;

        public IEnumerable<Book> Books { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? Genres { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? BookGenre { get; set; }

        public BookListContoller(IBookFacade bookFacade)
        {
            _bookFacade = bookFacade;
        }

        public async Task OnGetAsync()
        {

            //if (string.IsNullOrWhiteSpace(SearchString))
            //{
                Books = _bookFacade.GetAllBooksSortedByRating();
            //    return;
            //}

            //Books = _bookFacade.GetBooksBySubstring(SearchString);
        }

    }
}
