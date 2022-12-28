using BL.DTOs;
using BL.DTOs.Author;
using BL.DTOs.Genre;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Facades.IFacades
{
    public interface IBookFacade
    {
        IEnumerable<BookGridDto> GetBooksByTitle(string substring, int page, int pageSize);

        IEnumerable<BookGridDto> GetAllBooksSortedByRating(int page, int pageSize);

        IEnumerable<BookGridDto> GetBooksByAuthorName(string name, int page, int pageSize);

        BookDetailDto GetBookDetailByID(int bookID);
        IEnumerable<AuthorGridDto> GetAuthorsByName(string? searchString, int page, int pageSize);

        IEnumerable<BookGridDto> GetBooksForAuthorId(int? authorId, int page, int pageSize);

        IEnumerable<GenreDto> GetAllGenres();

        IEnumerable<BookGridDto> GetAllBooks(int page, int pageSize);
    }
}
