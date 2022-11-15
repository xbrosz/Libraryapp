using BL.DTOs;

namespace BL.Services
{
    public interface IReservationService
    {
        IEnumerable<ReservationsDto> getReservationsByUserId(int userId);
    }
}
