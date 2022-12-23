using AutoMapper;
using BL.DTOs.Author;
using BL.QueryObjects.IQueryObject;
using BL.Services.GenericService;
using BL.Services.IServices;
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

        public IEnumerable<AuthorDto> GetAuthorsByName(string name)
        {
            //if (!firstName.All(char.IsLetter) || !middleName.All(char.IsLetter) || !lastName.All(char.IsLetter))
            //{
            //    throw new Exception("Names should contain just letters.");
            //}

            var authors = _authorQueryObject.ExecuteQuery(new AuthorFilterDto() { FirstName = name }).Items;
            authors.Union(_authorQueryObject.ExecuteQuery(new AuthorFilterDto() { MiddleName = name }).Items);
            return authors.Union(_authorQueryObject.ExecuteQuery(new AuthorFilterDto() { LastName = name }).Items);
        }
    }
}
