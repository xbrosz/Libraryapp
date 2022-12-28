

using BL.Facades.Facades;

namespace FE.Models
{
    public class BookDetailModel
    {
        public int Id { get; set; }
        public string BookTitle { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string AuthorName { get; set; }
        public string Genres { get; set; }
        public double RatingNumber { get; set; }
    }
}
