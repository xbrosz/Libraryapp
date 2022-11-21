using AutoMapper;
using BL.DTOs.Branch;
using BL.QueryObjects.IQueryObject;
using BL.Services.GenericService;
using BL.Services.IServices;
using DAL.Entities;
using Infrastructure.UnitOfWork;

namespace BL.Services.Services
{
    public class BranchService : GenericService<Branch, BranchDto, BranchDto, BranchDto>, IBranchService
    {
        private IQueryObject<BranchFilterDto, BranchDto> _branchQueryObject;

        public BranchService(IUnitOfWork unitOfWork, IMapper mapper, IQueryObject<BranchFilterDto, BranchDto> branchQueryObject) : base(unitOfWork, mapper, unitOfWork.BranchRepository)
        {
            _branchQueryObject = branchQueryObject;
        }

        public IEnumerable<BranchDto> GetBranchesByName(string name)
        {
            return _branchQueryObject.ExecuteQuery(new BranchFilterDto() { Name = name, SortCriteria = nameof(Branch.Name), SortAscending = true }).Items;
        }

        public IEnumerable<BranchDto> GetBranchesByAddress(string address)
        {
            return _branchQueryObject.ExecuteQuery(new BranchFilterDto() { Address = address, SortCriteria = nameof(Branch.Address), SortAscending = true }).Items;
        }
    }
}
