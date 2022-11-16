using BL.DTOs.Branch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public interface IBranchService
    {
        public BranchDto GetBranchByName(BranchFilterDto filter);
    }
}
