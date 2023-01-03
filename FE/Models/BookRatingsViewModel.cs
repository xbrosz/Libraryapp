using BL.DTOs;

namespace FE.Models
{
    public class BookRatingsViewModel
    {
        public string Title { get; set; }
        public IEnumerable<RatingDto> Ratings { get; set; }
    }
}
