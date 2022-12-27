using AutoMapper;
using BL.DTOs.Author;
using BL.QueryObjects.IQueryObject;
using BL.Services.GenericService;
using BL.Services.IServices;
using DAL.Entities;
using Infrastructure.UnitOfWork;

namespace BL.Services.Services
{
    public class AuthorService : GenericService<Author, AuthorGridDto, AuthorInsertDto, AuthorInsertDto>, IAuthorService
    {
        private IQueryObject<AuthorFilterDto, AuthorGridDto> _authorQueryObject;

        public AuthorService(IUnitOfWork unitOfWork, IMapper mapper, IQueryObject<AuthorFilterDto, AuthorGridDto> authorQueryObject) : base(unitOfWork, mapper, unitOfWork.AuthorRepository)
        {
            _authorQueryObject = authorQueryObject;
        }

        public IEnumerable<AuthorGridDto> GetAuthorsByName(AuthorFilterDto filter)
        {
            return _authorQueryObject.ExecuteQuery(filter).Items;
        }

        public IEnumerable<AuthorGridDto> GetSortedAuthors(int page, int pageSize)
        {
            return _authorQueryObject.ExecuteQuery(new AuthorFilterDto() { PageSize = pageSize, RequestedPageNumber = page, SortAscending = false, SortCriteria = nameof(Author.FirstName) }).Items;
        
        }
    }
}
