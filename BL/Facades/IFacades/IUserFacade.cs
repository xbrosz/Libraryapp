﻿using BL.DTOs.User;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Facades.IFacades
{
    public interface IUserFacade
    {
        void Register(UserCreateDto user);

        UserDetailDto Login(UserLoginDto loginDto);

        UserDetailDto? GetUserByUserName(string userName);

        UserDetailDto? GetUserById(int id);
    }
}
