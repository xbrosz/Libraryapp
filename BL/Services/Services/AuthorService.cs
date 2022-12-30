using AutoMapper;
using BL.DTOs.Author;
using BL.QueryObjects.IQueryObject;
using BL.Services.GenericService;
using BL.Services.IServices;
using DAL.Entities;
using Infrastructure.UnitOfWork;

namespace BL.Services.Services
{
    public class AuthorService : GenericService<Author, AuthorGridDto, AuthorUpdateDto, AuthorInsertDto>, IAuthorService
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

        public IEnumerable<AuthorGridDto> GetSortedAuthors()
        {
            return _authorQueryObject.ExecuteQuery(new AuthorFilterDto() { SortAscending = false, SortCriteria = nameof(Author.FirstName) }).Items;
        
        }

        public AuthorDetailDto? GetAuthorDetailById(int id)
        {
            return _mapper.Map<AuthorDetailDto>(_unitOfWork.AuthorRepository.GetByID(id));
        }
    }
}
