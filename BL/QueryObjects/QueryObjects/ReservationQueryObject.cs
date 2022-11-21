using AutoMapper;
using BL.DTOs;
using BL.DTOs.Reservation;
using BL.QueryObjects.IQueryObject;
using DAL.Data;
using DAL.Entities;
using Infrastructure.EFCore;
using Infrastructure.EFCore.Query;
using Infrastructure.Query;

namespace BL.QueryObjects.QueryObjects
{
    public class ReservationQueryObject : IQueryObject<ReservationFilterDto, ReservationsDto>
    {
        private IMapper _mapper;

        private IReservationQuery _myQuery;

        public ReservationQueryObject(IMapper mapper, IReservationQuery query)
        {
            _mapper = mapper;
            _myQuery = query;
        }

        public QueryResultDto<ReservationsDto> ExecuteQuery(ReservationFilterDto filter)
        {
            var query = _myQuery;

            if (filter.UserId.HasValue)
            {
                query = _myQuery.Where<int>(a => a == filter.UserId, "UserId");
            }

            if (filter.BookId.HasValue)
            {
                query = _myQuery.Where<BookPrint>(a => a.BookId == filter.BookId, "BookPrint");
            }

            if (filter.BranchId.HasValue)
            {
                query = _myQuery.Where<BookPrint>(a => a.BranchId == filter.BranchId, "BookPrint");
            }

            if (filter.FromDate.HasValue)
            {
                query = _myQuery.FromFilter(filter.FromDate.Value);
            }

            if (filter.ToDate.HasValue)
            {
                query = _myQuery.ToFilter(filter.ToDate.Value);
            }

            if (filter.RequestedPageNumber.HasValue)
            {
                query = query.Page(filter.RequestedPageNumber.Value, filter.PageSize);
            }
            var res = query.Execute();
            return _mapper.Map<QueryResultDto<ReservationsDto>>(res);
        }
    }
}
