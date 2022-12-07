﻿using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BL.DTOs.User
{
    public class UserLoginDto
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
