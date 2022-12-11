using BL.DTOs.User;
using BL.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Infrastructure.UnitOfWork;
using BL.Facades.IFacades;

namespace BL.Facades.Facades
{
    public class UserFacade : IUserFacade
    {
        private readonly IUserService _userService;

        public UserFacade(IUserService userService)
        {
            _userService = userService;
        }

        public void Register(CreateUserDto user)
        {
            if (_userService.GetUserByUserName(user.UserName) != null) {
                throw new Exception("User name already exists");
            }

            user.RoleId = 2;    // id for role "User"

            _userService.Register(user);
        }

        public int Login(UserLoginDto loginDto)
        {
            var a = _userService.Login(loginDto);
            Console.WriteLine(loginDto.UserName + " was logged in!");
            return a;
        }
    }
}
