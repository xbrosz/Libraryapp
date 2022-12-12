using DAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BL.DTOs.User
{
    public class UserCreateDto
    {
        [Required]
        [StringLength(64)]
        [Display(Name = "Username")]
        public string? UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string? Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8)]
        [RegularExpression(@"([a-zA-Z]+[^a-zA-Z]+|[^a-zA-Z]+[a-zA-Z]+).*", ErrorMessage = "The password is not complex enough")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string? ConfirmPassword { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Laste name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(40)]
                                                // ToDo require correct phone number format
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(255)]                           
        [Display(Name = "Address")]
        public string Address { get; set; }

        public int RoleId { get; set; }
    }
}
