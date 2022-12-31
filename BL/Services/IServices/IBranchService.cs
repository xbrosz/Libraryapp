using BL.DTOs.Author;
using BL.DTOs.Branch;

namespace BL.Services.IServices
{
    public interface IBranchService
    {
        public BranchDto Find(int id);

        public void Delete(int id);

        public void Update(BranchDto dtoToUpdate);

        public void Insert(BranchDto dtoToInsert);

        public IEnumerable<BranchDto> GetBranchesByName(string name);

        public IEnumerable<BranchDto> GetBranchesByAddress(string address);

        public IEnumerable<BranchDto> GetAllBranches();

    }
}
