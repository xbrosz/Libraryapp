using Infrastructure.DTOs.Book.Out;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Query.Book
{
    public interface IBookQuery
    {
        Task<List<BookDTO>> GetAll();

        Task<BookDTO> GetByID(int ID);

    }
}
