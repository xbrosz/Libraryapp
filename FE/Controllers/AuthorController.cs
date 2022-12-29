using BL.DTOs;
using BL.DTOs.Author;
using BL.Facades.IFacades;
using FE.Models;
using Microsoft.AspNetCore.Mvc;

namespace FE.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IBookFacade _bookFacade;

        public AuthorController(IBookFacade bookFacade)
        {
            _bookFacade = bookFacade;
        }

        public IActionResult Index(string? searchString = null)
        {
            var authors = _bookFacade.GetAuthorsByName(searchString).ToList();

            var model = new AuthorSearchViewModel()
            {
                Authors = authors
            };

            return View(model);
        }
    }
}
