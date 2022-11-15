using AutoMapper;
using BL.DTOs;
using DAL.Data;
using DAL.Entities;
using Infrastructure.EFCore.Query;
using Infrastructure.Query;

namespace BL.QueryObjects
{
    public class ReservationQueryObject
    {
        private IMapper mapper;

        private IGenericQuery<Reservation> myQuery;

        public ReservationQueryObject(IMapper mapper, LibraryappDbContext dbx)
        {
            this.mapper = mapper;
            myQuery = new EFGenericQuery<Reservation>(dbx);
        }

        public QueryResultDto<ReservationsDto> ExecuteQuery(ReservationFilterDto filter)
        {
            var query = myQuery.Where<int>(a => a == filter.UserId, "UserId");

            if (filter.RequestedPageNumber.HasValue)
            {
                query = query.Page(filter.RequestedPageNumber.Value, filter.PageSize);
            }

            return mapper.Map<QueryResultDto<ReservationsDto>>(query.Execute());
        }
    }
}
