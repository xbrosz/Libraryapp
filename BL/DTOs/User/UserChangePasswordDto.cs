﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.User
{
    public class UserChangePasswordDto
    {
        public int Id { get; set; }

        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
