using AutoMapper;
using BL.DTOs;
using BL.DTOs.User;
using BL.QueryObjects.IQueryObject;
using DAL.Entities;
using Infrastructure.Query;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            if (!string.IsNullOrWhiteSpace(filter.UserName))
            {
                _myQuery = filter.ExactUserName ? _myQuery.Where<string>(a => a == filter.UserName, nameof(User.UserName))
                : _myQuery.Where<string>(a => a.ToLower().Contains(filter.UserName.ToLower()), nameof(User.UserName));
            }

            if (filter.RequestedPageNumber.HasValue)
            {
                _myQuery.Page(filter.RequestedPageNumber.Value, filter.PageSize);
            }

            return _mapper.Map<QueryResultDto<UserDetailDto>>(_myQuery.Execute());
        }
    }
}
