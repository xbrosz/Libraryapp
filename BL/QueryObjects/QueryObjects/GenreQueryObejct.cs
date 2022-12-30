using AutoMapper;
using BL.DTOs.Author;
using BL.DTOs;
using DAL.Entities;
using Infrastructure.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.QueryObjects.IQueryObject;
using BL.DTOs.Genre;

namespace BL.QueryObjects.QueryObjects
{
    public class GenreQueryObejct : IQueryObject<GenreDto, GenreDto>
    {
        private IMapper _mapper;
        private IAbstractQuery<Genre> _query;
        public GenreQueryObejct(IMapper mapper, IAbstractQuery<Genre> query)
        {
            _mapper = mapper;
            _query = query;
        }

        public QueryResultDto<GenreDto> ExecuteQuery(GenreDto filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                _query.Where<string>(a => a == filter.Name, nameof(Genre.Name));
            }

            return _mapper.Map<QueryResultDto<GenreDto>>(_query.Execute());
        }
    }
}