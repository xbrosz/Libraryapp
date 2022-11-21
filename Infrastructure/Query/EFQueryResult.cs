using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Query
{
    public class EFQueryResult<TEntity>
    {
        public long TotalItemsCount { get; set; }
        public int? RequestedPageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<TEntity> Items { get; set; }

    }
}
