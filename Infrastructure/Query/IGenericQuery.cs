using System.Linq.Expressions;

namespace Infrastructure.Query
{
    public interface IGenericQuery<TEntity> where TEntity : class, new()
    {
        IGenericQuery<TEntity> Where<T>(Expression<Func<T, bool>> rootPredicate, string columnName) where T : IComparable<T>;

        IEnumerable<TEntity> Execute();
    }
}
