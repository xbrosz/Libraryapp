using Ardalis.GuardClauses;
using AutoMapper;
using BL.DTOs.User;
using BL.Hasher;
using BL.QueryObjects.IQueryObject;
using BL.Services.GenericService;
using BL.Services.IServices;
using DAL.Entities;
using Infrastructure.UnitOfWork;

namespace BL.Services.Services
{
    public class UserService : GenericService<User, UserLoginResponseDto, UserDetailDto, UserCreateDto>, IUserService
    {
        private IQueryObject<UserFilterDto, UserDetailDto> _queryObject;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IQueryObject<UserFilterDto, UserDetailDto> userQueryObject)
            : base(unitOfWork, mapper, unitOfWork.UserRepository)
        {
            _queryObject = userQueryObject;
        }

        public void Register(UserCreateDto registerDto)
        {
            Guard.Against.NullOrWhiteSpace(registerDto.UserName, "UserName", "Username cannot be null");
            Guard.Against.NullOrWhiteSpace(registerDto.Password, "Password", "Password cannot be null");

            registerDto.Password = PasswordHasher.Hash(registerDto.Password);

            Insert(registerDto);
        }

        public int Login(UserLoginDto loginDto)
        {
            Guard.Against.NullOrWhiteSpace(loginDto.UserName, "UserName", "Username cannot be null");
            Guard.Against.NullOrWhiteSpace(loginDto.Password, "Password", "Password cannot be null");

            var queryResult = _queryObject.ExecuteQuery(new UserFilterDto() { Name = loginDto.UserName, ExactName = true });

            if (queryResult.TotalItemsCount == 0)
            {
                throw new KeyNotFoundException("User doesn't exist.");
            }

            var userDto = queryResult.Items.First();

            var userCheckLoginDto = Find(userDto.Id);

            if (!PasswordHasher.Verify(loginDto.Password, userCheckLoginDto.Password))
            {
                throw new Exception("Password is incorrect");
            }

            return userCheckLoginDto.RoleId;
        }

        public IEnumerable<UserDetailDto> GetUsersBySubstringName(string substring)
        {
            return _queryObject.ExecuteQuery(new UserFilterDto() { Name = substring, ExactName = false }).Items;
        }

        public UserDetailDto? GetUserByUserName(string userName)
        {
            var queryResult = _queryObject.ExecuteQuery(new UserFilterDto() { UserName = userName });
  
            if (queryResult.TotalItemsCount == 0)
            {
                return null;
            }
            return queryResult.Items.First();
        }
    }
}
