using DAL.Entities;
using System.Linq.Expressions;

namespace Infrastructure.Query
{
    public interface IReservationQuery
    {
        IReservationQuery Where<T>(Expression<Func<T, bool>> rootPredicate, string columnName);

        /// <summary>
        /// Adds a specified sort criteria to the query.
        /// </summary>
        IReservationQuery OrderBy<T>(string columnName, bool ascendingOrder = true) where T : IComparable<T>;

        /// <summary>
        /// Adds a posibility to paginate the result
        /// </summary>
        IReservationQuery Page(int pageToFetch, int pageSize = 10);
        IReservationQuery FromFilter(DateTime from);
        IReservationQuery ToFilter(DateTime to);

        EFQueryResult<Reservation> Execute();
    }
}
