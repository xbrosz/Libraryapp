using AutoMapper;
using BL.DTOs;
using BL.QueryObjects.IQueryObject;
using DAL.Entities;
using Infrastructure.Query;

namespace BL.QueryObjects.QueryObjects
{
    public class RatingQueryObject : IQueryObject<RatingFilterDto, RatingDto>
    {
        private IMapper _mapper;

        private IAbstractQuery<Rating> _myQuery;

        public RatingQueryObject(IMapper mapper, IAbstractQuery<Rating> query)
        {
            _mapper = mapper;
            _myQuery = query;
        }

        public QueryResultDto<RatingDto> ExecuteQuery(RatingFilterDto filter)
        {
            var query = _myQuery;
            
            if (filter.RequestedPageNumber.HasValue)
            {
                query = query.Page(filter.RequestedPageNumber.Value, filter.PageSize);
            }

            if (filter.BookId.HasValue)
            {
                query = query.Where<int>(a => a == filter.BookId, "BookId");
            }

            if (filter.UserId.HasValue)
            {
                query = query.Where<int>(a => a == filter.UserId, "UserId");
            }

            return _mapper.Map<QueryResultDto<RatingDto>>(query.Execute());
        }
    }
}
