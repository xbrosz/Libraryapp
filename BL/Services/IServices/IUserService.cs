using BL.DTOs.User;
using BL.Services.GenericService;
using DAL.Entities;

namespace BL.Services.IServices
{
    public interface IUserService : IGenericService<User, UserDetailDto, UserUpdateDto, UserCreateDto>
    {

        void Register(UserCreateDto registerDto);

        UserDetailDto Login(UserLoginDto userLoginDto);

        UserDetailDto? GetUserByUserName(string name);

        void UpdateUser(UserUpdateDto userDto);

        bool CheckPassword(string password, int userId);

        IEnumerable<UserDetailDto> GetUsersBySubStringUserName(string substring);

        IEnumerable<UserDetailDto> GetAllUsers();

        void Delete(int id);
    }
}
