using AutoMapper;
using BL.DTOs.Reservation;
using BL.QueryObjects;
using BL.Services;
using DAL.Data;
using DAL.Entities;
using Infrastructure.Repository;

namespace BL.Service
{
    public class ReservationService : GenericService<Reservation, ReservationsDto, ReservationsDto, ReservationsDto>, IReservationService
    {
        private IMapper mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));
        private IRepository<Reservation> repository;
        private LibraryappDbContext dbContext;
        private ReservationQueryObject queryObject;

        public ReservationService(IRepository<Reservation> repository, LibraryappDbContext ctx) : base(repository)
        {
            this.repository = repository;
            this.dbContext = ctx;
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
    }
}
