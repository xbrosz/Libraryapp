using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.User
{
    public class UserLoginResponseDto
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Password { get; set; }
    }
}
