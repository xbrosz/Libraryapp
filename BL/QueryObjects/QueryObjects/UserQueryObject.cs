using AutoMapper;
using BL.DTOs;
using BL.DTOs.User;
using BL.QueryObjects.IQueryObject;
using DAL.Entities;
using Infrastructure.Query;

namespace BL.QueryObjects.QueryObjects
{
    public class UserQueryObject : IQueryObject<UserFilterDto, UserDetailDto>
    {
        private IMapper _mapper;

        private IAbstractQuery<User> _myQuery;

        public UserQueryObject(IMapper mapper, IAbstractQuery<User> query)
        {
            _mapper = mapper;
            _myQuery = query;
        }

        public QueryResultDto<UserDetailDto> ExecuteQuery(UserFilterDto filter)
        {
            var query = filter.exactName ? _myQuery.Where<string>(a => a == filter.name, "UserName")
                : _myQuery.Where<string>(a => a.Contains(filter.name.ToLower()), "UserName");


            if (filter.RequestedPageNumber.HasValue)
            {
                query = query.Page(filter.RequestedPageNumber.Value, filter.PageSize);
            }

            return _mapper.Map<QueryResultDto<UserDetailDto>>(query.Execute());
        }
    }
}
