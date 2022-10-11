using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class User : BaseEntity
    {
        [MaxLength(64)]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public virtual List<Reservation> Reservations { get; set; }
    }
}
