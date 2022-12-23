using BL.DTOs.Author;

namespace BL.Services.IServices
{
    public interface IAuthorService
    {
        AuthorDto Find(int id);

        void Delete(int id);

        void Update(AuthorDto dtoToUpdate);

        void Insert(AuthorDto dtoToInsert);

        IEnumerable<AuthorDto> GetAuthorsByName(string name);
    }
}
