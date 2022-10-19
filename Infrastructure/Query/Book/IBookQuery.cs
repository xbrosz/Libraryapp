using Infrastructure.DTOs.Book;

namespace Infrastructure.Query.Book
{
    public interface IBookQuery
    {
        Task<List<BookDTO>> GetAll();

        Task<BookDTO> GetByID(int ID);

    }
}
