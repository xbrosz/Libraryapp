namespace BL.DTOs
{
    public class BookGridDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Release { get; set; }
        public string AuthorName { get; set; }
        public double Rating { get; set; }
        public List<string> BookGenres { get; set; }
    }
}
