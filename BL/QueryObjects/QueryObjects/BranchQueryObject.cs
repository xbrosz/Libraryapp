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
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                _query.Where<string>(a => a.ToLower().Contains(filter.Name.ToLower()), nameof(Branch.Name));
            }

            if (!string.IsNullOrWhiteSpace(filter.Address))
            {
                _query.Where<string>(a => a.ToLower().Contains(filter.Address.ToLower()), nameof(Branch.Address));
            }

            if (filter.RequestedPageNumber.HasValue)
            {
                _query.Page(filter.RequestedPageNumber.Value, filter.PageSize);
            }

            return _mapper.Map<QueryResultDto<BranchDto>>(_query.Execute());
        }
    }
}
