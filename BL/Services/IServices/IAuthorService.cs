using BL.DTOs.Author;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.IServices
{
    public interface IAuthorService
    {
        public AuthorDto Find(int id);

        public Task DeleteAsync(int id);

        public Task UpdateAsync(AuthorDto dtoToUpdate);

        public Task InsertAsync(AuthorDto dtoToInsert);

        public IEnumerable<AuthorDto> GetAuthorsByName(AuthorFilterDto filter);
    }
}
