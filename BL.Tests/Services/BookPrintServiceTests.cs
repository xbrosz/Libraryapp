using AutoMapper;
using BL.DTOs;
using BL.QueryObjects.IQueryObject;
using BL.Services.Services;
using DAL.Entities;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace BL.Tests.QueryObjects
{
    public class BookPrintServiceTests
    {
        Mock<IUnitOfWork> _uowMock;
        Mock<IQueryObject<BookPrintFilterDto, BookPrintDto>> _queryObjectMock;
        Mock<IRepository<BookPrint>> _repoMock;

        public BookPrintServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _queryObjectMock = new Mock<IQueryObject<BookPrintFilterDto, BookPrintDto>>();
            _repoMock = new Mock<IRepository<BookPrint>>();
        }

        [Fact]
        public void GetBookPrintsByBranchIDAndBookIDTest()
        {
            var mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));

            var bookPrint = new BookPrint
            {
                Book = new Book
                {
                    Id = 1,
                    Author = new Author
                    {
                        Id = 1,
                        FirstName = "J.",
                        MiddleName = "R.R.",
                        LastName = "Tolkien",
                        BirthDate = new DateTime(1900, 6, 6)
                    },
                    Release = DateTime.Now,
                    Title = "C++ for begginers"

                },

                Branch = new Branch
                {
                    Id = 1,
                    Address = "123 Street",
                    Name = "Library 1"
                }
            };
            var bookPrintDto = new BookPrintDto
            {
                Id = 1,
                BookId = 1,
                BranchId = 1
            };

            var query = new QueryResultDto<BookPrintDto>
            {
                Items = new List<BookPrintDto>() { bookPrintDto },
                TotalItemsCount = 1,
            };

            _repoMock
                .Setup(x => x.GetByID(1))
                .Returns(bookPrint);

            _uowMock
                .Setup(x => x.BookPrintRepository)
                .Returns(_repoMock.Object);

            _queryObjectMock
                .Setup(x => x.ExecuteQuery(It.IsAny<BookPrintFilterDto>()))
                .Returns(query);

            var service = new BookPrintService(_uowMock.Object, mapper, _queryObjectMock.Object);
            var result = service.GetBookPrintsByBranchIDAndBookID(1, 1);


            Assert.True(result.Count() == 1 &&
                        result.First().Id == 1 &&
                        result.First().BranchId == 1 &&
                        result.First().BookId == 1);
        }

        [Fact]
        public void GetBookPrintsByBranchIDTest()
        {
            var mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));

            var bookPrint = new BookPrint
            {
                Book = new Book
                {
                    Id = 1,
                    Author = new Author
                    {
                        Id = 1,
                        FirstName = "J.",
                        MiddleName = "R.R.",
                        LastName = "Tolkien",
                        BirthDate = new DateTime(1900, 6, 6)
                    },
                    Release = DateTime.Now,
                    Title = "C++ for begginers"

                },

                Branch = new Branch
                {
                    Id = 1,
                    Address = "123 Street",
                    Name = "Library 1"
                }
            };
            var bookPrintDto = new BookPrintDto
            {
                Id = 1,
                BookId = 1,
                BranchId = 1
            };

            var query = new QueryResultDto<BookPrintDto>
            {
                Items = new List<BookPrintDto>() { bookPrintDto },
                TotalItemsCount = 1,
            };

            _repoMock
                .Setup(x => x.GetByID(1))
                .Returns(bookPrint);

            _uowMock
                .Setup(x => x.BookPrintRepository)
                .Returns(_repoMock.Object);

            _queryObjectMock
                .Setup(x => x.ExecuteQuery(It.IsAny<BookPrintFilterDto>()))
                .Returns(query);

            var service = new BookPrintService(_uowMock.Object, mapper, _queryObjectMock.Object);
            var result = service.GetBookPrintsByBranchID(1);


            Assert.True(result.Count() == 1 &&
                        result.First().Id == 1 &&
                        result.First().BranchId == 1 &&
                        result.First().BookId == 1);
        }

        [Fact]
        public void GetBookPrintsByBookIDTest()
        {
            var mapper = new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));

            var bookPrint = new BookPrint
            {
                Book = new Book
                {
                    Id = 1,
                    Author = new Author
                    {
                        Id = 1,
                        FirstName = "J.",
                        MiddleName = "R.R.",
                        LastName = "Tolkien",
                        BirthDate = new DateTime(1900, 6, 6)
                    },
                    Release = DateTime.Now,
                    Title = "C++ for begginers"

                },

                Branch = new Branch
                {
                    Id = 1,
                    Address = "123 Street",
                    Name = "Library 1"
                }
            };
            var bookPrintDto = new BookPrintDto
            {
                Id = 1,
                BookId = 1,
                BranchId = 1
            };

            var query = new QueryResultDto<BookPrintDto>
            {
                Items = new List<BookPrintDto>() { bookPrintDto },
                TotalItemsCount = 1,
            };

            _repoMock
                .Setup(x => x.GetByID(1))
                .Returns(bookPrint);

            _uowMock
                .Setup(x => x.BookPrintRepository)
                .Returns(_repoMock.Object);

            _queryObjectMock
                .Setup(x => x.ExecuteQuery(It.IsAny<BookPrintFilterDto>()))
                .Returns(query);

            var service = new BookPrintService(_uowMock.Object, mapper, _queryObjectMock.Object);
            var result = service.GetBookPrintsByBranchID(1);


            Assert.True(result.Count() == 1 &&
                        result.First().Id == 1 &&
                        result.First().BranchId == 1 &&
                        result.First().BookId == 1);
        }
    }
}
