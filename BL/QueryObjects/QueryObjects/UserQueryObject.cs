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
        private IMapper mapper;

        private IAbstractQuery<User> myQuery;

        public UserQueryObject(IMapper mapper, IAbstractQuery<User> query)
        {
            this.mapper = mapper;
            myQuery = query;
        }

        public QueryResultDto<UserDetailDto> ExecuteQuery(UserFilterDto filter)
        {
            var query = filter.exactName ? myQuery.Where<string>(a => a == filter.name, "UserName")
                : myQuery.Where<string>(a => a.Contains(filter.name.ToLower()), "UserName");


            if (filter.RequestedPageNumber.HasValue)
            {
                query = query.Page(filter.RequestedPageNumber.Value, filter.PageSize);
            }

            return mapper.Map<QueryResultDto<UserDetailDto>>(query.Execute());
        }
    }
}
