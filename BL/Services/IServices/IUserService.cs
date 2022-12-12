using BL.DTOs.User;
using BL.Services.GenericService;
using DAL.Entities;

namespace BL.Services.IServices
{
    public interface IUserService : IGenericService<User, UserLoginResponseDto, UserDetailDto, UserCreateDto>
    {
        IEnumerable<UserDetailDto> GetUsersBySubstringName(string substring);

        void Register(UserCreateDto registerDto);

        public int Login(UserLoginDto loginDto);

        UserDetailDto? GetUserByUserName(string name);
    }
}
