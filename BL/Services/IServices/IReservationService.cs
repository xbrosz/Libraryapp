using BL.DTOs.Reservation;
using BL.Services.GenericService;
using DAL.Entities;

namespace BL.Services.IServices
{
    public interface IReservationService : IGenericService<Reservation, ReservationsDto, CreateReservationDto, CreateReservationDto>
    {
        IEnumerable<ReservationsDto> GetReservationsByUserId(int userId);
        IEnumerable<ReservationsDto> GetReservationsByBookId(int bookId);
        IEnumerable<ReservationsDto> GetReservationsInDateRangeByBookAndBranch(
            int bookId, int branchId, DateTime fromDate, DateTime toDate);
    }
}
