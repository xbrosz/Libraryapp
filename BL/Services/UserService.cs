using Ardalis.GuardClauses;
using AutoMapper;
using BL.DTOs.User;
using BL.Hasher;
using BL.QueryObjects;
using BL.Services;
using DAL.Data;
using DAL.Entities;
using Infrastructure.Repository;

namespace BL.Service
{
    public class UserService : GenericService<User, UserDetailDto, UserDetailDto>, IUserService
    {
        private IMapper mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));
        private IRepository<User> repository;
        private QueryObject<UserFilterDto, UserDetailDto> queryObject;

        public UserService(IRepository<User> repository, QueryObject<UserFilterDto, UserDetailDto> queryObject)
            : base(repository)
        {
            this.repository = repository;
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

            var user = repository.GetByID(userDto.Id);

            return PasswordHasher.Verify(loginDto.Password, user.Password);
        }

        public IEnumerable<UserDetailDto> getUsersBySubstringName(string substring)
        {
            return queryObject.ExecuteQuery(new UserFilterDto() { name = substring, exactName = false }).Items;
        }
    }
}
