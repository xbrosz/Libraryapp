using BL.DTOs;
using BL.Services.GenericService;
using DAL.Entities;

namespace BL.Services.IServices
{
    public interface IRatingService : IGenericService<Rating, RatingDto, RatingDto, RatingDto>
    {
        public double GetBookAverageRating(int bookId);

        public IEnumerable<RatingDto> GetRatingsByBook(int bookId);

        public IEnumerable<RatingDto> GetRatingsByUser(int userId);
    }
}
