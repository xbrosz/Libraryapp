using AutoMapper;
using BL.DTOs;
using BL.QueryObjects.IQueryObject;
using DAL.Entities;
using Infrastructure.Query;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BL.QueryObjects.QueryObjects
{
    public class BookQueryObject : IQueryObject<BookFilterDto, BookGridDto>
    {
        private IMapper mapper;

        private IAbstractQuery<Book> myQuery;

        public BookQueryObject(IMapper mapper, IAbstractQuery<Book> query)
        {
            this.mapper = mapper;
            myQuery = query;
        }

        public QueryResultDto<BookGridDto> ExecuteQuery(BookFilterDto filter)
        {
            if (filter.AuthorID.HasValue)
            {
                myQuery.Where<int>(a => a == filter.AuthorID, nameof(Book.AuthorId));
            }
            
            if (string.IsNullOrWhiteSpace(filter.Title))
            {
                myQuery.Where<string>(a => a == filter.Title, nameof(Book.Title));
            }

            if (string.IsNullOrWhiteSpace(filter.AuthorName))
            {
                //myQuery.Where<string>(a => a == filter.AuthorName, nameof(Book.Author.));
            }

            if (filter.Ratings!= null && filter.Ratings.Any())
            {
                filter.Ratings.Sort();

                //myQuery.Where<int>(a => a == filter.AuthorID, nameof(Book.AuthorId));
            }

            if (!string.IsNullOrWhiteSpace(filter.SortCriteria))
            {
                myQuery.OrderBy<string>(filter.SortCriteria, filter.SortAscending);
            }

            if (filter.RequestedPageNumber.HasValue)
            {
                myQuery.Page(filter.RequestedPageNumber.Value, filter.PageSize);
            }

            return mapper.Map<QueryResultDto<BookGridDto>>(myQuery.Execute());
        }
    }
}
