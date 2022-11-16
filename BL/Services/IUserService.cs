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
        IEnumerable<UserDetailDto> getUsersBySubstringName(string substring);

        bool login(UserLoginDto userLogin);
    }
}
