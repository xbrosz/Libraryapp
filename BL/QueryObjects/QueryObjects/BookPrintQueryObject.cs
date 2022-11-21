using AutoMapper;
using BL.DTOs;
using BL.DTOs.Author;
using BL.QueryObjects.IQueryObject;
using DAL.Data;
using DAL.Entities;
using Infrastructure.EFCore;
using Infrastructure.Query;

namespace BL.QueryObjects.QueryObjects
{
    public class BookPrintQueryObject : IQueryObject<BookPrintFilterDto, BookPrintDto>
    {
        private IMapper _mapper;

        private IAbstractQuery<BookPrint> _query;

        public BookPrintQueryObject(IMapper mapper, IAbstractQuery<BookPrint> query)
        {
            _mapper = mapper;
            _query = query;
        }

        public QueryResultDto<BookPrintDto> ExecuteQuery(BookPrintFilterDto filter)
        {
            if (filter.BranchId != null)
            {
                _query = _query.Where<int>(a => a == filter.BranchId, "BranchId");
            }
            if (filter.BookId != null && _query != null)
            {
                _query = _query.Where<int>(a => a == filter.BookId, "BookId");
            }

            if (filter.ReservedBookPrintIDs != null)
            {
                _query = _query.Where<int>(a => !filter.ReservedBookPrintIDs.Contains(a), "Id");
            }

            if (filter.RequestedPageNumber.HasValue)
            {
                _query = _query.Page(filter.RequestedPageNumber.Value, filter.PageSize);
            }
            if (_query is null)
            {
                return null;
            }
            var res = _query.Execute();
            return _mapper.Map<QueryResultDto<BookPrintDto>>(res);
        }
    }
}
