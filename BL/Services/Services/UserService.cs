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
    public class UserService : GenericService<User, UserDetailDto, UserDetailDto, CreateUserDto>, IUserService
    {
        private IQueryObject<UserFilterDto, UserDetailDto> queryObject;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IQueryObject<UserFilterDto, UserDetailDto> userQueryObject)
            : base(unitOfWork, mapper, unitOfWork.UserRepository)
        {
            this.queryObject = userQueryObject;
        }

        public void Register(CreateUserDto registerDto)
        {
            Guard.Against.NullOrWhiteSpace(registerDto.UserName, "UserName", "Username cannot be null");
            Guard.Against.NullOrWhiteSpace(registerDto.Password, "Password", "Password cannot be null");

            registerDto.Password = PasswordHasher.Hash(registerDto.Password);

            base.InsertAsync(registerDto);
        }

        public bool Login(UserLoginDto loginDto)
        {
            Guard.Against.NullOrWhiteSpace(loginDto.UserName, "UserName", "Username cannot be null");

            var queryResult = queryObject.ExecuteQuery(new UserFilterDto() { name = loginDto.UserName, exactName = true });

            if (queryResult.TotalItemsCount == 0)
            {
                throw new KeyNotFoundException("User doesn't exist.");
            }

            var userDto = queryResult.Items.First();

            var user = _unitOfWork.UserRepository.GetByID(userDto.Id);

            return PasswordHasher.Verify(loginDto.Password, user.Password);
        }

        public IEnumerable<UserDetailDto> GetUsersBySubstringName(string substring)
        {
            return queryObject.ExecuteQuery(new UserFilterDto() { name = substring, exactName = false }).Items;
        }
    }
}
