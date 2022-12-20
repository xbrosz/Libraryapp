using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public static class DataInitializer
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var role_admin = new Role { Id = 1, Name = "Admin" };
            var role_user = new Role { Id = 2, Name = "User" };
            var user = new User
            {
                Id = 1,
                Address = "Bratislavská 25",
                Email = "peter@gmail.com",
                FirstName = "Peter",
                LastName = "Pavol",
                Password = "123",
                PhoneNumber = "4567890",
                UserName = "pp",
                RoleId = 2
            };
            var admin = new User
            {
                Id = 2,
                Address = "Bratislavská 21",
                Email = "karlic@gmail.com",
                FirstName = "Marek",
                LastName = "Karlicky",
                Password = "marekk",
                PhoneNumber = "4567890",
                UserName = "mk",
                RoleId = 1
            };
            var author1 = new Author { BirthDate = DateTime.Now, FirstName = "Štefan", Id = 1, LastName = "Hemingway", MiddleName = "" };
            var author2 = new Author { BirthDate = DateTime.Now, FirstName = "Adrian", Id = 2, LastName = "McKinty", MiddleName = "Alfonz" };
            var genre1 = new Genre { Id = 1, Name = "Sci-fi", };
            var genre2 = new Genre { Id = 2, Name = "Fantasy", };
            var genre3 = new Genre { Id = 3, Name = "Thriller", };
            var branch = new Branch { Address = "botanicka 68A", Id = 1, Name = "Pobocka" };
            var bookPrint1 = new BookPrint { BranchId = 1, Id = 1, BookId = 1 };
            var bookPrint2 = new BookPrint { BranchId = 1, Id = 2, BookId = 2 };
            var rating = new Rating { Id = 1, RatingNumber = 4, Comment = "Super", BookId = 1, UserId = 1 };

            var book1 = new Book { AuthorId = 1, Id = 1, Title = "Space", Release = DateTime.Now, RatingNumber = 3.4 };
            var book2 = new Book { AuthorId = 2, Id = 2, Title = "The chain", Release = DateTime.Now, RatingNumber = 5 };

            //var reservation1 = new Reservation { BookPrintId = 1, Id = 1, UserId = 1, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(4) };
            //var reservation2 = new Reservation { BookPrintId = 2, Id = 2, UserId = 1, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(2) };
            var bookgenre1 = new BookGenre { Id = 1, BookId = 1, GenreId = 1 };
            var bookgenre2 = new BookGenre { Id = 2, BookId = 2, GenreId = 3 };
            var bookgenre3 = new BookGenre { Id = 3, BookId = 2, GenreId = 2 };


            modelBuilder.Entity<Role>().HasData(role_admin);
            modelBuilder.Entity<Role>().HasData(role_user);
            modelBuilder.Entity<User>().HasData(user);
            modelBuilder.Entity<User>().HasData(admin);
            modelBuilder.Entity<Author>().HasData(author1);
            modelBuilder.Entity<Author>().HasData(author2);
            modelBuilder.Entity<Book>().HasData(book1);
            modelBuilder.Entity<Book>().HasData(book2);
            modelBuilder.Entity<Genre>().HasData(genre1);
            modelBuilder.Entity<Genre>().HasData(genre2);
            modelBuilder.Entity<Genre>().HasData(genre3);
            modelBuilder.Entity<Branch>().HasData(branch);

            modelBuilder.Entity<BookPrint>().HasData(bookPrint1);
            modelBuilder.Entity<BookPrint>().HasData(bookPrint2);
            modelBuilder.Entity<Rating>().HasData(rating);

            //modelBuilder.Entity<Reservation>().HasData(reservation1);
            //modelBuilder.Entity<Reservation>().HasData(reservation2);
            modelBuilder.Entity<BookGenre>().HasData(bookgenre1);
            modelBuilder.Entity<BookGenre>().HasData(bookgenre2);
            modelBuilder.Entity<BookGenre>().HasData(bookgenre3);
        }
    }
}
