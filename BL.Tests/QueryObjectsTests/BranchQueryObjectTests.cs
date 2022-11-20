using BL.DTOs.Branch;
using BL.DTOs.User;
using BL.QueryObjects.IQueryObject;
using DAL.Entities;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Tests.QueryObjectsTests
{
    public class BranchQueryObjectTests
    {
        Mock<IUnitOfWork> _uowMock;
        Mock<IQueryObject<BranchFilterDto, BranchDto>> _queryObjectMock;
        Mock<IRepository<Branch>> _repoMock;

        public BranchQueryObjectTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _queryObjectMock = new Mock<IQueryObject<BranchFilterDto, BranchDto>>();
            _repoMock = new Mock<IRepository<Branch>>();
        }


    }
}
