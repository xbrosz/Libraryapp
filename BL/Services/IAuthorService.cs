using BL.DTOs.Author;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IAuthorService
    {
        public AuthorDto GetAuthorByName(AuthorFilterDto filter);
    }
}
