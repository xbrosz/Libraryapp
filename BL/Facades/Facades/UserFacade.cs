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

        public void Register(UserCreateDto user)
        {
            if (_userService.GetUserByUserName(user.UserName) != null) {
                throw new Exception("User name already exists");
            }

            _userService.Register(user);
        }

        public UserDetailDto Login(UserLoginDto userloginDto)
        {
            return _userService.Login(userloginDto);
        }

        public UserDetailDto? GetUserByUserName(string userName)
        {
            return _userService.GetUserByUserName(userName);
        }

        public UserDetailDto? GetUserById(int id)
        {
            return _userService.Find(id);
        }

        public void UpdateUserData(UserUpdateDto userDto)
        {
            _userService.UpdateUser(userDto);
        }

        public bool UpdateUserPassword(UserChangePasswordDto userDto)
        {
            if (!_userService.CheckPassword(userDto.CurrentPassword, userDto.Id))
            {
                return false;
            }

            _userService.UpdateUser(new UserUpdateDto()
            {
                Password = userDto.NewPassword,
                Id = userDto.Id,
            });

            return true;
        }
    }
}
