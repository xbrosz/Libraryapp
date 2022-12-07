using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FE.Models.Identity
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Username")]
        public string? UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
