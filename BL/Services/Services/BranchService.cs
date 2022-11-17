using BL.DTOs.Author;
using BL.DTOs.Branch;
using BL.QueryObjects;
using BL.Services.GenericService;
using BL.Services.IServices;
using DAL.Data;
using DAL.Entities;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Services
{
    public class BranchService : GenericService<Branch, BranchDto, BranchDto, BranchDto>, IBranchService
    {
        private LibraryappDbContext _dbContext;
        public BranchService(IRepository<Branch> repository, LibraryappDbContext dbContext) : base(repository)
        {
            _dbContext = dbContext;
        }

        public BranchDto GetBranchByName(BranchFilterDto filter)
        {
            var queryObject = new BranchQueryObject(mapper, _dbContext);
            return queryObject.ExecuteQuery(filter).Items.First();
        }
    }
}

