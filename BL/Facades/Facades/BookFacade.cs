using BL.DTOs;
using BL.Facades.IFacades;
using BL.Services.IServices;
using DAL.Entities;
using System.Linq;

namespace BL.Facades.Facades
{
    public class BookFacade : IBookFacade
    {
        private IBookPrintService _bookPrintService { get; set; }
        private IReservationService _reservationService { get; set; }
        private IBookService _bookService { get; set; }
        private IAuthorService _authorService { get; set; }

        public BookFacade(IBookPrintService bookPrintService, IReservationService reservationService, IBookService bookService, IAuthorService authorService)
        {
            _bookPrintService = bookPrintService;
            _reservationService = reservationService;
            _bookService = bookService;
            _authorService = authorService;
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

        public IEnumerable<BookGridDto> GetAllBooksSortedByRating(int page, int pageSize)
        {
            return _bookService.GetBooksbyFilter(new BookFilterDto() { PageSize = pageSize, RequestedPageNumber = page, SortCriteria = nameof(Book.RatingNumber) });
        }

        // Pri vyhladavanie podla mena autora, najskor ziskat id autora z databazy a potom podla jeho id az vyhladavat knihu

        public IEnumerable<BookGridDto> GetBooksBySubstring(string substring)
        {
            var substrings = substring.ToLower().Split(' ').ToList().Where(a => a != " ");

            foreach (var a in substrings)
            {
                Console.WriteLine("Substring: " + a);
            }

            var books = new List<BookGridDto>();

            foreach (var str in substrings)
            {
                books = books.Union(GetBooksForAuthorName(str)).ToList();
                books = books.Union(_bookService.GetBooksbyFilter(new BookFilterDto() { Title = str })).ToList();
            }

            return books;
        }

        private IEnumerable<BookGridDto> GetBooksForAuthorName(string name)
        {
            var authors = _authorService.GetAuthorsByName(name);
            var books = new List<BookGridDto>();
            foreach(var author in authors)
            {
                books = books.Union(_bookService.GetBooksbyFilter(new BookFilterDto() { AuthorID = author.Id })).ToList();
            }
            return books;
        }

        public BookDetailDto GetBookDetailByID(int bookID)
        {
            return _bookService.GetBookDetailByID(bookID);
        }
    }
}
