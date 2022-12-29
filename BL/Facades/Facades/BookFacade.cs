using BL.DTOs;
using BL.DTOs.Author;
using BL.DTOs.Genre;
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

        public IEnumerable<BookGridDto> GetBooksByTitle(string substring)
        {
            return _bookService.GetBooksbyFilter(new BookFilterDto() { Title = substring });
        }

        public IEnumerable<BookGridDto> GetBooksByAuthorName(string name)
        {
            return new List<BookGridDto>();             // dorobit
        }

        public BookDetailDto GetBookDetailByID(int bookID)
        {
            return _bookService.GetBookDetailByID(bookID);
        }

        public IEnumerable<AuthorGridDto> GetAuthorsByName(string? searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                return _authorService.GetSortedAuthors();
            }

            var subsStrings = searchString.Trim().Split(' ');

            var filterDto = new AuthorFilterDto();

            if (subsStrings.Count() > 0 && subsStrings.ElementAt(0) != " ")
            {
                filterDto.FirstName = subsStrings.ElementAt(0);
            }

            if (subsStrings.Count() > 1 && subsStrings.ElementAt(1) != " ")
            {
                filterDto.MiddleName = subsStrings.ElementAt(1);
            }

            if (subsStrings.Count() > 2 && subsStrings.ElementAt(2) != " ")
            {
                filterDto.LastName = subsStrings.ElementAt(2);
            }

            return _authorService.GetAuthorsByName(filterDto);
        }

        public IEnumerable<BookGridDto> GetBooksForAuthorId(int? authorId)
        {
            return _bookService.GetBooksbyFilter(new BookFilterDto() { AuthorID = authorId });
        }

        public IEnumerable<GenreDto> GetAllGenres()
        {
            return _bookService.GetAllGenres();
        }

        public IEnumerable<BookGridDto> GetAllBooks()
        {
            return _bookService.GetAllBooks();
        }

        public IEnumerable<BookGridDto> GetBooksBySearchFilter(string? searchString, int? rating, string? genre)
        {
            if (string.IsNullOrWhiteSpace(searchString) && !rating.HasValue && string.IsNullOrWhiteSpace(genre))
            {
                return GetAllBooks();
            }
            return _bookService.GetBooksbyFilter(new BookFilterDto() { Title = searchString, LowestRating = rating, Genre = genre });
        }

        public void DeleteBook(int id) 
        {
            _bookService.Delete(id);
        }

        public void UpdateBook(BookUpdateDto dto)
        {
            _bookService.UpdateBook(dto);
        }
    }
}