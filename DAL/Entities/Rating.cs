using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        [Range(0, 5)]
        public int RatingNumber { get; set; }
        public int BookId { get; set; }
        [MaxLength(255)]
        public string Comment { get; set; }
    }
}
