using BL.DTOs.Genre;
using BL.Facades.Facades;
using BL.Facades.IFacades;
using BL.Services.IServices;
using FE.Models.Admin;
using FE.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace FE.Controllers.Admin
{
    public class AdminGenreController : Controller
    {
        private IBookFacade _bookFacade;

        public AdminGenreController(IBookFacade bookFacade)
        {
            _bookFacade = bookFacade;
        }

        public IActionResult Index()
        {
            return View(new AdminGenreIndexViewModel()
            {
                Genres = _bookFacade.GetAllGenres().ToList()
            });
        }

        public IActionResult Edit(int genreId)
        {
            var genre = _bookFacade.GetGenreForId(genreId);

            return View(new AdminGenreEditViewModel()
            {
                Id = genre.Id,
                Name = genre.Name
            });
        }

        [HttpPost]
        public IActionResult Edit(AdminGenreEditViewModel model)
        {
            if (!ModelState.IsValid) 
            {
                return View(model);
            }

            _bookFacade.UpdateGenre(new GenreDto() { Id = model.Id, Name = model.Name });

            return RedirectToAction("Index", "AdminGenre");
        }

        public IActionResult Add()
        {
            return View(new AdminGenreAddViewModel());
        }

        [HttpPost]
        public IActionResult Add(AdminGenreAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _bookFacade.InsertGenre(new GenreDto() { Name= model.Name });

            return RedirectToAction("Index", "AdminGenre");
        }

        public IActionResult Delete(int genreId)
        {
            _bookFacade.DeleteGenre(genreId);

            return RedirectToAction("Index", "AdminGenre");
        }
    }
}
