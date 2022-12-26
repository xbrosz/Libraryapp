using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FE.Models.User
{
    public class UserLoginViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string? UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }


    }
}
