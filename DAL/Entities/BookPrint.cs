namespace DAL.Entities
{
    public class BookPrint
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
        public int BranchId { get; set; }
        public virtual Branch Branch { get; set; }
    }
}
