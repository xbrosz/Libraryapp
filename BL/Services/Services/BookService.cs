using AutoMapper;
using BL.DTOs;
using BL.DTOs.BookGenre;
using BL.QueryObjects.IQueryObject;
using BL.QueryObjects.QueryObjects;
using BL.Services.GenericService;
using BL.Services.IServices;
using DAL.Entities;
using Infrastructure.UnitOfWork;

namespace BL.Services.Services
{
    public class BookService : GenericService<Book, BookDetailDto, BookPrintDto, BookDetailDto>, IBookService
    {
        IQueryObject<BookFilterDto, BookGridDto> _bookQueryObject;
        IQueryObject<BookGenreFilterDto, BookGenreDto> _bookGenreQueryObject;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper, IQueryObject<BookFilterDto, BookGridDto> bookQueryObject, IQueryObject<BookGenreFilterDto, BookGenreDto> bookGenreQueryObject) : base(unitOfWork, mapper, unitOfWork.BookRepository)
        {
            _bookQueryObject = bookQueryObject;
            _bookGenreQueryObject = bookGenreQueryObject;
        }

        public IEnumerable<BookGridDto> GetBooksbyAuthorID(int authorID)
        {
            return _bookQueryObject.ExecuteQuery(new BookFilterDto
            {
                AuthorID = authorID,

            }).Items;
        }

        public IEnumerable<Genre> GetGenresForBookId(int bookId)
        {
            return _bookGenreQueryObject.ExecuteQuery(new BookGenreFilterDto() { BookId = bookId}).Items.Select(x => x.Genre);
        }

        public BookDetailDto GetBookDetailByID(int bookID)
        {
            return _mapper.Map<BookDetailDto>(Find(bookID));
        }

        public IEnumerable<BookGridDto> GetBooksbyFilter(BookFilterDto filter)
        {
            return _bookQueryObject.ExecuteQuery(filter).Items;
        }
    }
}
