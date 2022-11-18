using AutoMapper;
using BL.DTOs.Reservation;
using BL.QueryObjects;
using BL.Services;
using BL.Services.GenericService;
using BL.Services.IServices;
using DAL.Data;
using DAL.Entities;
using Infrastructure.Repository;

namespace BL.Services.Services
{
    public class ReservationService : GenericService<Reservation, ReservationsDto, ReservationsDto, ReservationsDto>, IReservationService
    {
        private LibraryappDbContext dbContext;
        private ReservationQueryObject queryObject;

        public ReservationService(IRepository<Reservation> repository, LibraryappDbContext ctx) : base(repository)
        {
            dbContext = ctx;
        }

        public IEnumerable<ReservationsDto> GetReservationsByUserId(int userId)
        {
            queryObject = new ReservationQueryObject(mapper, dbContext);

            return queryObject.ExecuteQuery(new ReservationFilterDto { UserId = userId }).Items;
        }

        public IEnumerable<ReservationsDto> GetReservationsByBookId(int bookId)
        {
            queryObject = new ReservationQueryObject(mapper, dbContext);

            return queryObject.ExecuteQuery(new ReservationFilterDto { BookId = bookId }).Items;
        }

        public IEnumerable<ReservationsDto> GetReservationsInDateRangeByBookAndBranch(
            int bookId, int branchId, DateTime fromDate, DateTime toDate)
        {
            queryObject = new ReservationQueryObject(mapper, dbContext);

            return queryObject.ExecuteQuery(new ReservationFilterDto { BookId = bookId, BranchId = branchId, FromDate = fromDate, ToDate = toDate }).Items;
        }
    }
}
