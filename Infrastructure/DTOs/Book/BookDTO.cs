namespace Infrastructure.DTOs.Book
{
    public class BookDTO
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public List<string> Genres { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int NumberOfPrints { get; set; }
        public double? AverageRating { get; set; }
    }
}
