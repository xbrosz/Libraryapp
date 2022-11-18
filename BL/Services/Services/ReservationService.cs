using AutoMapper;
using BL.DTOs.Reservation;
using BL.QueryObjects.QueryObjects;
using BL.Services.GenericService;
using BL.Services.IServices;
using DAL.Data;
using DAL.Entities;
using Infrastructure.UnitOfWork;

namespace BL.Services.Services
{
    public class ReservationService : GenericService<Reservation, ReservationsDto, ReservationsDto, ReservationsDto>, IReservationService
    {
        private ReservationQueryObject queryObject;
        LibraryappDbContext dbContex;
        public ReservationService(IUnitOfWork unitOfWork, LibraryappDbContext dbContext, IMapper mapper) : base(unitOfWork, mapper, unitOfWork.ReservationRepository) 
        {
            this.dbContex = dbContex;
        }

        public IEnumerable<ReservationsDto> GetReservationsByUserId(int userId)
        {
            queryObject = new ReservationQueryObject(_mapper, dbContex);

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
