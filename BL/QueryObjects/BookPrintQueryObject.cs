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
    public class BookPrintQueryObject
    {
        private IMapper mapper;

        private IGenericQuery<BookPrint> myQuery;

        public BookPrintQueryObject(IMapper mapper, LibraryappDbContext context)
        {
            this.mapper = mapper;
            myQuery = new EFGenericQuery<BookPrint>(context);
        }

        public QueryResultDto<BookPrintDto> ExecuteQuery(BookPrintFilterDto filter)
        {
            var query = myQuery;
            if (filter.BranchId != null)
            {
                query = query.Where<int>(a => a == filter.BranchId, "BranchId");
            }
            if (filter.BookId != null)
            {
                query = query.Where<int>(a => a == filter.BookId, "BookId");
            }
            
            if (filter.RequestedPageNumber.HasValue)
            {
                query = query.Page(filter.RequestedPageNumber.Value, filter.PageSize);
            }

            return mapper.Map<QueryResultDto<BookPrintDto>>(query.Execute());
        }
    }
}
