using BL.DTOs.User;
using BL.Services.GenericService;
using DAL.Entities;

namespace BL.Services.IServices
{
    public interface IUserService : IGenericService<User, UserDetailDto, UserDetailDto, UserCreateDto>
    {
        IEnumerable<UserDetailDto> GetUsersBySubstringName(string substring);

        void Register(UserCreateDto registerDto);

        UserDetailDto Login(UserLoginDto loginDto);

        UserDetailDto? GetUserByUserName(string name);
    }
}
