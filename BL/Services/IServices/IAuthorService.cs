using BL.DTOs.Author;

namespace BL.Services.IServices
{
    public interface IAuthorService
    {
        public AuthorDto Find(int id);

        public void Delete(int id);

        public void Update(AuthorDto dtoToUpdate);

        public void Insert(AuthorDto dtoToInsert);

        public IEnumerable<AuthorDto> GetAuthorsByName(string firstName, string middleName, string lastName);
    }
}
