using AutoMapper;
using BL.DTOs;
using BL.DTOs.BookGenre;
using BL.DTOs.Genre;
using BL.QueryObjects.IQueryObject;
using BL.Services.GenericService;
using BL.Services.IServices;
using DAL.Entities;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Services
{
    public class GenreService : GenericService<Genre, GenreDto, GenreDto, GenreDto>, IGenreService
    {
        private readonly IQueryObject<GenreDto, GenreDto> _genreQueryObject;

        public GenreService(IUnitOfWork unitOfWork, IMapper mapper, IQueryObject<GenreDto, GenreDto> genreQueryObject) : base(unitOfWork, mapper, unitOfWork.GenreRepository)
        {
            _genreQueryObject = genreQueryObject;
        }

        public int? GetGenreIdForName(string name)
        {
            var result = _genreQueryObject.ExecuteQuery(new GenreDto() { Name= name });
            if (result.TotalItemsCount== 0)
            {
                return null;
            }
            return result.Items.First().Id;
        }
    }
}
