using BL.DTOs;

namespace BL.Services.IServices
{
    public interface IRatingService
    {
        public double GetBookAverageRating(int bookId);

        public IEnumerable<RatingDto> GetRatingsByBook(int bookId);



    }
}
