using BL.DTOs;
using System.ComponentModel.DataAnnotations;

namespace FE.Models
{
    public class RatingInsertViewModel
    {
        [Required(ErrorMessage = "Required")]
        [Range(0, 5)]
        public int RatingNumber { get; set; }
        public string BookTitle { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        [StringLength(150, ErrorMessage = "Max length is 150.")]
        public string Comment { get; set; }

        public RatingDto ToDto() => new()
        {
            BookId = BookId,
            Comment = Comment,
            RatingNumber = RatingNumber,
            UserId = UserId
        };
    }
}
