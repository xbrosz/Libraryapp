using BL.DTOs.User;
using BL.Services.GenericService;
using DAL.Entities;

namespace BL.Services.IServices
{
    public interface IUserService : IGenericService<User, UserDetailDto, UserDetailDto, CreateUserDto>
    {
        IEnumerable<UserDetailDto> GetUsersBySubstringName(string substring);

        void Register(CreateUserDto registerDto);

        public int Login(UserLoginDto loginDto);

        UserDetailDto? GetUserByUserName(string name);
    }
}
