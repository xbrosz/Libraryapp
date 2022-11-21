using AutoMapper;
using BL.DTOs;
using BL.QueryObjects.IQueryObject;
using DAL.Entities;
using Infrastructure.Query;

namespace BL.QueryObjects.QueryObjects
{
    public class RatingQueryObject : IQueryObject<RatingFilterDto, RatingDto>
    {
        private IMapper mapper;

        private IAbstractQuery<Rating> myQuery;

        public RatingQueryObject(IMapper mapper, IAbstractQuery<Rating> context)
        {
            this.mapper = mapper;
            myQuery = context;
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
