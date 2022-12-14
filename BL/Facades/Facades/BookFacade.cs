using BL.DTOs;
using BL.Facades.IFacades;
using BL.Services.IServices;
using DAL.Entities;

namespace BL.Facades.Facades
{
    public class BookFacade : IBookFacade
    {
        private IBookPrintService _bookPrintService { get; set; }
        private IReservationService _reservationService { get; set; }
        private IBookService _bookService { get; set; }

        public BookFacade(IBookPrintService bookPrintService, IReservationService reservationService, IBookService bookService)
        {
            _bookPrintService = bookPrintService;
            _reservationService = reservationService;
            _bookService = bookService;
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


        public IEnumerable<Book> GetAllBooksSortedByRating()
        {
            // dorobit sortovanie podla ratingu
            return _bookService.GetAll();
        }

        public IEnumerable<BookGridDto> GetBooksBySubstring(string substring)
        {
            var substrings = substring.ToLower().Split(' ').ToList().Select(a => a != " ");

            Console.WriteLine(substring);
            foreach(var a in substrings)
            {
                Console.WriteLine(a);
            }

            List<BookGridDto> books= new List<BookGridDto>();

            foreach(var str in substrings)
            {
                //books.Union(_bookService.GetBooksByAuthorName(str));
                //books.Union(_bookService.GetBooksByBookTitle(str));
            }

            return new List<BookGridDto>();

        public BookDetailDto GetBookDetailByID(int bookID)
        {
            return _bookService.GetBookDetailByID(bookID);

        }
    }
}
