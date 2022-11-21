using AutoMapper;
using BL.DTOs;
using BL.DTOs.Branch;
using BL.QueryObjects.QueryObjects;
using DAL.Entities;
using Infrastructure.Query;
using System.Linq.Expressions;

namespace BL.Tests.QueryObjects
{
    public class BranchQueryObjectTests
    {
        private Mock<IMapper> _mapperMock;
        private Mock<IAbstractQuery<Branch>> _queryMock;

        public BranchQueryObjectTests()
        {
            _mapperMock = new Mock<IMapper>();
            _queryMock = new Mock<IAbstractQuery<Branch>>();
        }

        [Fact]
        public void FilterBranches()
        {
            var branch = new Branch()
            {
                Id = 1,
                Name = "Moravska kniznica",
                Address = "Namesti svobody"
            };

            var branchDto = new BranchDto()
            {
                Id = 1,
                Name = "Moravska kniznica",
                Address = "Namesti svobody"
            };

            var queryResultDto = new QueryResultDto<BranchDto>()
            {
                Items = new List<BranchDto>() { branchDto },
                TotalItemsCount = 1
            };

            var efQueryResult = new EFQueryResult<Branch>()
            {
                Items = new List<Branch>() { branch },
                TotalItemsCount = 1
            };

            _queryMock
                .Setup(x => x.Where(It.IsAny<Expression<Func<string, bool>>>(), It.IsAny<string>()))
                .Returns(_queryMock.Object);

            _queryMock
                .Setup(x => x.Page(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(_queryMock.Object);

            _queryMock
                .Setup(x => x.OrderBy<string>(It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(_queryMock.Object);

            _queryMock
                .Setup(x => x.Execute())
                .Returns(efQueryResult);

            _mapperMock
                .Setup(x => x.Map<QueryResultDto<BranchDto>>(It.IsAny<EFQueryResult<Branch>>()))
                .Returns(queryResultDto);

            var queryObject = new BranchQueryObject(_mapperMock.Object, _queryMock.Object);

            var filterDto = new BranchFilterDto()
            {
                Name = "Moravska kniznica",
                Address = "Namesti svobody",
                PageSize = 5,
                RequestedPageNumber = 1,
                SortCriteria = "Name",
                SortAscending = true
            };

            var res = queryObject.ExecuteQuery(filterDto);

            Assert.Equal(queryResultDto, res);
            _queryMock.Verify(x => x.Where(It.IsAny<Expression<Func<string, bool>>>(), It.IsAny<string>()), Times.Exactly(2));
            _queryMock.Verify(x => x.Page(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            _queryMock.Verify(x => x.OrderBy<string>(It.IsAny<string>(), It.IsAny<bool>()), Times.Once);
            _queryMock.Verify(x => x.Execute(), Times.Once);
        }
    }
}
