namespace DAL.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Release { get; set; }
        public virtual int AuthorId { get; set; }
        public virtual Author Author { get; set; }
  
        public virtual List<BookPrint> BookPrints { get; set; }
        public virtual List<Rating> Ratings { get; set; }

    }
}
