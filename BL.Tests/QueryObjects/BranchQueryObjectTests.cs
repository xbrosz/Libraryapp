using AutoMapper;
using DAL.Entities;
using Infrastructure.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
