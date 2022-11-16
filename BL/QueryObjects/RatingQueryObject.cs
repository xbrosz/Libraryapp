using AutoMapper;
using BL.DTOs;
using DAL.Data;
using DAL.Entities;
using Infrastructure.EFCore.Query;
using Infrastructure.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.QueryObjects
{
    public class RatingQueryObject
    {
        private IMapper mapper;

        private IGenericQuery<Rating> myQuery;

        public RatingQueryObject(IMapper mapper, LibraryappDbContext context)
        {
            this.mapper = mapper;
            myQuery = new EFGenericQuery<Rating>(context);
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
