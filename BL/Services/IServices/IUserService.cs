using BL.DTOs.User;
using BL.Services.GenericService;
using DAL.Entities;

namespace BL.Services.IServices
{
    public interface IUserService : IGenericService<User, UserDetailDto, UserUpdateDto, UserCreateDto>
    {
        IEnumerable<UserDetailDto> GetUsersBySubstringName(string substring);

        void Register(UserCreateDto registerDto);

        UserDetailDto Login(UserLoginDto userLoginDto);

        UserDetailDto? GetUserByUserName(string name);

        void UpdateUser(UserUpdateDto userDto);

        bool CheckPassword(string password, int userId);
    }
}
