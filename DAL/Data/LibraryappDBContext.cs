using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    internal class LibraryappDbContext : DbContext
    {
        private const string ConnectionString = $"Server=(localdb)\\mssqllocaldb;Integrated Security=True;MultipleActiveResultSets=True;Database=Libraryapp;Trusted_Connection=True;";

        public DbSet<Author> Author { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Branch> Branch { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<Role> Role { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }

    }
}