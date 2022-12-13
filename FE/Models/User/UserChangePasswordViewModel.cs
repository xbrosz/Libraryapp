using System.ComponentModel.DataAnnotations;

namespace FE.Models.User
{
    public class UserChangePasswordViewModel
    {
        [Required]
        [Display(Name = "Current password")]
        public string? CurrentPassword { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8)]
        [RegularExpression(@"([a-zA-Z]+[^a-zA-Z]+|[^a-zA-Z]+[a-zA-Z]+).*", ErrorMessage = "The password is not complex enough")]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string? NewPassword { get; set; }

        //[DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare(nameof(NewPassword))]
        public string? ConfirmNewPassword { get; set; }
    }
}
