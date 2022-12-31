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

        public double? GetBookAverageRating(int bookId)
        {
            var ratings = _ratingQueryObject.ExecuteQuery(new RatingFilterDto
            {
                BookId = bookId,
            });

            if (ratings.TotalItemsCount > 0)
            {
                return ratings.Items.Select(r => r.RatingNumber).Average();
            }

            return null;
        }

        public IEnumerable<RatingDto> GetRatingsByBook(int bookId)
        {
            return _ratingQueryObject.ExecuteQuery(new RatingFilterDto
            {
                BookId = bookId,
            }).Items;
        }

        public IEnumerable<RatingDto> GetRatingsByUser(int userId)
        {
            return _ratingQueryObject.ExecuteQuery(new RatingFilterDto
            {
                UserId = userId,
            }).Items;
        }
    }
}
