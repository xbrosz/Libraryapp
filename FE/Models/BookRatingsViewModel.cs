using BL.DTOs;

namespace FE.Models
{
    public class BookRatingsViewModel
    {
        public string title { get; set; }
        public IEnumerable<RatingDto> ratings { get; set; }
    }
}
