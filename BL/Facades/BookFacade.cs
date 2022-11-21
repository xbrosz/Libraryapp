using BL.DTOs;
using BL.Services.IServices;

namespace BL.Facades
{
    public class BookFacade
    {
        private IBookService _bookService { get; set; }
        private IBookPrintService _bookPrintService { get; set; }
        private IReservationService _reservationService { get; set; }

        public BookFacade(IBookService bookService, IBookPrintService bookPrintService, IReservationService reservationService)
        {
            _bookService = bookService;
            _bookPrintService = bookPrintService;
            _reservationService = reservationService;
        }

        public IEnumerable<BookPrintDto> GetAvailableBookPrints(int bookId, int branchId, DateTime from, DateTime to)
        {
            var reservedBookPrints = _reservationService.GetReservationsInDateRangeByBookAndBranch(bookId, branchId, from, to).Select(r => r.BookPrintId);
            var books = _bookPrintService.GetBookPrintsByBranchIDAndBookID(branchId, bookId);
            return books.Where(b => !reservedBookPrints.Contains(b.BookId));
        }

        public int GetNumOfAvailablePrints(int bookId, int branchId, DateTime from, DateTime to)
        {
            return GetAvailableBookPrints(bookId, branchId, from, to).Count();
        }
    }
}
