using BL.DTOs.Branch;

namespace BL.Services.IServices
{
    public interface IBranchService
    {
        public IEnumerable<BranchDto> GetBranchesByName(string name);

        public IEnumerable<BranchDto> GetBranchesByAddress(string address);
    }
}
