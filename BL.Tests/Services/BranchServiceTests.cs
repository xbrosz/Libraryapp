using AutoMapper;
using BL.DTOs;
using BL.DTOs.Branch;
using BL.QueryObjects.IQueryObject;
using BL.Services.Services;
using Infrastructure.UnitOfWork;

namespace BL.Tests.Services
{
    public class BranchServiceTests
    {
        Mock<IUnitOfWork> _uowMock;
        Mock<IQueryObject<BranchFilterDto, BranchDto>> _queryObjectMock;
        IMapper _mapper;

        public BranchServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _queryObjectMock = new Mock<IQueryObject<BranchFilterDto, BranchDto>>();
            _mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));
        }

        [Fact]
        public void GetBranchesByName()
        {
            var branchDto = new BranchDto()
            {
                Id = 1,
                Name = "Moravska kniznica",
                Address = "Namesti svobody"
            };

            var queryResult = new QueryResultDto<BranchDto>
            {
                Items = new List<BranchDto>() { branchDto },
                TotalItemsCount = 1
            };

            _queryObjectMock
                .Setup(x => x.ExecuteQuery(It.IsAny<BranchFilterDto>()))
                .Returns(queryResult);

            var service = new BranchService(_uowMock.Object, _mapper, _queryObjectMock.Object);

            var expectedOutput = new List<BranchDto>() { branchDto };

            var realOutput = service.GetBranchesByName("Moravska kniznica");

            _queryObjectMock.Verify(x => x.ExecuteQuery(It.IsAny<BranchFilterDto>()), Times.Once);
            Assert.Equal(expectedOutput, realOutput);
        }

        [Fact]
        public void GetBranchesByAddress()
        {
            var branchDto = new BranchDto()
            {
                Id = 1,
                Name = "Moravska kniznica",
                Address = "Namesti svobody"
            };

            var queryResult = new QueryResultDto<BranchDto>
            {
                Items = new List<BranchDto>() { branchDto },
                TotalItemsCount = 1
            };

            _queryObjectMock
                .Setup(x => x.ExecuteQuery(It.IsAny<BranchFilterDto>()))
                .Returns(queryResult);

            var service = new BranchService(_uowMock.Object, _mapper, _queryObjectMock.Object);

            var expectedOutput = new List<BranchDto>() { branchDto };

            var realOutput = service.GetBranchesByName("Namesti svobody");

            _queryObjectMock.Verify(x => x.ExecuteQuery(It.IsAny<BranchFilterDto>()), Times.Once);
            Assert.Equal(expectedOutput, realOutput);
        }
    }
}
