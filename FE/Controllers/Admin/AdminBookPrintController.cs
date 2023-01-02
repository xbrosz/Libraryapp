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
            List<BookPrintGridDto> prints = new List<BookPrintGridDto>();
            foreach (var d in dtos)
            {
                prints.Add(new BookPrintGridDto
                {
                    Id = d.Id,
                    BookTitle = _bookFacade.GetBookDetailByID(d.BookId).Title,
                    BranchName = _reservationFacade.GetBranchById(d.BranchId).Name
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
    }
}
