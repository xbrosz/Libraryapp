﻿using BL.DTOs.Reservation;

namespace BL.Services
{
    public interface IReservationService
    {
        IEnumerable<ReservationsDto> GetReservationsByUserId(int userId);
        IEnumerable<ReservationsDto> GetReservationsByBookId(int bookId);

    }
}
