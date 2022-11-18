using AutoMapper;
using BL.DTOs;
using BL.DTOs.Author;
using BL.DTOs.Branch;
using BL.QueryObjects.IQueryObject;
using DAL.Data;
using DAL.Entities;
using Infrastructure.EFCore;

namespace BL.QueryObjects.QueryObjects
{
    public class BranchQueryObject : IQueryObject<BranchFilterDto, BranchDto>
    {
        private IMapper _mapper;
        private GenericQuery<Author> _query;
        public BranchQueryObject(IMapper mapper, LibraryappDbContext dbContext)
        {
            _mapper = mapper;
            _query = new GenericQuery<Author>(dbContext);
        }

        public QueryResultDto<BranchDto> ExecuteQuery(BranchFilterDto filter)
        {
            var query = _query.Where<string>(a => a == filter.Name, nameof(Branch.Name));

            return _mapper.Map<QueryResultDto<BranchDto>>(query.Execute().First<Author>());
        }
    }
}
