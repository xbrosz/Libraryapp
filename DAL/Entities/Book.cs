using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Book : BaseEntity
    {
        [MaxLength(150)]
        public string Title { get; set; }
        public DateTime Release { get; set; }
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }

        public virtual List<BookPrint> BookPrints { get; set; }
        public virtual List<Rating> Ratings { get; set; }
        public virtual List<Genre> Genres { get; set; }

        public static object Rating()
        {
            throw new NotImplementedException();
        }
    }
}
