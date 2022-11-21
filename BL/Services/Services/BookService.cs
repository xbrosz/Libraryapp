using AutoMapper;
using BL.DTOs;
using BL.QueryObjects;
using BL.QueryObjects.IQueryObject;
using BL.QueryObjects.QueryObjects;
using BL.Services.GenericService;
using BL.Services.IServices;
using DAL.Data;
using DAL.Entities;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace BL.Services.Services
{
    public class BookService : GenericService<Book, BookDetailDto, BookPrintDto, BookDetailDto>, IBookService
    {
        IQueryObject<BookFilterDto, BookGridDto> _bookQueryObject;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper, IQueryObject<BookFilterDto,BookGridDto> bookQueryObject) : base(unitOfWork, mapper, unitOfWork.BookRepository)
        {

            _bookQueryObject = bookQueryObject;
        }

        public IEnumerable<BookGridDto> GetBooksbyAuthorID(int authorID)
        {
            

            return _bookQueryObject.ExecuteQuery(new BookFilterDto
            {
                AuthorID = authorID,

            }).Items;
        }

        public BookDetailDto GetBookDetailByID(int bookID)
        {
            return _mapper.Map<BookDetailDto>(Find(bookID));
        }

    }
}
