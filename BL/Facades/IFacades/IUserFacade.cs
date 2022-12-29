using BL.DTOs.User;
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

        UserDetailDto Login(UserLoginDto userloginDto);

        UserDetailDto? GetUserByUserName(string userName);

        UserDetailDto? GetUserById(int id);

        void UpdateUserData(UserUpdateDto userDto);

        public bool UpdateUserPassword(UserChangePasswordDto userDto);

        IEnumerable<UserDetailDto> GetUsersBySubStringUserName(string subString);

        IEnumerable<UserDetailDto> GetAllUsers();

        void SwitchRoleForUserId(int userId);
    }
}
