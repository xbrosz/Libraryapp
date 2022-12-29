using BL.DTOs.Author;

namespace BL.Services.IServices
{
    public interface IAuthorService
    {
        AuthorGridDto Find(int id);

        void Delete(int id);

        void Update(AuthorInsertDto dtoToUpdate);

        void Insert(AuthorInsertDto dtoToInsert);

        IEnumerable<AuthorGridDto> GetAuthorsByName(AuthorFilterDto filter);

        IEnumerable<AuthorGridDto> GetSortedAuthors();
    }
}
