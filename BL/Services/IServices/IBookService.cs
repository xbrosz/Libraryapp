using BL.DTOs;

namespace BL.Services.IServices
{
    public interface IBookService
    {
        public IEnumerable<BookGridDto> GetBooksbyAuthorID(int authorID);
        public BookDetailDto GetBookDetailByID(int bookID);


    }
}
