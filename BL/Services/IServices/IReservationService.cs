using BL.DTOs.Reservation;

namespace BL.Services.IServices
{
    public interface IReservationService
    {
        IEnumerable<ReservationsDto> getReservationsByUserId(int userId);
    }
}
