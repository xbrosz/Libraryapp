using BL.DTOs.Author;

namespace BL.Services.IServices
{
    public interface IAuthorService
    {
        AuthorGridDto Find(int id);

        void Delete(int id);

        void Update(AuthorUpdateDto dtoToUpdate);

        void Insert(AuthorInsertDto dtoToInsert);

        IEnumerable<AuthorGridDto> GetAuthorsByName(AuthorFilterDto filter);

        IEnumerable<AuthorGridDto> GetSortedAuthors();
        IEnumerable<AuthorGridDto> GetAll();

        AuthorDetailDto? GetAuthorDetailById(int id);
    }
}
