using AutoMapper;
using BL.DTOs.Reservation;
using BL.DTOs;
using DAL.Data;
using DAL.Entities;
using Infrastructure.EFCore.Query;
using Infrastructure.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTOs.User;

namespace BL.QueryObjects
{
    public class UserQueryObject : QueryObject<UserFilterDto, UserDetailDto>
    {
        private IMapper mapper;

        private IGenericQuery<User> myQuery;

        public UserQueryObject(IMapper mapper, LibraryappDbContext dbx)
        {
            this.mapper = mapper;
            myQuery = new EFGenericQuery<User>(dbx);
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
