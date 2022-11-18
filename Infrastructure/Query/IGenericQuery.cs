using System.Linq.Expressions;

namespace Infrastructure.Query
{
    public interface IGenericQuery<TEntity> where TEntity : class, new()
    {
        IGenericQuery<TEntity> Where<T>(Expression<Func<T, bool>> rootPredicate, string columnName);

        /// <summary>
        /// Adds a specified sort criteria to the query.
        /// </summary>
        IGenericQuery<TEntity> OrderBy<T>(string columnName, bool ascendingOrder = true) where T : IComparable<T>;

        /// <summary>
        /// Adds a posibility to paginate the result
        /// </summary>
        IGenericQuery<TEntity> Page(int pageToFetch, int pageSize = 10);

        IEnumerable<TEntity> Execute();
    }
}
