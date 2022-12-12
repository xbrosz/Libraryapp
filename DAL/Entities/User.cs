using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class User : BaseEntity
    {
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        [MaxLength(64)]
        public string UserName { get; set; }

        public string Password { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(255)]
        public string Email { get; set; }
        [MaxLength(40)]
        public string PhoneNumber { get; set; }
        [MaxLength(255)]
        public string Address { get; set; }
        
        public virtual List<Reservation> Reservations { get; set; }
    }
}
