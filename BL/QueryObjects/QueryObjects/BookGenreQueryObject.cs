using AutoMapper;
using BL.DTOs;
using BL.DTOs.BookGenre;
using BL.QueryObjects.IQueryObject;
using DAL.Entities;
using Infrastructure.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.QueryObjects.QueryObjects
{
    public class BookGenreQueryObject : IQueryObject<BookGenreFilterDto, BookGenreDto>
    {
        private IMapper _mapper;

        private IAbstractQuery<BookGenre> _query;

        public BookGenreQueryObject(IMapper mapper, IAbstractQuery<BookGenre> query)
        {
            _mapper = mapper;
            _query = query;
        }

        public QueryResultDto<BookGenreDto> ExecuteQuery(BookGenreFilterDto filter)
        {
            if (filter.BookId.HasValue)
            {
                _query.Where<int>(a => a == filter.BookId, nameof(BookGenre.BookId));
            }

            if (filter.GenreId.HasValue)
            {
                _query.Where<int>(a => a == filter.GenreId, nameof(BookGenre.Genre.Id));
            }

            if (!string.IsNullOrWhiteSpace(filter.GenreName))
            {
                _query.Where<string>(a => a.ToLower().Contains(filter.GenreName.ToLower()), nameof(BookGenre.Genre.Name));
            }

            return _mapper.Map<QueryResultDto<BookGenreDto>>(_query.Execute());
        }
    }
}
