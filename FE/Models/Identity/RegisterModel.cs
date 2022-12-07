﻿using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FE.Models.Identity
{
    public class RegisterModel
    {
        [Required]
        [StringLength(64)]
        [Display(Name = "Username")]
        public string? UserName { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

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
        [Compare(nameof(Password))]
        public string? ConfirmPassword { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
