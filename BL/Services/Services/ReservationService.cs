using AutoMapper;
using BL.DTOs.Author;
using BL.DTOs.Reservation;
using BL.QueryObjects.IQueryObject;
using BL.QueryObjects.QueryObjects;
using BL.Services.GenericService;
using BL.Services.IServices;
using DAL.Data;
using DAL.Entities;
using Infrastructure.UnitOfWork;

namespace BL.Services.Services
{
    public class ReservationService : GenericService<Reservation, ReservationsDto, CreateReservationDto, CreateReservationDto>, IReservationService
    {
        private IQueryObject<ReservationFilterDto, ReservationsDto> queryObject;
        public ReservationService(IUnitOfWork unitOfWork, IMapper mapper, IQueryObject<ReservationFilterDto, ReservationsDto> reservationQueryObject)
            : base(unitOfWork, mapper, unitOfWork.ReservationRepository) 
        {
            this.queryObject = reservationQueryObject;
        }

        public IEnumerable<ReservationsDto> GetReservationsByUserId(int userId)
        {
            return queryObject.ExecuteQuery(new ReservationFilterDto { UserId = userId }).Items;
        }

        public IEnumerable<ReservationsDto> GetReservationsByBookId(int bookId)
        {
            return queryObject.ExecuteQuery(new ReservationFilterDto { BookId = bookId }).Items;
        }

        public IEnumerable<ReservationsDto> GetReservationsInDateRangeByBookAndBranch(
            int bookId, int branchId, DateTime fromDate, DateTime toDate)
        {
            return queryObject.ExecuteQuery(new ReservationFilterDto { BookId = bookId, BranchId = branchId, FromDate = fromDate, ToDate = toDate }).Items;
        }
    }
}
