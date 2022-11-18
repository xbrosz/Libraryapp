using Ardalis.GuardClauses;
using AutoMapper;
using BL.DTOs.User;
using BL.Hasher;
using BL.QueryObjects;
using BL.Services;
using BL.Services.GenericService;
using BL.Services.IServices;
using DAL.Entities;
using Infrastructure.Repository;
using BL.QueryObjects.IQueryObject;
using Infrastructure.UnitOfWork;
using DAL.Data;

namespace BL.Services.Services
{
    public class UserService : GenericService<User, UserDetailDto, UserDetailDto, UserDetailDto>, IUserService
    {
        private IQueryObject<UserFilterDto, UserDetailDto> queryObject;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IQueryObject<UserFilterDto, UserDetailDto> queryObject) : base(unitOfWork, mapper, unitOfWork.UserRepository) {
            this.queryObject = queryObject;
        }

        public void register(CreateUserDto registerDto)
        {
            Guard.Against.NullOrWhiteSpace(registerDto.UserName, "UserName", "Username cannot be null");
            Guard.Against.NullOrWhiteSpace(registerDto.Password, "Password", "Password cannot be null");

            registerDto.Password = PasswordHasher.Hash(registerDto.Password);

            //TODO: call genericservice to create user
        }

        public bool login(UserLoginDto loginDto)
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

        public IEnumerable<UserDetailDto> getUsersBySubstringName(string substring)
        {
            return queryObject.ExecuteQuery(new UserFilterDto() { name = substring, exactName = false }).Items;
        }
    }
}
