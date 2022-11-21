using AutoMapper;
using BL.DTOs;
using BL.QueryObjects.IQueryObject;
using DAL.Entities;
using Infrastructure.Query;

namespace BL.QueryObjects.QueryObjects
{
    public class BookPrintQueryObject : IQueryObject<BookPrintFilterDto, BookPrintDto>
    {
        private IMapper mapper;

        private IAbstractQuery<BookPrint> myQuery;

        public BookPrintQueryObject(IMapper mapper, IAbstractQuery<BookPrint> context)
        {
            this.mapper = mapper;
            myQuery = context;
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

            if (filter.ReservedBookPrintIDs != null)
            {
                query = query.Where<int>(a => !filter.ReservedBookPrintIDs.Contains(a), "Id");
            }

            if (filter.RequestedPageNumber.HasValue)
            {
                query = query.Page(filter.RequestedPageNumber.Value, filter.PageSize);
            }

            return mapper.Map<QueryResultDto<BookPrintDto>>(query.Execute());
        }
    }
}
