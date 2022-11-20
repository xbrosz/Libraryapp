using AutoMapper;
using BL.DTOs;
using BL.DTOs.Author;
using BL.QueryObjects.IQueryObject;
using DAL.Data;
using DAL.Entities;
using Infrastructure.EFCore;
using Infrastructure.Query;

namespace BL.QueryObjects.QueryObjects
{
    public class BookQueryObject : IQueryObject<BookFilterDto, BookGridDto>
    {
        private IMapper mapper;

        private IAbstractQuery<Book> myQuery;

        public BookQueryObject(IMapper mapper, LibraryappDbContext context)
        {
            this.mapper = mapper;
            myQuery = new GenericQuery<Book>(context);
        }

        public QueryResultDto<BookGridDto> ExecuteQuery(BookFilterDto filter)
        {
            var query = myQuery.Where<int>(a => a == filter.AuthorID, "AuthorId");
            if (filter.RequestedPageNumber.HasValue)
            {
                query = query.Page(filter.RequestedPageNumber.Value, filter.PageSize);
            }

            return mapper.Map<QueryResultDto<BookGridDto>>(query.Execute());
        }
    }
}
