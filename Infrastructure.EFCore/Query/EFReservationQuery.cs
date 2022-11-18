using DAL.Data;
using DAL.Entities;
using Infrastructure.EFCore.ExpressionHelpers;
using Infrastructure.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EFCore.Query
{
    public class EFReservationQuery : ReservationQuery
    {
        protected LibraryappDbContext DbContext { get; set; }

        public EFReservationQuery(LibraryappDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public override IEnumerable<Reservation> Execute()
        {
            IQueryable<Reservation> query = DbContext.Set<Reservation>();

            if (WherePredicate.Capacity != 0)
            {
                query = ApplyWhere(query);
            }

            if (fromDate.HasValue)
            {
                query = ApplyFromFilter(query);
            }

            if (fromDate.HasValue)
            {
                query = ApplyToFilter(query);
            }

            if (OrderByContainer != null)
            {
                query = OrderBy(query);
            }

            if (PaginationContainer.HasValue)
            {
                query = Pagination(query);
            }

            return query.ToList();
        }

        private IQueryable<Reservation> ApplyFromFilter(IQueryable<Reservation> query)
        {
            query.Where(r => r.StartDate >= fromDate || r.EndDate >= fromDate);

            return query;
        }

        private IQueryable<Reservation> ApplyToFilter(IQueryable<Reservation> query)
        {
            query.Where(r => r.StartDate <= toDate || r.EndDate <= toDate);

            return query;
        }

        private IQueryable<Reservation> ApplyWhere(IQueryable<Reservation> query)
        {
            foreach (var expr in WherePredicate)
            {
                var p = Expression.Parameter(typeof(Reservation), "p");

                var columnNameFromObject = typeof(Reservation)
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

                var lambda = Expression.Lambda<Func<Reservation, bool>>(exprNewBody, p);

                query = query.Where(lambda);
            }

            return query;
        }

        private IQueryable<Reservation> OrderBy(IQueryable<Reservation> query)
        {
            var orderByColumn = OrderByContainer.Value.tableName;
            var isAscending = OrderByContainer.Value.isAscending;
            var argumentType = OrderByContainer.Value.argumentType;

            var p = Expression.Parameter(typeof(Reservation), "p");

            var columnNameFromObject = typeof(Reservation)
                .GetProperty(orderByColumn)
                ?.Name;

            var exprProp = Expression.Property(p, columnNameFromObject);
            var lambda = Expression.Lambda(exprProp, p);

            var orderByMethod = typeof(Queryable)
                .GetMethods()
                .First(a => a.Name == (isAscending ? "OrderBy" : "OrderByDescending") && a.GetParameters().Length == 2);

            var orderByClosedMethod = orderByMethod.MakeGenericMethod(typeof(Reservation), argumentType);

            return (IQueryable<Reservation>)orderByClosedMethod.Invoke(null, new object[] { query, lambda })!;
        }

        private IQueryable<Reservation> Pagination(IQueryable<Reservation> query)
        {
            var page = PaginationContainer.Value.PageToFetch;
            var pageSize = PaginationContainer.Value.PageSize;

            return query
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }
    }
}
