using BL.DTOs;
using BL.DTOs.Genre;
using DAL.Entities;

namespace BL.Services.IServices
{
    public interface IBookService
    {
        public BookDetailDto GetBookDetailByID(int bookID);
        IEnumerable<BookGridDto> GetBooksbyFilter(BookFilterDto filter);
        IEnumerable<BookGridDto> AddGenresToBooks(IEnumerable<BookGridDto> books);
        IEnumerable<GenreDto> GetAllGenres();
    }
}
