using DAL.Entities;
using Infrastructure.EFCore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Service
{
    public class BranchService
    {
        private EFGenericRepository<Branch> _branchRepository;
    }
}
