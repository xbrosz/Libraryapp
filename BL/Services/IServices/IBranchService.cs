using BL.DTOs.Branch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.IServices
{
    public interface IBranchService
    {
        public IEnumerable<BranchDto> GetBranchesByName(BranchFilterDto filter);

        public IEnumerable<BranchDto> GetBranchesByAddress(BranchFilterDto filter);
    }
}
