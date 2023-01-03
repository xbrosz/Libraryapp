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

        public IActionResult Index()
        {
            var dtos = _bookFacade.GetAllBookPrints();
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
                bookPrints = prints
            };
            return View(model);
        }
       
        [HttpPost]
        public IActionResult Delete(int Id)
        {
            _bookFacade.DeleteBookPrint(Id);
            return RedirectToAction("Index", "AdminBookPrint");
        }
        public IActionResult Add()
        {
            var model = new AdminBookPrintAddViewModel
            {
                Books = _bookFacade.GetAllBooks().Select(x => x.Title).ToList(),
                Branches = _reservationFacade.GetAllBranches().Select(x => x.Name).ToList()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Add(AdminBookPrintAddViewModel model)
        {
            int bookId = _bookFacade.GetBooksByTitle(model.SelectedBook).First().Id;
            int branchId = _reservationFacade.GetBranchIDByName(model.SelectedBranch);
            _bookFacade.InsertBookPrint(bookId, branchId);
            return RedirectToAction("Index", "AdminBookPrint");
        }
    }
}
