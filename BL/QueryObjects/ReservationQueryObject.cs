using AutoMapper;
using BL.DTOs;
using BL.DTOs.Reservation;
using DAL.Data;
using DAL.Entities;
using Infrastructure.EFCore.Query;
using Infrastructure.Query;

namespace BL.QueryObjects
{
    public class ReservationQueryObject
    {
        private IMapper mapper;

        private IReservationQuery myQuery;

        public ReservationQueryObject(IMapper mapper, LibraryappDbContext dbx)
        {
            this.mapper = mapper;
            myQuery = new EFReservationQuery(dbx);
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

            return mapper.Map<QueryResultDto<ReservationsDto>>(query.Execute());
        }
    }
}
