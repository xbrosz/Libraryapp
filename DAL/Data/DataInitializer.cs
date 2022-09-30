using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public static class DataInitializer
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var role = new Role { Id = 1, Name = "Admin" };
            var user = new User { Id = 1, Address = "", Email = "", FirstName = "Peter", LastName = "Pavol", Password = "123", PhoneNumber = "4567890", UserName = "P", RoleId = 1 };
            var author = new Author { BirthDate = DateTime.Now, FirstName = "Štefan" , Id = 1, LastName = "Hemingway" , MiddleName=""};
            var genre = new Genre { Id = 1, Name = "Sci-fi", };
            var branch = new Branch { Address = "botanicka 68A", Id = 1, Name = "Pobocka" };
            var bookPrint = new BookPrint { BranchId = 1, Id = 1, BookId = 1};
            var rating = new Rating { Id = 1, RatingNumber = 4, Comment = "Super" , BookId = 1};
            var book = new Book { AuthorId = 1, Id = 1, Title = "Space", Release = DateTime.Now };
            var reservation = new Reservation { BookPrintId = 1, Id = 1, UserId = 1, StartDate = DateTime.Now, EndDate = DateTime.Now };
            var bookgenre = new BookGenre { Id = 1, BookId = 1, GenreId = 1 };
  

            modelBuilder.Entity<Role>().HasData(role);
            modelBuilder.Entity<User>().HasData(user);
            modelBuilder.Entity<Author>().HasData(author);
            modelBuilder.Entity<Book>().HasData(book);
            modelBuilder.Entity<Genre>().HasData(genre);
            modelBuilder.Entity<Branch>().HasData(branch);
           
            modelBuilder.Entity<BookPrint>().HasData(bookPrint);
            modelBuilder.Entity<Rating>().HasData(rating);
    
            modelBuilder.Entity<Reservation>().HasData(reservation);
            modelBuilder.Entity<BookGenre>().HasData(bookgenre);
        }
    }
}
