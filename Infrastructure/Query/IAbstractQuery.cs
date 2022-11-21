using System.Linq.Expressions;

namespace Infrastructure.Query
{
    public interface IAbstractQuery<TEntity> where TEntity : class, new()
    {
        IAbstractQuery<TEntity> Where<T>(Expression<Func<T, bool>> rootPredicate, string columnName) where T : IComparable<T>;

        IAbstractQuery<TEntity> OrderBy<T>(string columnName, bool ascendingOrder = true) where T : IComparable<T>;

        IAbstractQuery<TEntity> Page(int pageToFetch, int pageSize = 10);

        EFQueryResult<TEntity> Execute();
    }
}
