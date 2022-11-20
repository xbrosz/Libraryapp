using AutoMapper;
using BL.DTOs.Author;
using BL.QueryObjects.IQueryObject;
using BL.QueryObjects.QueryObjects;
using BL.Services.GenericService;
using BL.Services.IServices;
using DAL.Data;
using DAL.Entities;
using Infrastructure.UnitOfWork;

namespace BL.Services.Services
{
    public class AuthorService : GenericService<Author, AuthorDto, AuthorDto, AuthorDto>, IAuthorService
    {
        private IQueryObject<AuthorFilterDto, AuthorDto> _authorQueryObject;

        public AuthorService(IUnitOfWork unitOfWork, IMapper mapper, IQueryObject<AuthorFilterDto, AuthorDto> authorQueryObject) : base(unitOfWork, mapper, unitOfWork.AuthorRepository) 
        {
            _authorQueryObject = authorQueryObject;
        }

        public IEnumerable<AuthorDto> GetAuthorsByName(string firstName, string middleName, string lastName)
        {
            return _authorQueryObject.ExecuteQuery(new AuthorFilterDto() { FirstName = firstName, MiddleName = middleName, LastName = lastName, SortCriteria = nameof(Author.FirstName), SortAscending = true }).Items;
        }
    }
}
