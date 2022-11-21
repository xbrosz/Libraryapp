using AutoMapper;
using BL.DTOs;
using BL.DTOs.User;
using BL.QueryObjects.QueryObjects;
using DAL.Entities;
using Infrastructure.Query;
using System.Linq.Expressions;

namespace BL.Tests.QueryObjects
{
    public class UserQueryObjectTests
    {
        private Mock<IMapper> _mapperMock;
        private Mock<IAbstractQuery<User>> _queryMock;

        public UserQueryObjectTests()
        {
            _mapperMock = new Mock<IMapper>();
            _queryMock = new Mock<IAbstractQuery<User>>();
        }

        [Fact]
        public void FilterReservations()
        {
            var user1 = new User()
            {
                Id = 1,
                UserName = "xkarol",
                FirstName = "Karol",
                LastName = "Adamek",
                Password = "passwd1"
            };

            var userDto1 = new UserDetailDto() { Id = 1, UserName = "xkarol", FirstName = "Karol", LastName = "Adamek" };

            var queryResultDto = new QueryResultDto<UserDetailDto>()
            {
                Items = new List<UserDetailDto>() { userDto1 },
                TotalItemsCount = 1
            };

            var efQueryResult = new EFQueryResult<User>() { Items = new List<User>() { user1 }, TotalItemsCount = 1 };
            _queryMock
                .Setup(x => x.Where(It.IsAny<Expression<Func<string, bool>>>(), It.IsAny<string>()))
                .Returns(_queryMock.Object);

            _queryMock
                .Setup(x => x.Page(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(_queryMock.Object);

            _queryMock
                .Setup(x => x.Execute())
                .Returns(efQueryResult);

            _mapperMock
                .Setup(x => x.Map<QueryResultDto<UserDetailDto>>(It.IsAny<EFQueryResult<User>>()))
                .Returns(queryResultDto);

            var queryObject = new UserQueryObject(_mapperMock.Object, _queryMock.Object);

            var filterDto = new UserFilterDto()
            {
                name = "karol",
                exactName = true,
                PageSize = 5,
                RequestedPageNumber = 1
            };

            var res = queryObject.ExecuteQuery(filterDto);

            Assert.Equal(queryResultDto, res);
            _queryMock.Verify(x => x.Where(It.IsAny<Expression<Func<string, bool>>>(), It.IsAny<string>()), Times.Once);
            _queryMock.Verify(x => x.Page(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            _queryMock.Verify(x => x.Execute(), Times.Once);
        }
    }
}
