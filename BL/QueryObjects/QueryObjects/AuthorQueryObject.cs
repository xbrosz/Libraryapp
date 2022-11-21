using AutoMapper;
using BL.DTOs;
using BL.DTOs.Author;
using BL.QueryObjects.IQueryObject;
using DAL.Entities;
using Infrastructure.Query;

namespace BL.QueryObjects.QueryObjects
{
    public class AuthorQueryObject : IQueryObject<AuthorFilterDto, AuthorDto>
    {
        private IMapper _mapper;
        private IAbstractQuery<Author> _query;
        public AuthorQueryObject(IMapper mapper, IAbstractQuery<Author> query)
        {
            _mapper = mapper;
            _query = query;
        }

        public QueryResultDto<AuthorDto> ExecuteQuery(AuthorFilterDto filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.FirstName))
            {
                _query.Where<string>(a => a.ToLower().Contains(filter.FirstName.ToLower()), nameof(Author.FirstName));
            }

            if (!string.IsNullOrWhiteSpace(filter.MiddleName))
            {
                _query.Where<string>(a => a.ToLower().Contains(filter.MiddleName.ToLower()), nameof(Author.MiddleName));
            }

            if (!string.IsNullOrWhiteSpace(filter.LastName))
            {
                _query.Where<string>(a => a.ToLower().Contains(filter.LastName.ToLower()), nameof(Author.LastName));
            }

            if (!string.IsNullOrWhiteSpace(filter.SortCriteria))
            {
                _query.OrderBy<string>(filter.SortCriteria, filter.SortAscending);
            }

            if (filter.RequestedPageNumber.HasValue)
            {
                _query.Page(filter.RequestedPageNumber.Value, filter.PageSize);
            }

            return _mapper.Map<QueryResultDto<AuthorDto>>(_query.Execute());
        }
    }
}
