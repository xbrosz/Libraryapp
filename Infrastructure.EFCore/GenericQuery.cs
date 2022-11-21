using DAL.Data;
using Infrastructure.Query;
using System.Linq.Expressions;

namespace Infrastructure.EFCore
{
    public class GenericQuery<TEntity> : AbstractQuery<TEntity> where TEntity : class, new()
    {
        private LibraryappDbContext _dbContext;

        public GenericQuery(LibraryappDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public override EFQueryResult<TEntity> Execute()
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (WherePredicate.Capacity != 0)
            {
                query = ApplyWhere(query);
            }

            if (OrderByContainer != null)
            {
                query = OrderBy(query);
            }

            if (PaginationContainer.HasValue)
            {
                query = Pagination(query);
            }

            var resultQuery = new EFQueryResult<TEntity>()
            {
                Items = query.ToList(),
                RequestedPageNumber = PaginationContainer != null ? PaginationContainer.Value.PageToFetch : null,
                PageSize = PaginationContainer != null ? PaginationContainer.Value.PageSize : 0
            };

            ClearContainers();  // clears all the containers after execution

            return resultQuery;
        }

        private IQueryable<TEntity> ApplyWhere(IQueryable<TEntity> query)
        {
            foreach (var expr in WherePredicate)
            {
                var p = Expression.Parameter(typeof(TEntity), "p");

                var columnNameFromObject = typeof(TEntity)
                    .GetProperty(expr.columnName)
                    ?.Name;

                var exprProp = Expression.Property(p, columnNameFromObject);

                var expression = expr.expression;

                var parameters = (IReadOnlyCollection<ParameterExpression>)expression
                    .GetType()
                    .GetProperty("Parameters")
                    ?.GetValue(expression);

                var body = (Expression)expression
                    .GetType()
                    .GetProperty("Body")
                    ?.GetValue(expression);

                var visitor = new ReplaceParamVisitor(parameters.First(), exprProp);

                var exprNewBody = visitor.Visit(body);

                var lambda = Expression.Lambda<Func<TEntity, bool>>(exprNewBody, p);

                query = query.Where(lambda);
            }

            return query;
        }

        private IQueryable<TEntity> OrderBy(IQueryable<TEntity> query)
        {
            var orderByColumn = OrderByContainer.Value.tableName;
            var isAscending = OrderByContainer.Value.isAscending;
            var argumentType = OrderByContainer.Value.argumentType;

            var p = Expression.Parameter(typeof(TEntity), "p");

            var columnNameFromObject = typeof(TEntity)
                .GetProperty(orderByColumn)
                ?.Name;

            var exprProp = Expression.Property(p, columnNameFromObject);
            var lambda = Expression.Lambda(exprProp, p);

            var orderByMethod = typeof(Queryable)
                .GetMethods()
                .First(a => a.Name == (isAscending ? "OrderBy" : "OrderByDescending") && a.GetParameters().Length == 2);

            var orderByClosedMethod = orderByMethod.MakeGenericMethod(typeof(TEntity), argumentType);

            return (IQueryable<TEntity>)orderByClosedMethod.Invoke(null, new object[] { query, lambda })!;
        }

        private IQueryable<TEntity> Pagination(IQueryable<TEntity> query)
        {
            var page = PaginationContainer.Value.PageToFetch;
            var pageSize = PaginationContainer.Value.PageSize;

            return query
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }
    }
}
