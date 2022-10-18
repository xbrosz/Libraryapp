using System.Linq.Expressions;

namespace Infrastructure.Query
{
    public abstract class GenericQuery<TEntity> : IGenericQuery<TEntity> where TEntity : class, new()
    {
        public List<(Expression expression, Type argumentType, string columnName)> WherePredicate { get; set; } = new();

        public IGenericQuery<TEntity> Where<T>(Expression<Func<T, bool>> predicate, string columnName) where T : IComparable<T>
        {
            WherePredicate.Add((predicate, typeof(T), columnName));
            return this;
        }

        public abstract IEnumerable<TEntity> Execute();
    }
}
