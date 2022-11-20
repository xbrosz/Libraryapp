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

        public IEnumerable<AuthorDto> GetAuthorsByName(AuthorFilterDto filter)
        {
            return _authorQueryObject.ExecuteQuery(filter).Items;
        }
    }
}
