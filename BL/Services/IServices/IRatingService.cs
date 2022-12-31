using BL.DTOs;
using BL.Services.GenericService;
using DAL.Entities;

namespace BL.Services.IServices
{
    public interface IRatingService : IGenericService<Rating, RatingDto, RatingDto, RatingDto>
    {
        double? GetBookAverageRating(int bookId);

        IEnumerable<RatingDto> GetRatingsByBook(int bookId);

        IEnumerable<RatingDto> GetRatingsByUser(int userId);
    }
}
