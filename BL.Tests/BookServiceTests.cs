using BL.DTOs;
using BL.QueryObjects.IQueryObject;
using DAL.Entities;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Tests
{
    public class BookServiceTests
    {
        Mock<IUnitOfWork> _uowMock;
        Mock<IQueryObject<BookFilterDto, BookGridDto>> _queryObjectMock;
        Mock<IRepository<Book>> _repoMock;

        public BookServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _queryObjectMock = new Mock<IQueryObject<BookFilterDto, BookGridDto>>();
            _repoMock = new Mock<IRepository<Book>>();
        }


    }
}
