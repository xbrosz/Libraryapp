using BL.DTOs.BookPrint;
using BL.Facades.IFacades;
using FE.Models.Admin;
using Microsoft.AspNetCore.Mvc;

namespace FE.Controllers.Admin
{
    public class AdminBookPrintController : Controller
    {
        private readonly IBookFacade _bookFacade;
        private readonly IReservationFacade _reservationFacade;

        public AdminBookPrintController(IBookFacade bookFacade, IReservationFacade reservationFacade)
        {
            _bookFacade = bookFacade;
            _reservationFacade = reservationFacade;

        }

        public IActionResult Index(int Id)
        {
            var dtos = _bookFacade.GetAllBookPrints().Where(x => x.BookId == Id);
            var reservedPrints = _reservationFacade.GetAllActiveAndFutureReservations().Select(x => x.BookPrintId);
            List<BookPrintGridDto> prints = new List<BookPrintGridDto>();
            foreach (var d in dtos)
            {
                prints.Add(new BookPrintGridDto
                {
                    Id = d.Id,
                    BookTitle = _bookFacade.GetBookDetailByID(d.BookId).Title,
                    BranchName = _reservationFacade.GetBranchById(d.BranchId).Name,
                    CanBeDeleted = !reservedPrints.Contains(d.Id)
                });
            }
            var model = new AdminBookPrintIndexViewModel
            {
                Id = Id,
                bookPrints = prints
            };
            return View(model);
        }
       
        
        public IActionResult Delete(int Id, int bookId)
        {
            _bookFacade.DeleteBookPrint(Id);
            return RedirectToAction("Index", "AdminBookPrint", new {Id = bookId});
        }
        public IActionResult Add(int Id)
        {
            var model = new AdminBookPrintAddViewModel
            {
                Id = Id,
                BookTitle = _bookFacade.GetBookDetailByID(Id).Title,
                Branches = _reservationFacade.GetAllBranches().Select(x => x.Name).ToList()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Add(AdminBookPrintAddViewModel model)
        { try
            {
                int bookId = model.Id;
                int branchId = _reservationFacade.GetBranchIDByName(model.SelectedBranch);
                _bookFacade.InsertBookPrint(bookId, branchId);
                return RedirectToAction("Index", "AdminBookPrint", new { Id = model.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(nameof(AdminBookPrintAddViewModel.SelectedBranch), "Please select a valid branch");
                model.Branches = _reservationFacade.GetAllBranches().Select(x => x.Name).ToList();
                return View("Add", model);
            }
        }
    }
}
