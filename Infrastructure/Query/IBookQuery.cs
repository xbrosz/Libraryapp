using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Query
{
    public interface IBookQuery
    {
        public double GetAverageRatingForBook(int BookID);

    }
}
