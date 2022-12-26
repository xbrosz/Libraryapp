using BL.DTOs;
using DAL.Entities;

namespace BL.Services.IServices
{
    public interface IBookService
    {
        public IEnumerable<BookGridDto> GetBooksbyAuthorID(int authorID);
        public BookDetailDto GetBookDetailByID(int bookID);

        IEnumerable<BookGridDto> GetBooksbyFilter(BookFilterDto filter);

        IEnumerable<Book> GetAll();
    }
}
