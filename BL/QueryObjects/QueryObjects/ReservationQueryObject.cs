using AutoMapper;
using BL.DTOs;
using BL.DTOs.Reservation;
using BL.QueryObjects.IQueryObject;
using DAL.Entities;
using Infrastructure.Query;

namespace BL.QueryObjects.QueryObjects
{
    public class ReservationQueryObject : IQueryObject<ReservationFilterDto, ReservationsDto>
    {
        private IMapper mapper;

        private IReservationQuery myQuery;

        public ReservationQueryObject(IMapper mapper, IReservationQuery query)
        {
            this.mapper = mapper;
            myQuery = query;
        }

        public QueryResultDto<ReservationsDto> ExecuteQuery(ReservationFilterDto filter)
        {
            var query = myQuery;

            if (filter.UserId.HasValue)
            {
                query = myQuery.Where<int>(a => a == filter.UserId, "UserId");
            }

            if (filter.BookId.HasValue)
            {
                query = myQuery.Where<BookPrint>(a => a.BookId == filter.BookId, "BookPrint");
            }

            if (filter.BranchId.HasValue)
            {
                query = myQuery.Where<BookPrint>(a => a.BranchId == filter.BranchId, "BookPrint");
            }

            if (filter.FromDate.HasValue)
            {
                query = myQuery.FromFilter(filter.FromDate.Value);
            }

            if (filter.ToDate.HasValue)
            {
                query = myQuery.ToFilter(filter.ToDate.Value);
            }

            if (filter.RequestedPageNumber.HasValue)
            {
                query = query.Page(filter.RequestedPageNumber.Value, filter.PageSize);
            }
            var res = query.Execute();
            return mapper.Map<QueryResultDto<ReservationsDto>>(res);
        }
    }
}
