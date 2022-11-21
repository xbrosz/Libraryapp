using AutoMapper;
using BL.DTOs;
using BL.DTOs.Branch;
using BL.QueryObjects.IQueryObject;
using DAL.Entities;
using Infrastructure.Query;

namespace BL.QueryObjects.QueryObjects
{
    public class BranchQueryObject : IQueryObject<BranchFilterDto, BranchDto>
    {
        private IMapper _mapper;
        private IAbstractQuery<Branch> _query;
        public BranchQueryObject(IMapper mapper, IAbstractQuery<Branch> query)
        {
            _mapper = mapper;
            _query = query;
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

            if (!string.IsNullOrWhiteSpace(filter.SortCriteria))
            {
                _query.OrderBy<string>(filter.SortCriteria, filter.SortAscending);
            }

            return _mapper.Map<QueryResultDto<BranchDto>>(_query.Execute());
        }
    }
}
