using BL.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Facades.IFacades
{
    public interface IUserFacade
    {
        void Register(CreateUserDto user);

        int Login(UserLoginDto loginDto);
    }
}
