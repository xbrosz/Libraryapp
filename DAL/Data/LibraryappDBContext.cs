using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class LibraryappDbContext : IdentityDbContext
    {
        public DbSet<Author> Author { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<BookPrint> BookPrint { get; set; }
        public DbSet<BookGenre> BookGenre { get; set; }
        public DbSet<Branch> Branch { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<Role> Role { get; set; }

        public LibraryappDbContext()
        {
        }

        public LibraryappDbContext(DbContextOptions<LibraryappDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Seed();


            base.OnModelCreating(modelBuilder);
        }

    }
}
