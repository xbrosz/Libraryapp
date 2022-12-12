using System.ComponentModel.DataAnnotations;

namespace FE.Models
{
    public class RatingEditViewModel
    {
        public int Id { get; set; }
        public string BookTitle { get; set; }
        public int BookId { get; set; }
        [Required(ErrorMessage = "Required")]
        [Range(0, 5)]
        public int RatingNumber { get; set; }
        [StringLength(150, ErrorMessage = "Max length is 150.")]
        public string Comment { get; set; }
    }
}
