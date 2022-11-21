using BL.DTOs.User;
using BL.Services.GenericService;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.IServices
{
    public interface IUserService : IGenericService<User, UserDetailDto, UserDetailDto, CreateUserDto>
    {
        IEnumerable<UserDetailDto> GetUsersBySubstringName(string substring);
        void Register(CreateUserDto registerDto);
        bool Login(UserLoginDto userLogin);
    }
}
