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

        public IEnumerable<ReservationsDto> getReservationsByUserId(int userId)
        {
            queryObject = new ReservationQueryObject(mapper, dbContext);

            return queryObject.ExecuteQuery(new ReservationFilterDto() { UserId = userId }).Items;
        }

    }
}
