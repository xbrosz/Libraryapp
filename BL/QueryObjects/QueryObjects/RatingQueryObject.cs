using AutoMapper;
using BL.DTOs;
using DAL.Data;
using DAL.Entities;
using Infrastructure.EFCore;
using Infrastructure.Query;

namespace BL.QueryObjects.QueryObjects
{
    public class RatingQueryObject
    {
        private IMapper mapper;

        private IAbstractQuery<Rating> myQuery;

        public RatingQueryObject(IMapper mapper, LibraryappDbContext context)
        {
            this.mapper = mapper;
            myQuery = new GenericQuery<Rating>(context);
        }

        public QueryResultDto<RatingDto> ExecuteQuery(RatingFilterDto filter)
        {
            var query = myQuery.Where<int>(a => a == filter.BookId, "BookId");
            if (filter.RequestedPageNumber.HasValue)
            {
                query = query.Page(filter.RequestedPageNumber.Value, filter.PageSize);
            }

            return mapper.Map<QueryResultDto<RatingDto>>(query.Execute());
        }
    }
}
