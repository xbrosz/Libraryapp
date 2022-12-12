using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Rating : BaseEntity
    {
        [Range(0, 5)]
        public int RatingNumber { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
        public int UserId { get; set; }
        [MaxLength(255)]
        public string Comment { get; set; }
    }
}
