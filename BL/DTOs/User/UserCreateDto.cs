using DAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BL.DTOs.User
{
    public class UserCreateDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int RoleId { get; set; } = 2;    // User by default
    }
}
