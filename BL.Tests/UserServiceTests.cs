using AutoMapper;
using BL.DTOs;
using BL.DTOs.User;
using BL.Hasher;
using BL.QueryObjects.IQueryObject;
using BL.Services.Services;
using DAL.Entities;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace BL.Tests
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
    }
}
