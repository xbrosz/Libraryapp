using System.Linq.Expressions;

namespace Infrastructure.Query
{
    public interface IAbstractQuery<TEntity> where TEntity : class, new()
    {
        IAbstractQuery<TEntity> Where<T>(Expression<Func<T, bool>> rootPredicate, string columnName) where T : IComparable<T>;

        /// <summary>
        /// Adds a specified sort criteria to the query.
        /// </summary>
        IAbstractQuery<TEntity> OrderBy<T>(string columnName, bool ascendingOrder = true) where T : IComparable<T>;

        /// <summary>
        /// Adds a posibility to paginate the result
        /// </summary>
        IAbstractQuery<TEntity> Page(int pageToFetch, int pageSize = 10);

        IEnumerable<TEntity> Execute();
    }
}
