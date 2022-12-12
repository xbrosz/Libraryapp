using BL.DTOs;
using BL.DTOs.Rating;

namespace FE.Models
{
    public class RatingIndexViewModel
    {
        public IEnumerable<RatingDto> ratings { get; set; }
        public IEnumerable<RatingAwaitingDto> awaitingRatings { get; set; }
    }
}
