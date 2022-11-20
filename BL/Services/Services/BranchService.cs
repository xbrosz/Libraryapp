using AutoMapper;
using BL.DTOs.Author;
using BL.DTOs.Branch;
using BL.QueryObjects;
using BL.QueryObjects.IQueryObject;
using BL.QueryObjects.QueryObjects;
using BL.Services.GenericService;
using BL.Services.IServices;
using DAL.Data;
using DAL.Entities;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Services
{
    public class BranchService : GenericService<Branch, BranchDto, BranchDto, BranchDto>, IBranchService
    {
        private IQueryObject<BranchFilterDto, BranchDto> _branchQueryObject;

        public BranchService(IUnitOfWork unitOfWork, IMapper mapper, IQueryObject<BranchFilterDto, BranchDto> branchQueryObject) : base(unitOfWork, mapper, unitOfWork.BranchRepository) 
        {
            _branchQueryObject = branchQueryObject;
        }

        public IEnumerable<BranchDto> GetBranchesByName(BranchFilterDto filter)
        {
            return _branchQueryObject.ExecuteQuery(filter).Items;
        }

        public IEnumerable<BranchDto> GetBranchesByAddress(BranchFilterDto filter)
        {
            return _branchQueryObject.ExecuteQuery(filter).Items;
        }
    }
}
