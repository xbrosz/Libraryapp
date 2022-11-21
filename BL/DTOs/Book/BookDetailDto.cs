namespace BL.DTOs
{
    public class BookDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Release { get; set; }
        public string AuthorName { get; set; }
        public string BookGenres { get; set; }

    }
}
