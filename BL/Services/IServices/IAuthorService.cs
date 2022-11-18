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

        public void Delete(int id);

        public void Update(AuthorDto dtoToUpdate);

        public void Insert(AuthorDto dtoToInsert);

        public AuthorDto GetAuthorByName(AuthorFilterDto filter);
    }
}
