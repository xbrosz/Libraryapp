using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FE.Models.User
{
    public class UserEditViewModel
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
        [StringLength(50)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Last name")]
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

    }
}
