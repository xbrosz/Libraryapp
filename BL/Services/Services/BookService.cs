using AutoMapper;
using BL.DTOs;
using BL.DTOs.BookGenre;
using BL.DTOs.Genre;
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
        private readonly IQueryObject<BookFilterDto, BookGridDto> _bookQueryObject;
        private readonly IQueryObject<BookGenreFilterDto, BookGenreDto> _bookGenreQueryObject;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper, IQueryObject<BookFilterDto, BookGridDto> bookQueryObject, IQueryObject<BookGenreFilterDto, BookGenreDto> bookGenreQueryObject) : base(unitOfWork, mapper, unitOfWork.BookRepository)
        {
            _bookQueryObject = bookQueryObject;
            _bookGenreQueryObject = bookGenreQueryObject;
        }

        public IEnumerable<BookGridDto> AddGenresToBooks(IEnumerable<BookGridDto> books)
        {
            foreach (var book in books)
            {
                book.BookGenres = string.Join("/", _bookGenreQueryObject.ExecuteQuery(new BookGenreFilterDto() { BookId = book.Id}).Items.Select(x => x.Genre.Name));
            }

            return books;
        }

        public BookDetailDto GetBookDetailByID(int bookID)
        {
            var book = _mapper.Map<BookDetailDto>(Find(bookID));
            book.BookGenres = string.Join("/", _bookGenreQueryObject.ExecuteQuery(new BookGenreFilterDto() { BookId = bookID }).Items.Select(x => x.Genre.Name));
            return book;
        }

        public IEnumerable<BookGridDto> GetBooksbyFilter(BookFilterDto filter)
        {
            var books = _bookQueryObject.ExecuteQuery(filter).Items;
            return AddGenresToBooks(books);
        }

        public IEnumerable<GenreDto> GetAllGenres()
        {
            return _mapper.Map<IEnumerable<GenreDto>>(_unitOfWork.GenreRepository.GetAll());
        }
    }
}
