using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Query
{
    public abstract class ReservationQuery : IReservationQuery
    {
        public List<(Expression expression, Type argumentType, string columnName)> WherePredicate { get; set; } = new();
        public (string tableName, bool isAscending, Type argumentType)? OrderByContainer { get; set; }
        public (int PageToFetch, int PageSize)? PaginationContainer { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }

        public ReservationQuery() : base() { }

        public IReservationQuery FromFilter(DateTime from)
        {
            fromDate = from;
            return this;
        }

        public IReservationQuery ToFilter(DateTime to)
        {
            toDate = to;
            return this;
        }

        public IReservationQuery Page(int pageToFetch, int pageSize = 10)
        {
            PaginationContainer = (pageToFetch, pageSize);
            return this;
        }

        public IReservationQuery OrderBy<T>(string columnName, bool ascendingOrder = true) where T : IComparable<T>
        {
            OrderByContainer = (columnName, ascendingOrder, typeof(T));
            return this;
        }

        public IReservationQuery Where<T>(Expression<Func<T, bool>> predicate, string columnName)
        {
            WherePredicate.Add((predicate, typeof(T), columnName));
            return this;
        }

        protected void ClearContainers()
        {
            WherePredicate.Clear();
            OrderByContainer = null;
            PaginationContainer = null;
        }

        public abstract IEnumerable<Reservation> Execute();
    }
}
