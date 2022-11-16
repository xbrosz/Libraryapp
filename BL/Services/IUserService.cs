using BL.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IUserService
    {
        IEnumerable<UserDetailDto> GetUsersBySubstringName(string substring);
        void Register(CreateUserDto registerDto);
        bool Login(UserLoginDto userLogin);
    }
}
