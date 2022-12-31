using BL.DTOs.Branch;

namespace FE.Models.Admin
{
    public class AdminBranchIndexViewModel
    {
        public List<Tuple<BranchDto, bool>> Branches { get; set; }
    }
}
