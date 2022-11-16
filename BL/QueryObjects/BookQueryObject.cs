using AutoMapper;
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

namespace BL.QueryObjects
{
    public class BookQueryObject
    {
            private IMapper mapper;

            private IGenericQuery<Book> myQuery;

            public BookQueryObject(IMapper mapper, LibraryappDbContext context)
            {
                this.mapper = mapper;
                myQuery = new EFGenericQuery<Book>(context);
            }

            public QueryResultDto<BookGridDto> ExecuteQuery(BookFilterDto filter)
            {
            var query = myQuery.Where<int>(a => a == filter.AuthorID, "AuthorId");
                if (filter.RequestedPageNumber.HasValue)
                {
                    query = query.Page(filter.RequestedPageNumber.Value, filter.PageSize);
                }

                return mapper.Map<QueryResultDto<BookGridDto>>(query.Execute());
            }
    }
}
