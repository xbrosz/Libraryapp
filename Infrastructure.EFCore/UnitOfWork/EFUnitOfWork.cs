using DAL.Data;
using DAL.Entities;
using Infrastructure.EFCore.Repository;
using Infrastructure.Repository;

namespace Infrastructure.EFCore.UnitOfWork
{
    public class EFUnitOfWork
    {
        public LibraryappDbContext Context { get; } = new();
        private IRepository<Author> authorRepository;
        private IRepository<Book> bookRepository;
        private IRepository<BookPrint> bookPrintRepository;
        private IRepository<Branch> branchRepository;
        private IRepository<Genre> genreRepository;
        private IRepository<Rating> ratingRepository;
        private IRepository<Reservation> reservationRepository;
        private IRepository<Role> roleRepository;
        private IRepository<User> userRepository;

        public EFUnitOfWork(LibraryappDbContext dbContext)
        {
            Context = dbContext;
        }

        public IRepository<Author> AuthorRepository
        {
            get
            {
                if (this.authorRepository == null)
                {
                    this.authorRepository = new EFGenericRepository<Author>(Context);
                }
                return authorRepository;
            }
        }

        public IRepository<Book> BookRepository
        {
            get
            {
                if (this.bookRepository == null)
                {
                    this.bookRepository = new EFGenericRepository<Book>(Context);
                }
                return bookRepository;
            }
        }

        public IRepository<BookPrint> BookPrintRepository
        {
            get
            {
                if (this.bookPrintRepository == null)
                {
                    this.bookPrintRepository = new EFGenericRepository<BookPrint>(Context);
                }
                return bookPrintRepository;
            }
        }

        public IRepository<Branch> BranchRepository
        {
            get
            {
                if (this.branchRepository == null)
                {
                    this.branchRepository = new EFGenericRepository<Branch>(Context);
                }
                return branchRepository;
            }
        }

        public IRepository<Genre> GenreRepository
        {
            get
            {
                if (this.genreRepository == null)
                {
                    this.genreRepository = new EFGenericRepository<Genre>(Context);
                }
                return genreRepository;
            }
        }

        public IRepository<Rating> RatingRepository
        {
            get
            {
                if (this.ratingRepository == null)
                {
                    this.ratingRepository = new EFGenericRepository<Rating>(Context);
                }
                return ratingRepository;
            }
        }

        public IRepository<Reservation> ReservationRepository
        {
            get
            {
                if (this.reservationRepository == null)
                {
                    this.reservationRepository = new EFGenericRepository<Reservation>(Context);
                }
                return reservationRepository;
            }
        }

        public IRepository<Role> RoleRepository
        {
            get
            {
                if (this.roleRepository == null)
                {
                    this.roleRepository = new EFGenericRepository<Role>(Context);
                }
                return roleRepository;
            }
        }

        public IRepository<User> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new EFGenericRepository<User>(Context);
                }
                return userRepository;
            }
        }

        public async Task Commit()
        {
            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
