using AutoMapper;
using BL.DTOs;
using BL.QueryObjects.IQueryObject;
using BL.Services.GenericService;
using BL.Services.IServices;
using DAL.Entities;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace BL.Services.Services
{
    public class RatingService : GenericService<Rating, RatingDto, RatingDto, RatingDto>, IRatingService
    {


        private IQueryObject<RatingFilterDto, RatingDto> _ratingQueryObject;
        public RatingService(IUnitOfWork unitOfWork, IMapper mapper, IRepository<Rating> repository, IQueryObject<RatingFilterDto, RatingDto> ratingQueryObject) : base(unitOfWork, mapper, repository)
        {
            _ratingQueryObject = ratingQueryObject;
        }

        public double GetBookAverageRating(int bookId)
        {
            return _ratingQueryObject.ExecuteQuery(new RatingFilterDto
            {
                BookId = bookId,
            }).Items.Select(r => r.RatingNumber).Average();
        }

        public IEnumerable<RatingDto> GetRatingsByBook(int bookId)
        {
            return _ratingQueryObject.ExecuteQuery(new RatingFilterDto
            {
                BookId = bookId,
            }).Items;
        }
    }
}
