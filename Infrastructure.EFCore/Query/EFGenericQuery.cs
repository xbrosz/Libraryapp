using DAL.Data;
using Infrastructure.EFCore.ExpressionHelpers;
using Infrastructure.Query;
using System.Linq.Expressions;

namespace Infrastructure.EFCore.Query
{
    public class EFGenericQuery<TEntity> : GenericQuery<TEntity> where TEntity : class, new()
    {
        protected LibraryappDbContext DbContext { get; set; }

        public EFGenericQuery(LibraryappDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public override IEnumerable<TEntity> Execute()
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>();

            if (WherePredicate.Capacity != 0)
            {
                query = ApplyWhere(query);
            }

            return query.ToList();
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
    }
}
