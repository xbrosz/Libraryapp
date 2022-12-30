using DAL.Entities;
using Infrastructure.Repository;

namespace Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Author> AuthorRepository { get; }

        IRepository<Book> BookRepository { get; }

        IRepository<BookPrint> BookPrintRepository { get; }

        IRepository<Branch> BranchRepository { get; }

        IRepository<Genre> GenreRepository { get; }

        IRepository<Rating> RatingRepository { get; }

        IRepository<Reservation> ReservationRepository { get; }

        IRepository<Role> RoleRepository { get; }

        IRepository<User> UserRepository { get; }
        IRepository<BookGenre> BookGenreRepository { get; }

        public void Commit();

        public void Dispose();
    }
}
