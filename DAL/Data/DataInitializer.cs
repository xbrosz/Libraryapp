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
            var genre4 = new Genre { Id = 4, Name = "Romance", };
            var branch = new Branch { Address = "botanicka 68A", Id = 1, Name = "Pobocka" };
            var bookPrint1 = new BookPrint { BranchId = 1, Id = 1, BookId = 1 };
            var bookPrint2 = new BookPrint { BranchId = 1, Id = 2, BookId = 2 };
            var rating = new Rating { Id = 1, RatingNumber = 4, Comment = "Super", BookId = 1, UserId = 1 };

            var book1 = new Book { AuthorId = 1, Id = 1, Title = "Space", Release = DateTime.Now, RatingNumber = 4, SortRatingNumber = 4 };
            var book2 = new Book { AuthorId = 2, Id = 2, Title = "The chain", Release = DateTime.Now };
            var book3 = new Book { AuthorId = 2, Id = 3, Title = "Snow", Release = DateTime.Now };
            var book4 = new Book { AuthorId = 1, Id = 4, Title = "The war", Release = DateTime.Now };
            var book5 = new Book { AuthorId = 1, Id = 5, Title = "Vote", Release = DateTime.Now };
            var book6 = new Book { AuthorId = 1, Id = 6, Title = "Mountains", Release = DateTime.Now };
            var book7 = new Book { AuthorId = 1, Id = 7, Title = "Himalay", Release = DateTime.Now };
            var book8 = new Book { AuthorId = 1, Id = 8, Title = "Tatras", Release = DateTime.Now };
            var book9 = new Book { AuthorId = 2, Id = 9, Title = "Slovakia", Release = DateTime.Now };
            var book10 = new Book { AuthorId = 2, Id = 10, Title = "Skill", Release = DateTime.Now };
            var book11 = new Book { AuthorId = 2, Id = 11, Title = "Muni guide", Release = DateTime.Now };
            var book12 = new Book { AuthorId = 2, Id = 12, Title = "C#", Release = DateTime.Now };

            //var reservation1 = new Reservation { BookPrintId = 1, Id = 1, UserId = 1, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(4) };
            //var reservation2 = new Reservation { BookPrintId = 2, Id = 2, UserId = 1, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(2) };
            
            var bookgenre1 = new BookGenre { Id = 1, BookId = 1, GenreId = 1 };
            var bookgenre2 = new BookGenre { Id = 2, BookId = 2, GenreId = 3 };
            var bookgenre3 = new BookGenre { Id = 3, BookId = 2, GenreId = 2 };
            var bookgenre4 = new BookGenre { Id = 4, BookId = 3, GenreId = 2 };
            var bookgenre5 = new BookGenre { Id = 5, BookId = 4, GenreId =1  };
            var bookgenre6 = new BookGenre { Id = 6, BookId = 5, GenreId =1  };
            var bookgenre7 = new BookGenre { Id = 7, BookId = 6, GenreId =2  };
            var bookgenre8 = new BookGenre { Id = 8, BookId = 7, GenreId =2  };
            var bookgenre9 = new BookGenre { Id = 9, BookId = 8, GenreId =2  };
            var bookgenre10 = new BookGenre { Id = 10, BookId = 9, GenreId =3  };
            var bookgenre11 = new BookGenre { Id = 11, BookId = 10, GenreId =3  };
            var bookgenre12 = new BookGenre { Id = 12, BookId = 11, GenreId = 3 };
            var bookgenre13 = new BookGenre { Id = 13, BookId = 12, GenreId = 4 };
            var bookgenre14 = new BookGenre { Id = 14, BookId = 6, GenreId = 1 };
            var bookgenre15 = new BookGenre { Id = 15, BookId = 7, GenreId = 1 };
            var bookgenre16 = new BookGenre { Id = 16, BookId = 8, GenreId = 4 };


            modelBuilder.Entity<Role>().HasData(role_admin);
            modelBuilder.Entity<Role>().HasData(role_user);
            modelBuilder.Entity<User>().HasData(user);
            modelBuilder.Entity<User>().HasData(admin);
            modelBuilder.Entity<Author>().HasData(author1);
            modelBuilder.Entity<Author>().HasData(author2);
            modelBuilder.Entity<Book>().HasData(book1);
            modelBuilder.Entity<Book>().HasData(book2);
            modelBuilder.Entity<Book>().HasData(book3);
            modelBuilder.Entity<Book>().HasData(book4);
            modelBuilder.Entity<Book>().HasData(book5);
            modelBuilder.Entity<Book>().HasData(book6);
            modelBuilder.Entity<Book>().HasData(book7);
            modelBuilder.Entity<Book>().HasData(book8);
            modelBuilder.Entity<Book>().HasData(book9);
            modelBuilder.Entity<Book>().HasData(book10);
            modelBuilder.Entity<Book>().HasData(book11);
            modelBuilder.Entity<Book>().HasData(book12);
            modelBuilder.Entity<Genre>().HasData(genre1);
            modelBuilder.Entity<Genre>().HasData(genre2);
            modelBuilder.Entity<Genre>().HasData(genre3);
            modelBuilder.Entity<Genre>().HasData(genre4);
            modelBuilder.Entity<Branch>().HasData(branch);

            modelBuilder.Entity<BookPrint>().HasData(bookPrint1);
            modelBuilder.Entity<BookPrint>().HasData(bookPrint2);
            modelBuilder.Entity<Rating>().HasData(rating);

            //modelBuilder.Entity<Reservation>().HasData(reservation1);
            //modelBuilder.Entity<Reservation>().HasData(reservation2);

            modelBuilder.Entity<BookGenre>().HasData(bookgenre1);
            modelBuilder.Entity<BookGenre>().HasData(bookgenre2);
            modelBuilder.Entity<BookGenre>().HasData(bookgenre3);
            modelBuilder.Entity<BookGenre>().HasData(bookgenre4);
            modelBuilder.Entity<BookGenre>().HasData(bookgenre5);
            modelBuilder.Entity<BookGenre>().HasData(bookgenre6);
            modelBuilder.Entity<BookGenre>().HasData(bookgenre7);
            modelBuilder.Entity<BookGenre>().HasData(bookgenre8);
            modelBuilder.Entity<BookGenre>().HasData(bookgenre9);
            modelBuilder.Entity<BookGenre>().HasData(bookgenre10);
            modelBuilder.Entity<BookGenre>().HasData(bookgenre11);
            modelBuilder.Entity<BookGenre>().HasData(bookgenre12);
            modelBuilder.Entity<BookGenre>().HasData(bookgenre13);
            modelBuilder.Entity<BookGenre>().HasData(bookgenre14);
            modelBuilder.Entity<BookGenre>().HasData(bookgenre15);
            modelBuilder.Entity<BookGenre>().HasData(bookgenre16);
        }
    }
}
