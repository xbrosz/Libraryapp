namespace DAL.Entities
{
    public class BookPrint : BaseEntity
    {
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
        public int BranchId { get; set; }
        public virtual Branch Branch { get; set; }
    }
}
