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

        public IEnumerable<ReservationsDto> getReservationsByUserId(int userId)
        {
            queryObject = new ReservationQueryObject(_mapper, dbContex);

            return queryObject.ExecuteQuery(new ReservationFilterDto() { UserId = userId }).Items;
        }

    }
}
