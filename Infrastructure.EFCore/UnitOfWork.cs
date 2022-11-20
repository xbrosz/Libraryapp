using DAL.Data;
using DAL.Entities;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace Infrastructure.EFCore
{
    public class UnitOfWork : IUnitOfWork
    {
        private LibraryappDbContext _context;
        private IRepository<Author> _authorRepository;
        private IRepository<Book> _bookRepository;
        private IRepository<BookPrint> _bookPrintRepository;
        private IRepository<Branch> _branchRepository;
        private IRepository<Genre> _genreRepository;
        private IRepository<Rating> _ratingRepository;
        private IRepository<Reservation> _reservationRepository;
        private IRepository<Role> _roleRepository;
        private IRepository<User> _userRepository;

        public UnitOfWork(LibraryappDbContext dbContext)
        {
            _context = dbContext;
        }

        public IRepository<Author> AuthorRepository
        {
            get
            {
                if (_authorRepository == null)
                {
                    _authorRepository = new Repository<Author>(_context);
                }
                return _authorRepository;
            }
        }

        public IRepository<Book> BookRepository
        {
            get
            {
                if (_bookRepository == null)
                {
                    _bookRepository = new Repository<Book>(_context);
                }
                return _bookRepository;
            }
        }

        public IRepository<BookPrint> BookPrintRepository
        {
            get
            {
                if (_bookPrintRepository == null)
                {
                    _bookPrintRepository = new Repository<BookPrint>(_context);
                }
                return _bookPrintRepository;
            }
        }

        public IRepository<Branch> BranchRepository
        {
            get
            {
                if (_branchRepository == null)
                {
                    _branchRepository = new Repository<Branch>(_context);
                }
                return _branchRepository;
            }
        }

        public IRepository<Genre> GenreRepository
        {
            get
            {
                if (_genreRepository == null)
                {
                    _genreRepository = new Repository<Genre>(_context);
                }
                return _genreRepository;
            }
        }

        public IRepository<Rating> RatingRepository
        {
            get
            {
                if (_ratingRepository == null)
                {
                    _ratingRepository = new Repository<Rating>(_context);
                }
                return _ratingRepository;
            }
        }

        public IRepository<Reservation> ReservationRepository
        {
            get
            {
                if (_reservationRepository == null)
                {
                    _reservationRepository = new Repository<Reservation>(_context);
                }
                return _reservationRepository;
            }
        }

        public IRepository<Role> RoleRepository
        {
            get
            {
                if (_roleRepository == null)
                {
                    _roleRepository = new Repository<Role>(_context);
                }
                return _roleRepository;
            }
        }

        public IRepository<User> UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new Repository<User>(_context);
                }
                return _userRepository;
            }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
