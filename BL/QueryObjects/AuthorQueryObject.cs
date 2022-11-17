using AutoMapper;
using BL.DTOs;
using BL.DTOs.Author;
using DAL.Data;
using DAL.Entities;
using Infrastructure.EFCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.QueryObjects
{
    public class AuthorQueryObject
    {
        private IMapper _mapper;
        private EFGenericQuery<Author> _query;
        public AuthorQueryObject(IMapper mapper, LibraryappDbContext dbContext)
        {
            _mapper = mapper;
            _query = new EFGenericQuery<Author>(dbContext);
        }

        public QueryResultDto<AuthorDto> ExecuteQuery(AuthorFilterDto filter)
        {
            var query = _query.Where<string>(a => a.ToLower() == filter.FirstName.ToLower(), nameof(Author.FirstName))
                .Where<string>(a => a.ToLower() == filter.MiddleName.ToLower(), nameof(Author.MiddleName))
                .Where<string>(a => a.ToLower() == filter.LastName.ToLower(), nameof(Author.LastName));
            return _mapper.Map<QueryResultDto<AuthorDto>>(query.Execute());
        }
    }
}
