﻿using BL.DTOs.User;
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
        void register(CreateUserDto registerDto);
        bool login(UserLoginDto userLogin);
    }
}
