using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Book : BaseEntity
    {
        [MaxLength(150)]
        public string Title { get; set; }
        public DateTime Release { get; set; }
        public int AuthorId { get; set; }

        public double? RatingNumber { get; set; }
        public double SortRatingNumber { get; set; } = 0;
        public virtual Author Author { get; set; }
    }
}
