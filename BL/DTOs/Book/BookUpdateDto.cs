namespace BL.DTOs
{
    public class BookUpdateDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime? Release { get; set; }
        public int? AuthorId { get; set; }
        public double? RatingNumber { get; set; }
    }
}
