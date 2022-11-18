using Ardalis.GuardClauses;
using AutoMapper;
using BL.DTOs.User;
using BL.Hasher;
using BL.QueryObjects;
using BL.Services;
using BL.Services.GenericService;
using BL.Services.IServices;
using DAL.Data;
using DAL.Entities;
using Infrastructure.Repository;

namespace BL.Services.Services
{
    public class UserService : GenericService<User, UserDetailDto, UserDetailDto, CreateUserDto>, IUserService
    {
        private IMapper mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));
        private IRepository<User> repository;
        private LibraryappDbContext dbContext;
        private UserQueryObject queryObject;

        public UserService(IRepository<User> repository, LibraryappDbContext dbContext)
            : base(repository)
        {
            this.repository = repository;
            this.dbContext = dbContext;
        }

        public void Register(CreateUserDto registerDto)
        {
            Guard.Against.NullOrWhiteSpace(registerDto.UserName, "UserName", "Username cannot be null");
            Guard.Against.NullOrWhiteSpace(registerDto.Password, "Password", "Password cannot be null");

            registerDto.Password = PasswordHasher.Hash(registerDto.Password);

            base.Insert(registerDto);
        }

        public bool Login(UserLoginDto loginDto)
        {
            Guard.Against.NullOrWhiteSpace(loginDto.UserName, "UserName", "Username cannot be null");

            queryObject = new UserQueryObject(mapper, dbContext);

            var queryResult = queryObject.ExecuteQuery(new UserFilterDto() { name = loginDto.UserName, exactName = true });

            if (queryResult.TotalItemsCount == 0)
            {
                throw new KeyNotFoundException("User doesn't exist.");
            }

            var userDto = queryResult.Items.First();

            var user = repository.GetByID(userDto.Id);

            return PasswordHasher.Verify(loginDto.Password, user.Password);
        }

        public IEnumerable<UserDetailDto> GetUsersBySubstringName(string substring)
        {
            return queryObject.ExecuteQuery(new UserFilterDto() { name = substring, exactName = false }).Items;
        }
    }
}
