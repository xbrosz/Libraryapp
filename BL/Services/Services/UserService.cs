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

        public bool CheckPassword(string password, int userId)
        {
            var user = _unitOfWork.UserRepository.GetByID(userId);

            return PasswordHasher.Verify(password, user.Password);
        }

        public UserDetailDto Login(UserLoginDto userLoginDto)
        {
            Guard.Against.NullOrWhiteSpace(userLoginDto.UserName, "UserName", "Username cannot be null");
            Guard.Against.NullOrWhiteSpace(userLoginDto.Password, "Password", "Password cannot be null");

            var queryResult = _queryObject.ExecuteQuery(new UserFilterDto() { Name = userLoginDto.UserName });

            if (queryResult.TotalItemsCount == 0)
            {
                throw new KeyNotFoundException("User doesn't exist.");
            }

            var userDto = queryResult.Items.First();

            if (!CheckPassword(userLoginDto.Password, userDto.Id))
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
            var user = _unitOfWork.UserRepository.GetByID(userDto.Id);

            var toUpdateDto = new UserUpdateDto() { 

                Id= userDto.Id,

                FirstName = userDto.FirstName != null ? userDto.FirstName : user.FirstName,

                LastName = userDto.LastName != null ? userDto.LastName : user.LastName,

                UserName = userDto.UserName != null ? userDto.UserName : user.UserName,

                Email = userDto.Email != null ? userDto.Email : user.Email,

                Address = userDto.Address != null ? userDto.Address : user.Address,

                Password = userDto.Password != null ? PasswordHasher.Hash(userDto.Password) : user.Password,

                RoleId = userDto.RoleId != null ? userDto.RoleId : user.RoleId,

                PhoneNumber = userDto.PhoneNumber != null? userDto.PhoneNumber : user.PhoneNumber,

            };

            Update(toUpdateDto);
        }
    }
}
