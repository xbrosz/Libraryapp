namespace FE.Models
{
    public class BookEditVIewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Release { get; set; }
        public int? AuthorId { get; set; }
        public string? AuthorName { get; set; }
        public string? BookGenres { get; set; }
        public List<int>? BookGenresIds { get; set; }
    }
}
