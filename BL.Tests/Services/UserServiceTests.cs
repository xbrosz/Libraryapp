using AutoMapper;
using BL.DTOs;
using BL.DTOs.User;
using BL.Hasher;
using BL.QueryObjects.IQueryObject;
using BL.Services.Services;
using DAL.Entities;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace BL.Tests.Services
{
    public class UserServiceTests
    {
        Mock<IUnitOfWork> _uowMock;
        Mock<IQueryObject<UserFilterDto, UserDetailDto>> _queryObjectMock;
        Mock<IRepository<User>> _repoMock;

        public UserServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _queryObjectMock = new Mock<IQueryObject<UserFilterDto, UserDetailDto>>();
            _repoMock = new Mock<IRepository<User>>();
        }

        [Fact]
        public void Login_CorrectCredentials()
        {
            var mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));

            var user = new User
            {
                Id = 1,
                UserName = "xkristof",
                FirstName = "Kristof",
                LastName = "Adamek",
                Password = PasswordHasher.Hash("passwd")
            };

            var userDetailDto = new UserDetailDto
            {
                Id = 1,
                UserName = "xkristof",
                FirstName = "Kristof",
                LastName = "Adamek"
            };

            var query = new QueryResultDto<UserDetailDto>
            {
                Items = new List<UserDetailDto>() { userDetailDto },
                TotalItemsCount = 1,
            };

            _repoMock
                .Setup(x => x.GetByID(1))
                .Returns(user);

            _uowMock
                .Setup(x => x.UserRepository)
                .Returns(_repoMock.Object);

            _queryObjectMock
                .Setup(x => x.ExecuteQuery(It.IsAny<UserFilterDto>()))
                .Returns(query);

            var service = new UserService(_uowMock.Object, mapper, _queryObjectMock.Object);

            var loginDto = new UserLoginDto() { UserName = "xkristof", Password = "passwd" };

            var roleId = service.Login(loginDto);

            _repoMock.Verify(x => x.GetByID(1), Times.Once);
            _uowMock.Verify(x => x.UserRepository, Times.Exactly(2));
            _queryObjectMock.Verify(x => x.ExecuteQuery(It.IsAny<UserFilterDto>()), Times.Once);

            Assert.True(roleId == user.RoleId);
        }

        [Fact]
        public void Login_IncorrectPassword()
        {
            var mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));

            var user = new User
            {
                Id = 1,
                UserName = "xkristof",
                FirstName = "Kristof",
                LastName = "Adamek",
                Password = PasswordHasher.Hash("passwd")
            };

            var userDetailDto = new UserDetailDto
            {
                Id = 1,
                UserName = "xkristof",
                FirstName = "Kristof",
                LastName = "Adamek"
            };

            var query = new QueryResultDto<UserDetailDto>
            {
                Items = new List<UserDetailDto>() { userDetailDto },
                TotalItemsCount = 1,
            };

            _repoMock
                .Setup(x => x.GetByID(1))
                .Returns(user);

            _uowMock
                .Setup(x => x.UserRepository)
                .Returns(_repoMock.Object);

            _queryObjectMock
                .Setup(x => x.ExecuteQuery(It.IsAny<UserFilterDto>()))
                .Returns(query);

            var service = new UserService(_uowMock.Object, mapper, _queryObjectMock.Object);

            var loginDto = new UserLoginDto() { UserName = "xkristof", Password = "passwwwd" };

            var exception = Assert.Throws<Exception>(() => service.Login(loginDto));

            _repoMock.Verify(x => x.GetByID(1), Times.Once);
            _uowMock.Verify(x => x.UserRepository, Times.Exactly(2));
            _queryObjectMock.Verify(x => x.ExecuteQuery(It.IsAny<UserFilterDto>()), Times.Once);

            Assert.Equal("Password is incorrect", exception.Message);
        }

        [Fact]
        public void Login_UserDoesntExist()
        {
            var mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));

            _uowMock
                .Setup(x => x.UserRepository)
                .Returns(_repoMock.Object);

            _queryObjectMock
                .Setup(x => x.ExecuteQuery(It.IsAny<UserFilterDto>()))
                .Returns(new QueryResultDto<UserDetailDto> { TotalItemsCount = 0 });

            var service = new UserService(_uowMock.Object, mapper, _queryObjectMock.Object);

            var loginDto = new UserLoginDto() { UserName = "xkristof", Password = "passwwwd" };

            KeyNotFoundException exception = Assert.Throws<KeyNotFoundException>(() => service.Login(loginDto));

            _uowMock.Verify(x => x.UserRepository, Times.Exactly(1));
            _queryObjectMock.Verify(x => x.ExecuteQuery(It.IsAny<UserFilterDto>()), Times.Once);

            Assert.Equal("User doesn't exist.", exception.Message);
        }

        [Fact]
        public void Login_UserNameIsNull()
        {
            var mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));

            _uowMock
                .Setup(x => x.UserRepository)
                .Returns(_repoMock.Object);

            _queryObjectMock
                .Setup(x => x.ExecuteQuery(It.IsAny<UserFilterDto>()))
                .Returns(new QueryResultDto<UserDetailDto> { TotalItemsCount = 0 });

            var service = new UserService(_uowMock.Object, mapper, _queryObjectMock.Object);

            var loginDto = new UserLoginDto() { UserName = null, Password = "passwwwd" };

            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => service.Login(loginDto));

            _uowMock.Verify(x => x.UserRepository, Times.Once);
            _queryObjectMock.Verify(x => x.ExecuteQuery(It.IsAny<UserFilterDto>()), Times.Never);

            Assert.Equal("Username cannot be null (Parameter 'UserName')", exception.Message);
        }

        [Fact]
        public void Login_PasswordIsNull()
        {
            var mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));

            _uowMock
                .Setup(x => x.UserRepository)
                .Returns(_repoMock.Object);

            _queryObjectMock
                .Setup(x => x.ExecuteQuery(It.IsAny<UserFilterDto>()))
                .Returns(new QueryResultDto<UserDetailDto> { TotalItemsCount = 0 });

            var service = new UserService(_uowMock.Object, mapper, _queryObjectMock.Object);

            var loginDto = new UserLoginDto() { UserName = "xkarel", Password = null };

            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => service.Login(loginDto));

            _uowMock.Verify(x => x.UserRepository, Times.Once);
            _queryObjectMock.Verify(x => x.ExecuteQuery(It.IsAny<UserFilterDto>()), Times.Never);

            Assert.Equal("Password cannot be null (Parameter 'Password')", exception.Message);
        }

        [Fact]
        public void Register_ValidCredentials()
        {
            var mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));

            _uowMock
                .Setup(x => x.UserRepository)
                .Returns(_repoMock.Object);

            _queryObjectMock
                .Setup(x => x.ExecuteQuery(It.IsAny<UserFilterDto>()))
                .Returns(new QueryResultDto<UserDetailDto> { TotalItemsCount = 0 });

            var service = new UserService(_uowMock.Object, mapper, _queryObjectMock.Object);

            var registerDto = new UserCreateDto()
            {
                UserName = "xkristof",
                FirstName = "Kristof",
                LastName = "Adamek",
                Address = "Brno",
                Email = "kristof@gmail.com",
                Password = "passwd",
                PhoneNumber = "1234"
            };

            service.Register(registerDto);

            _uowMock.Verify(x => x.UserRepository, Times.Exactly(1));
            _repoMock.Verify(x => x.Insert(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public void Register_UserNameIsNull()
        {
            var mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));

            _uowMock
                .Setup(x => x.UserRepository)
                .Returns(_repoMock.Object);

            _queryObjectMock
                .Setup(x => x.ExecuteQuery(It.IsAny<UserFilterDto>()))
                .Returns(new QueryResultDto<UserDetailDto> { TotalItemsCount = 0 });

            var service = new UserService(_uowMock.Object, mapper, _queryObjectMock.Object);

            var registerDto = new UserCreateDto() { UserName = null, Password = "passwwwd" };

            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => service.Register(registerDto));

            _uowMock.Verify(x => x.UserRepository, Times.Once);
            _queryObjectMock.Verify(x => x.ExecuteQuery(It.IsAny<UserFilterDto>()), Times.Never);

            Assert.Equal("Username cannot be null (Parameter 'UserName')", exception.Message);
        }

        [Fact]
        public void Register_PasswordIsNull()
        {
            var mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));

            _uowMock
                .Setup(x => x.UserRepository)
                .Returns(_repoMock.Object);

            _queryObjectMock
                .Setup(x => x.ExecuteQuery(It.IsAny<UserFilterDto>()))
                .Returns(new QueryResultDto<UserDetailDto> { TotalItemsCount = 0 });

            var service = new UserService(_uowMock.Object, mapper, _queryObjectMock.Object);

            var registerDto = new UserCreateDto() { UserName = "xkarel", Password = null };

            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => service.Register(registerDto));

            _uowMock.Verify(x => x.UserRepository, Times.Once);
            _queryObjectMock.Verify(x => x.ExecuteQuery(It.IsAny<UserFilterDto>()), Times.Never);

            Assert.Equal("Password cannot be null (Parameter 'Password')", exception.Message);
        }
    }
}
