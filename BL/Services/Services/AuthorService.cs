using BL.DTOs.Author;
using BL.QueryObjects;
using BL.Services.GenericService;
using BL.Services.IServices;
using DAL.Data;
using DAL.Entities;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Services
{
    public class AuthorService : GenericService<Author, AuthorDto, AuthorDto, AuthorDto>, IAuthorService
    {
        private LibraryappDbContext _dbContext;
        public AuthorService(IRepository<Author> repository, LibraryappDbContext dbContext) : base(repository)
        {
            _dbContext = dbContext;
        }

        public AuthorDto GetAuthorByName(AuthorFilterDto filter)
        {
            var queryObject = new AuthorQueryObject(mapper, _dbContext);
            return queryObject.ExecuteQuery(filter).Items.First();
        }
    }
}
