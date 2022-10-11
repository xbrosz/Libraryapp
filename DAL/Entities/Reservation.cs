namespace DAL.Entities
{
    public class Reservation : BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int BookPrintId { get; set; }
        public virtual BookPrint BookPrint { get; set; }

    }
}
