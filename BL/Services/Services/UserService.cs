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
    public class UserService : GenericService<User, UserDetailDto, UserUpdateDto, UserCreateDto>, IUserService
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

        public UserDetailDto Login(string userName, string password)
        {
            Guard.Against.NullOrWhiteSpace(userName, "UserName", "Username cannot be null");
            Guard.Against.NullOrWhiteSpace(password, "Password", "Password cannot be null");

            var queryResult = _queryObject.ExecuteQuery(new UserFilterDto() { Name = userName });

            if (queryResult.TotalItemsCount == 0)
            {
                throw new KeyNotFoundException("User doesn't exist.");
            }

            var userDto = queryResult.Items.First();

            var user = _unitOfWork.UserRepository.GetByID(userDto.Id);

            if (!PasswordHasher.Verify(password, user.Password))
            {
                throw new Exception("Incorrect password");
            }

            return userDto;
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

        public void UpdateUser(UserUpdateDto userDto)
        {
            if (userDto.RoleId == null)
            {
                userDto.RoleId = _unitOfWork.UserRepository.GetByID(userDto.Id).RoleId;
            }

            if (userDto.Password == null) {
                userDto.Password = _unitOfWork.UserRepository.GetByID(userDto.Id).Password;
            } else
            {
                userDto.Password = PasswordHasher.Hash(userDto.Password);
            }

            Update(userDto);
        }
    }
}
