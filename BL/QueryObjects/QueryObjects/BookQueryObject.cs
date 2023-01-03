using AutoMapper;
using BL.DTOs;
using BL.QueryObjects.IQueryObject;
using DAL.Entities;
using Infrastructure.Query;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BL.QueryObjects.QueryObjects
{
    public class BookQueryObject : IQueryObject<BookFilterDto, BookGridDto>
    {
        private IMapper _mapper;

        private IAbstractQuery<Book> _query;

        public BookQueryObject(IMapper mapper, IAbstractQuery<Book> query)
        {
            _mapper = mapper;
            _query = query;
        }

        public QueryResultDto<BookGridDto> ExecuteQuery(BookFilterDto filter)
        {
            if (filter.AuthorID.HasValue)
            {
                _query.Where<int>(a => a == filter.AuthorID, nameof(Book.AuthorId)); 
            }
            
            if (!string.IsNullOrWhiteSpace(filter.Title))
            {
                _query.Where<string>(a => a.ToLower().Contains(filter.Title.ToLower()), nameof(Book.Title));
            }

            if (filter.LowestRating.HasValue)
            {
                _query.Where<double>(a => a >= (double) filter.LowestRating, nameof(Book.SortRatingNumber));
            }

            if (!string.IsNullOrWhiteSpace(filter.SortCriteria))
            {
                _query.OrderBy<double>(filter.SortCriteria, filter.SortAscending);
            }

            if (filter.RequestedPageNumber.HasValue)
            {
                _query.Page(filter.RequestedPageNumber.Value, filter.PageSize);
            }

            return _mapper.Map<QueryResultDto<BookGridDto>>(_query.Execute());
        }
    }
}
