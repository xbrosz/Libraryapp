using BL.DTOs.Author;
using BL.DTOs.Genre;
using BL.Facades.IFacades;
using FE.Models.Admin;
using Microsoft.AspNetCore.Mvc;

namespace FE.Controllers.Admin
{
    public class AdminAuthorController : Controller
    {
        private IBookFacade _bookFacade;

        public AdminAuthorController(IBookFacade bookFacade)
        {
            _bookFacade = bookFacade;
        }

        public IActionResult Index()
        {
            return View(new AdminAuthorIndexViewModel()
            {
                Authors = _bookFacade.GetAllAuthors().ToList()
            });
        }

        public IActionResult Edit(int authorId)
        {
            var author = _bookFacade.GetAuthorDetailById(authorId);

            return View(new AdminAuthorEditViewModel()
            {
                Id = author.Id,
                FirstName= author.FirstName,
                LastName= author.LastName,
                MiddleName= author.MiddleName,
                BirthDate= author.BirthDate
            });
        }

        [HttpPost]
        public IActionResult Edit(AdminAuthorEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _bookFacade.UpdateAuthor(new AuthorUpdateDto()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName= model.LastName,
                MiddleName= model.MiddleName,
                BirthDate= model.BirthDate
            });

            return RedirectToAction("Index", "AdminAuthor");
        }

        public IActionResult Add()
        {
            return View(new AdminAuthorAddViewModel());
        }

        [HttpPost]
        public IActionResult Add(AdminAuthorAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _bookFacade.InsertAuthor(new AuthorInsertDto()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                BirthDate = model.BirthDate
            });

            return RedirectToAction("Index", "AdminAuthor");
        }

        public IActionResult Delete(int authorId)
        {
            _bookFacade.DeleteAuthor(authorId);

            return RedirectToAction("Index", "AdminAuthor");
        }
    }
}
