using BL.DTOs;
using BL.DTOs.Author;
using BL.Facades.IFacades;
using FE.Models;
using Microsoft.AspNetCore.Mvc;

namespace FE.Controllers
{
    public class AuthorController : BaseController
    {
        private readonly IBookFacade _bookFacade;

        public AuthorController(IBookFacade bookFacade)
        {
            _bookFacade = bookFacade;
        }

        public IActionResult Index(int page = 1, string? searchString = null)
        {
            var authors = _bookFacade.GetAuthorsByName(searchString, page, PageSize).ToList();

            var model = new AuthorSearchViewModel()
            {
                Authors = authors,
                Pagination = new PaginationViewModel(page, authors.Count(), PageSize)
            };

            return View(model);
        }
    }
}
