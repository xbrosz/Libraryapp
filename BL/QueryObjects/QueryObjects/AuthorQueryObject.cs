using AutoMapper;
using BL.DTOs;
using BL.DTOs.Author;
using BL.DTOs.User;
using BL.QueryObjects.IQueryObject;
using DAL.Data;
using DAL.Entities;
using Infrastructure.EFCore;
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
            var query = _query.Where<string>(a => a.ToLower() == filter.FirstName.ToLower(), nameof(Author.FirstName));
            //.Where<string>(a => a.ToLower() == filter.MiddleName.ToLower(), nameof(Author.MiddleName))
            //.Where<string>(a => a.ToLower() == filter.LastName.ToLower(), nameof(Author.LastName));

            return _mapper.Map<QueryResultDto<AuthorDto>>(query.Execute().First<Author>());
        }
    }
}
