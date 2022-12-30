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
        IEnumerable<BookGridDto> GetBooksByTitle(string substring);

        BookDetailDto GetBookDetailByID(int bookID);
        IEnumerable<AuthorGridDto> GetAuthorsByName(string? searchString);

        IEnumerable<BookGridDto> GetBooksForAuthorId(int? authorId);

        IEnumerable<GenreDto> GetAllGenres();

        IEnumerable<BookGridDto> GetAllBooks();

        IEnumerable<BookGridDto> GetBooksBySearchFilter(string? searchString, int? rating, string? genre);

        void DeleteBook(int id);

        void UpdateBook(BookUpdateDto dto);

        IEnumerable<AuthorGridDto> GetAllAuthors();
        void DeleteBookGenreForBookId(int bookId);
        void InsertBookGenre(string genre, int bookid);
    }
}
