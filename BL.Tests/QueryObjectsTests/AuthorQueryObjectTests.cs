using BL.DTOs.Author;
using BL.DTOs.User;
using BL.QueryObjects.IQueryObject;
using DAL.Entities;
using Infrastructure.Query;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BL.Tests.QueryObjectsTests
{
    public class AuthorQueryObjectTests
    {
        Mock<IUnitOfWork> _uowMock;
        Mock<IQueryObject<AuthorFilterDto, AuthorDto>> _queryObjectMock;
        Mock<IRepository<Author>> _repoMock;

        public AuthorQueryObjectTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _queryObjectMock = new Mock<IQueryObject<AuthorFilterDto, AuthorDto>>();
            _repoMock = new Mock<IRepository<Author>>();
        }

        [Fact]
        public void ExecuteQuery_Test()
        {   // Expression<Func<T, bool>> rootPredicate, string columnName
            _queryObjectMock
                .Setup(x => x.Execute(It.IsAny<Expression<Func<, bool>>>()))
                .Returns(queryResult);
        }
    }
}
