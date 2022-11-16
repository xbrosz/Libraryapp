using AutoMapper;
using BL.DTOs.Author;
using BL.DTOs;
using DAL.Data;
using DAL.Entities;
using Infrastructure.EFCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTOs.Branch;

namespace BL.QueryObjects
{
    public class BranchQueryObject
    {
        private IMapper _mapper;
        private EFGenericQuery<Author> _query;
        public BranchQueryObject(IMapper mapper, LibraryappDbContext dbContext)
        {
            _mapper = mapper;
            _query = new EFGenericQuery<Author>(dbContext);
        }

        public QueryResultDto<BranchDto> ExecuteQuery(BranchFilterDto filter)
        {
            var query = _query.Where<string>(a => a == filter.Name, nameof(Branch.Name));
            return _mapper.Map<QueryResultDto<BranchDto>>(query.Execute());
        }
    }
}
