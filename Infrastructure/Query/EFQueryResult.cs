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
