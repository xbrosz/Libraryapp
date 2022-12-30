using BL.DTOs.Genre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.IServices
{
    public interface IGenreService
    {
        int? GetGenreIdForName(string name);

        void Delete(int id);

        GenreDto Find(int id);
        void Update(GenreDto genre);

        void Insert(GenreDto dtoToInsert);
    }
}
