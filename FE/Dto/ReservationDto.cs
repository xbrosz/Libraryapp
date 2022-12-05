namespace FE.Dto
{
    public class ReservationDto : BaseDto
    {
        public string BookTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Branch { get; set; }
    }
}
