using BL.DTOs;
using BL.DTOs.Author;
using BL.DTOs.Genre;

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

        void DeleteGenre(int genreId);

        GenreDto? GetGenreForId(int genreId);

        void UpdateGenre(GenreDto genre);

        void InsertGenre(GenreDto genre);

        AuthorDetailDto? GetAuthorDetailById(int authorId);

        void UpdateAuthor(AuthorUpdateDto author);

        void InsertAuthor(AuthorInsertDto author);

        void DeleteAuthor(int authorId);
    }
}
